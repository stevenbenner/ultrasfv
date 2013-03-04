using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Security;
using System.Threading;
using System.Xml;

namespace UltraSFV.Core
{
	/// <summary>
	/// Provides methods for checking, downloading and applying updates from a web server over the internet. This class cannot be inherited.
	/// </summary>
	public sealed class AutoUpdater
	{
		private Version _AppVersion;
		private string _PatchFile = null;
		private string _BaseDir = null;
		private string _PublicKeyToken = null;
		private string _UpdateURL = null;

		#region Properties

		private UpdateInfo _UpdateInfo = null;
		public UpdateInfo UpdateInfo
		{
			get
			{
				return _UpdateInfo;
			}
		}

		public Version ServerVersion
		{
			get
			{
				if (_UpdateInfo == null)
				{
					GetUpdateInfo();
				}

				if (!String.IsNullOrEmpty(_UpdateInfo.Version))
				{
					return new Version(_UpdateInfo.Version);
				}
				else
				{
					return new Version(0, 0, 0, 0);
				}
			}
		}

		#endregion

		#region Constructor

		public AutoUpdater(Version version, Guid guid)
		{
			_BaseDir = AppDomain.CurrentDomain.BaseDirectory;
			_UpdateInfo = new UpdateInfo();
			_AppVersion = version;
			_UpdateURL = "http://ultrasfv.com/_system/update.php?version=" + _AppVersion.ToString() + "&guid=" + guid.ToString();

			// Grab the public key of the executing assembly
			Assembly exe = Assembly.GetEntryAssembly();
			Byte[] bytes = exe.GetName().GetPublicKeyToken();
			_PublicKeyToken = BitConverter.ToString(bytes).Replace("-", String.Empty);
			if (_PublicKeyToken == String.Empty)
				throw new SecurityException("Executing assembly must be strong named.");
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Pulls the latest update information from the server.
		/// </summary>
		/// <param name="version">Current version of the application.</param>
		/// <param name="guid">GUID of the application.</param>
		public void GetUpdateInfo()
		{
			ThreadPool.QueueUserWorkItem(new WaitCallback(CleanupTemps)); // cleanup temporary files

			XmlTextReader reader = null;
			//XmlSerializer s = new XmlSerializer(typeof(UpdateInfo));

			try
			{
				reader = new XmlTextReader(_UpdateURL);

				// Use the serialized xml technique? Great idea.
				// Expect this one line of code takes up to 15 damn seconds to execute...
				//UpdateInfo update = (UpdateInfo)s.Deserialize(reader);

				while (reader.Read())
				{
					if (reader.NodeType == XmlNodeType.Element)
					{
						switch (reader.Name)
						{
							case "fileName":
								_UpdateInfo.FileName = reader.ReadString();
								break;
							case "fileSize":
								_UpdateInfo.FileSize = reader.ReadString();
								break;
							case "crc":
								_UpdateInfo.CRC = reader.ReadString();
								break;
							case "url":
								_UpdateInfo.Url = reader.ReadString();
								break;
							case "version":
								_UpdateInfo.Version = reader.ReadString();
								break;
							case "releaseDate":
								_UpdateInfo.ReleaseDate = reader.ReadString();
								break;
						}
					}
				}

				if (reader != null)
					reader.Close();
			}
			catch (Exception)
			{

			}
		}

		/// <summary>
		/// Downloads the file via HTTP to the requested location.
		/// </summary>
		/// <returns>The total number of bytes downloaded.</returns>
		public int DownloadPatch()
		{
			return DownloadPatch(null);
		}

		/// <summary>
		/// Downloads the file via HTTP to the requested location.
		/// </summary>
		/// <param name="bw">BackgroundWorker to report progress to.</param>
		/// <returns>The total number of bytes downloaded.</returns>
		public int DownloadPatch(BackgroundWorker bw)
		{
			int bytesdone = 0;
			Stream RStream = null;
			Stream LStream = null;
			WebResponse response = null;

			if (String.IsNullOrEmpty(_PatchFile))
				_PatchFile = Path.Combine(_BaseDir, _UpdateInfo.FileName); 

			try
			{
				WebRequest request = WebRequest.Create(_UpdateInfo.Url);
				if (request != null)
				{
					response = request.GetResponse();
					if (response != null)
					{
						RStream = response.GetResponseStream();
						LStream = File.Create(_PatchFile);
						byte[] buffer = new byte[1024];
						int bytesRead;
						do
						{
							bytesRead = RStream.Read(buffer, 0, buffer.Length);
							LStream.Write(buffer, 0, bytesRead);
							bytesdone += bytesRead;
							int PercentageComplete = (int)Math.Floor((((float)bytesdone / (float)response.ContentLength) * 100) + 0.5);
							if (bw != null && response.ContentLength > 0)
								bw.ReportProgress(PercentageComplete);
						}
						while (bytesRead > 0);
					}
				}
			}
			catch (Exception ex)
			{
				// We don't want error reporting here.
			}
			finally
			{
				if (response != null)
					response.Close();
				if (RStream != null)
					RStream.Close();
				if (LStream != null)
					LStream.Close();
			}

			return bytesdone;
		}

		/// <summary>
		/// Extract update files and copy into AppBase directory.
		/// </summary>
		public void ApplyUpdate()
		{
			Stream temp = null;
			try
			{
				// Load the update
				String name = Path.GetFileNameWithoutExtension(_PatchFile);
				name = String.Format("{0}", name, _PublicKeyToken);
				Assembly update = Assembly.Load(name);

				// Check to see if there is a type in the assembly with a custom extraction routine
				Boolean customCall = false;
				Type[] types = update.GetTypes();
				foreach (Type t in types)
				{
					MethodInfo[] methods = t.GetMethods(BindingFlags.Public | BindingFlags.Static);
					foreach (MethodInfo m in methods)
					{
						if (m.GetParameters().Length == 0 && m.ReturnType == typeof(void))
						{
							// Found one, lets call it
							customCall = true;
							m.Invoke(null, null);
						}
					}
				}

				// Apply the update if a custom call didn't happen
				if (!customCall)
					RenameAndExtractFiles(update);

				//// Add one to the update number for poll
				//xml.NextUpdate = xml.NextUpdate + 1;

			}
			catch (FileLoadException e)
			{
				HandleBrokenUpdate(e);
			}
			catch (FileNotFoundException e)
			{
				HandleBrokenUpdate(e);
			}
			finally
			{
				if (temp != null)
					temp.Close();
			}
		}

		// Re-run the application either in another AppDomain or in another process
		public void RelaunchExe(Boolean inProc)
		{
			// Get assembly that started it all
			Assembly entry = Assembly.GetEntryAssembly();
			// "Intuit" its exe name
			String name = entry.GetName().Name + ".exe";
			if (inProc)
			{
				// Create an AppDomain that shadow copies its files
				AppDomain current = AppDomain.CurrentDomain;
				AppDomainSetup info = current.SetupInformation;
				info.ShadowCopyFiles = true.ToString();
				AppDomain domain = AppDomain.CreateDomain(GetTempName(), current.Evidence, info);

				// Relaunch the application in the new application domain
				// using the same command line arguments
				String[] argsOld = Environment.GetCommandLineArgs();
				String[] argsNew = new String[argsOld.Length - 1];
				Array.Copy(argsOld, 1, argsNew, 0, argsNew.Length);
				domain.ExecuteAssembly(name, entry.Evidence, argsNew);
			}
			else
			{
				// Create a new process and relaunch the application
				Process.Start(name, Environment.CommandLine);
			}
		}

		#endregion

		#region Private Methods

		private void RenameAndExtractFiles(Assembly update)
		{
			// Get resource names from update assembly
			String[] resources = update.GetManifestResourceNames();
			Hashtable renameLog = new Hashtable();
			try
			{
				foreach (String s in resources)
				{
					// If a current file exists with the same name, rename it
					if (File.Exists(s))
					{
						String tempName = GetTempName();
						File.Move(s, tempName);
						renameLog[tempName] = s;
					}
					// Copy the resource out into the new file
					// this does not take into consideration file dates and other similar
					// attributes (but probobly should).
					using (Stream res = update.GetManifestResourceStream(s), file = new FileStream(s, FileMode.CreateNew))
					{
						Int32 pseudoByte;
						while ((pseudoByte = res.ReadByte()) != -1)
						{
							file.WriteByte((Byte)pseudoByte);
						}
					}
				}
				// If we made it this far, it is safe to rename the update assembly
				MakeUpdateTmp();
			}
			catch
			{
				// Unwind failed operation
				foreach (DictionaryEntry d in renameLog)
				{
					String filename = d.Value as String;
					if (File.Exists(filename))
					{
						File.Delete(filename);
					}
					File.Move(d.Key as String, filename);
				}
				throw; // rethrow whatever went wrong
			}
		}

		private void HandleBrokenUpdate(Exception e)
		{
			// Something was wrong with the update file
			// rename it so that it will attempt another download
			MakeUpdateTmp();
			throw new SecurityException("Assembly failed to load.", e);
		}

		private void MakeUpdateTmp()
		{
			File.Move(Path.GetFileName(_PatchFile), GetTempName());
		}

		private void CleanupTemps(Object o)
		{
			// Delete any file with the extension ".update_tempfile"
			String[] files = Directory.GetFiles(_BaseDir, "*.update_tempfile");
			foreach (String s in files)
			{
				try
				{
					File.Delete(s);
				}
				catch (IOException)
				{
					// ignore some expected errors
				}
				catch (UnauthorizedAccessException)
				{

				}
				catch (SecurityException)
				{
				
				}
			}
		}

		// Generate a unique name using a Guid
		private String GetTempName()
		{
			return Guid.NewGuid().ToString() + ".update_tempfile";
		}

		#endregion
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Reflection;

namespace UltraSFV.Core
{
	/// <summary>
	/// Provides instance methods for reading, creating, modifying, and saving SFV/MD5 files. This class cannot be inherited.
	/// </summary>
	public sealed class HashFile
	{
		private Regex CRCMatch = new Regex(@"(?<file>.+)\s+(?<hash>[A-Z0-9]{8})$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		private Regex MD5Match = new Regex(@"(?<hash>[A-Z0-9]{32})[\|\s]+(?<file>.+)$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		private FileInfo _File;
		private ArrayList _Items;

		#region Properties

		private HashType _FileType;
		/// <summary>
		/// Gets the HashType of the file.
		/// </summary>
		public HashType FileType
		{
			get
			{
				return _FileType;
			}
		}

		/// <summary>
		/// Gets the name of the current file.
		/// </summary>
		public string Name
		{
			get
			{
				return _File.Name;
			}
		}

		/// <summary>
		/// Gets a value indicating wheather the file exists.
		/// </summary>
		public bool Exists
		{
			get
			{
				return _File.Exists;
			}
		}

		private string _Comments;
		/// <summary>
		/// Gets or sets the comments for the file.
		/// </summary>
		public string Comments
		{
			get
			{
				return _Comments;
			}
			set
			{
				_Comments = value;
			}
		}

		#endregion

		#region Static Methods

		/// <summary>
		/// Searches a directory for supported hash files and returns a List of the results.
		/// </summary>
		/// <param name="directory">Directory to search.</param>
		/// <returns>A List object containing FileInfo objects for each matched file.</returns>
		public static List<FileInfo> GetHashFiles(DirectoryInfo diirectory)
		{
			List<FileInfo> HashFiles = new List<FileInfo>();
			HashFiles.AddRange(diirectory.GetFiles());
			HashFiles = HashFiles.FindAll(delegate(FileInfo fi) { return IsHashFile(fi); });
			return HashFiles;
		}

		/// <summary>
		/// Indicates wheather a file is a supported hash file type.
		/// </summary>
		/// <param name="file">File to check</param>
		/// <returns>True or false.</returns>
		public static bool IsHashFile(FileInfo file)
		{
			string ext = file.Extension.ToLower();
			return ext == ".sfv" || ext == ".md5" || ext == ".crc";
		}

		/// <summary>
		/// Indicates wheather a file is a supported hash file type.
		/// </summary>
		/// <param name="file">File name or file path to check.</param>
		/// <returns>True or false.
		public static bool IsHashFile(string file)
		{
			string ext = Path.GetExtension(file).ToLower();
			return ext == ".sfv" || ext == ".md5" || ext == ".crc";
		}

		#endregion

		#region Constructor

		public HashFile(string file)
		{
			_File = new FileInfo(file);
			_Items = new ArrayList();
			_Comments = String.Empty;

			switch (Path.GetExtension(file).ToLower())
			{
				case ".sfv":
					_FileType = HashType.CRC;
					break;
				case ".md5":
					_FileType = HashType.MD5;
					break;
				case ".crc":
					_FileType = HashType.CRC;
					break;
			}

			if (_File.Exists)
			{
				Add(LoadFile());
			}
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Attempts to pull a hash for a particular file stores within the HashFile.
		/// </summary>
		/// <param name="fileName">File name to search for.</param>
		/// <returns>A string containing the files hash or String.Empty if the file was not found in this archive.</returns>
		public string GetHash(string fileName)
		{
			foreach (QueueItem qi in _Items)
			{
				if (qi.File.Name == fileName)
				{
					return qi.TestHash;
				}
			}
			return String.Empty;
		}

		/// <summary>
		/// Adds each QueueItem in the ArrayList to the queue. Automatically skips duplicate entries.
		/// </summary>
		/// <param name="item">ArrayList of QueueItems to add.</param>
		public void Add(ArrayList items)
		{
			foreach (QueueItem qi in items)
			{
				Add(qi);
			}
		}

		/// <summary>
		/// Adds each QueueItem in the QueueItem array to the queue. Automatically skips duplicate entries.
		/// </summary>
		/// <param name="item">Array of QueueItems to add.</param>
		public void Add(QueueItem[] items)
		{
			foreach (QueueItem qi in items)
			{
				Add(qi);
			}
		}

		/// <summary>
		/// Adds the QueueItem to the queue. Automatically skips duplicate entries.
		/// </summary>
		/// <param name="item">QueueItem to add.</param>
		public void Add(QueueItem item)
		{
			if (item == null)
				throw new ArgumentNullException("item");

			// De-dupe
			foreach (QueueItem qi in _Items)
			{
				if (qi.File.FullName == item.File.FullName)
				{
					return;
				}
			}

			_Items.Add(item);
		}

		/// <summary>
		/// Saves the current HashFile.
		/// </summary>
		/// <returns>True for success or false for failure.</returns>
		public bool Save()
		{
			// Find the length of the longest file name and length
			int LongestFileLength = 0;
			int LongestFileName = 0;
			foreach (QueueItem qi in _Items)
			{
				if (qi.File.Length.ToString().Length > LongestFileLength)
					LongestFileLength = qi.File.Length.ToString().Length;

				if (qi.File.Name.Length > LongestFileName)
					LongestFileName = qi.File.Name.Length;
			}

			// Setup the comments... must contain generated by label
			//_Comments = _Comments.Trim();
			if (!_Comments.Contains("Generated By UltraSFV"))
			{
				_Comments =
					"Generated By UltraSFV " + String.Format("v{0}", Assembly.GetEntryAssembly().GetName().Version) + " on " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + Environment.NewLine +
					"http://www.ultrasfv.com/" + Environment.NewLine +
					Environment.NewLine +
					_Comments;
			}

			//if (!_Comments.EndsWith(Environment.NewLine))
			//	_Comments += Environment.NewLine;

			// Write the file, or die trying
			try
			{
				using (StreamWriter sw = new StreamWriter(_File.FullName))
				{
					string FileHash = String.Empty;

					// File size and date information
					foreach (QueueItem qi in _Items)
					{
						FileHash = String.Empty;
						if (!String.IsNullOrEmpty(qi.FileHash))
						{
							FileHash = qi.FileHash;
						}
						else if (!String.IsNullOrEmpty(qi.TestHash))
						{
							FileHash = qi.TestHash;
						}

						string FileLine = StringUtilities.AddWhiteSpace(qi.File.Length.ToString(), LongestFileLength, true) + " " + qi.File.LastWriteTime.ToString("HH:mm.ss yyyy-MM-dd") + " " + qi.File.Name;
						
						if (!String.IsNullOrEmpty(FileHash) && !_Comments.Contains(FileLine.Trim()))
						{
							_Comments += FileLine + Environment.NewLine;
						}
					}

					// Comments
					sw.WriteLine("; " + _Comments.Replace(Environment.NewLine, Environment.NewLine + "; ").Trim());

					// Hash information
					foreach (QueueItem qi in _Items)
					{
						FileHash = String.Empty;
						if (!String.IsNullOrEmpty(qi.FileHash))
						{
							FileHash = qi.FileHash;
						}
						else if (!String.IsNullOrEmpty(qi.TestHash))
						{
							FileHash = qi.TestHash;
						}

						if (!String.IsNullOrEmpty(FileHash))
						{
							switch (this.FileType)
							{
								case HashType.CRC:
									sw.WriteLine(StringUtilities.AddWhiteSpace(qi.File.Name, LongestFileName + 4) + FileHash);
									break;

								case HashType.MD5:
									sw.WriteLine(FileHash + "|" + qi.File.Name);
									break;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				return false;
			}

			// Success!
			return true;
		}

		/// <summary>
		/// Reads all valid lines from the SFV file. Returning an array of QueueItems
		/// </summary>
		/// <returns>QueueItem array containing the SFV data.</returns>
		public QueueItem[] ReadAll()
		{
			QueueItem[] items = new QueueItem[_Items.Count];
			for (int i = 0; i < _Items.Count; i++)
			{
				items[i] = (QueueItem)_Items[i];
			}
			return items;
		}

		/// <summary>
		/// Clears all entries from the current file.
		/// </summary>
		public void Clear()
		{
			_Comments = String.Empty;
			_Items.Clear();
		}

		#endregion

		#region Private Methods

		private QueueItem[] LoadFile()
		{
			ArrayList TempList = new ArrayList();

			// Read the file
			bool FoundOne = false;
			using (StreamReader sr = new StreamReader(_File.FullName))
			{
				string FileLine;
				while ((FileLine = sr.ReadLine()) != null)
				{
					Match m = null;
					if (_FileType == HashType.CRC)
					{
						m = CRCMatch.Match(FileLine);
					}
					else if (_FileType == HashType.MD5)
					{
						m = MD5Match.Match(FileLine);
					}

					if (FileLine.StartsWith(";"))
					{
						if (FileLine.StartsWith("; "))
						{
							_Comments += FileLine.Substring(2).TrimEnd(null) + Environment.NewLine;
						}
						else
						{
							_Comments += FileLine.Substring(1).TrimEnd(null) + Environment.NewLine;
						}
						continue;
					}

					if (m != null && m.Success)
					{
						TempList.Add(new QueueItem(new FileInfo(Path.Combine(_File.Directory.FullName, m.Groups["file"].Value.Trim())), m.Groups["hash"].Value, QueueItemAction.TestHash, _FileType));
						FoundOne = true;
					}
				}
			}

			if (_Comments.EndsWith(Environment.NewLine))
			{
			_Comments = _Comments.Substring(0, _Comments.Length - Environment.NewLine.Length);
			}

			if (FoundOne)
			{
				QueueItem[] items = new QueueItem[TempList.Count];
				for (int i = 0; i < TempList.Count; i++)
				{
					items[i] = (QueueItem)TempList[i];
				}
				return items;
			}
			else
			{
				return new QueueItem[0];
			}
		}

		#endregion
	}
}

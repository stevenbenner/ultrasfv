/*
 * ULTRASFV
 * Copyright (c) 2008 Steven Benner.
 * 
 * UltraSFV is a file verification and error detection
 * utility designed for the Microsoft Windows operating
 * system.
 * 
 * This project was built for Visual Studio 2005 based
 * on the Microsoft .NET framework version 2.0.
 */

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Win32;
using UltraSFV.Core;

namespace UltraSFV
{
	static class Program
	{
		public static ProcessManager CoreWorkQueue;
		public static AutoUpdater AutoUpdate;
		public static UltraSFV MainForm;
		public static ErrorLog Logger;
		public static Version AppVersion;

		#region void Main

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			// Initialize the error log
			Logger = new ErrorLog();

			// Handle unhandled exceptions
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
			Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);

			// Setup registry
			CheckRegistryValues();

			// Initialize the AutoUpdater
			AppVersion = Assembly.GetExecutingAssembly().GetName().Version;

			RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\UltraSFV\\Settings");
			Guid guid;
			if (key != null)
			{
				guid = new Guid(key.GetValue("guid", String.Empty).ToString());
			}
			else
			{
				guid = Guid.Empty;
			}
			AutoUpdate = new AutoUpdater(AppVersion, guid);
			key.Close();

			// Setup the visual forms stuff because SingletonApplication creates a form
			// for recieving events, and this cant be done after a form is created
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			// Initialize the main form
			MainForm = new UltraSFV();

			// Singleton application support
			if (Properties.Settings.Default.ReuseWindows)
			{
				if (SingletonApplication.Instance(args, "{C4B3E82C-907B-4525-A71A-5DD97260D94C}"))
				{
					SingletonApplication.SecondInstanceLoaded += new EventHandler<SingletonApplicationEventArgs>(SingletonApplication_SecondInstanceLoaded);
					RunUltraSFV(args);
				}
				else
				{
					Application.Exit();
				}
			}
			else
			{
				RunUltraSFV(args);
			}
		}

		#endregion

		#region Helper Methods

		static void CheckRegistryValues()
		{
			// Check for the user GUID registry value
			RegistryKey SettingsKey = Registry.CurrentUser.OpenSubKey("Software\\UltraSFV\\Settings", true);
			if (SettingsKey == null)
			{
				SettingsKey = Registry.CurrentUser.CreateSubKey("Software\\UltraSFV\\Settings");
			}
			if (SettingsKey.GetValue("guid", null) == null)
			{
				SettingsKey.SetValue("guid", Guid.NewGuid());
			}
			SettingsKey.Close();

			// Check for the statistics registry group
			RegistryKey StatisticsKey = Registry.CurrentUser.OpenSubKey("Software\\UltraSFV\\Statistics");
			if (StatisticsKey == null)
			{
				StatisticsKey = Registry.CurrentUser.CreateSubKey("Software\\UltraSFV\\Statistics");
			}
			StatisticsKey.Close();
		}

		/// <summary>
		/// Create the ProcessManager, setup ApplicationID on first launch, process arguments and run the main form.
		/// </summary>
		/// <param name="args"></param>
		static void RunUltraSFV(string[] args)
		{
			CoreWorkQueue = new ProcessManager();
			if (Properties.Settings.Default.KeepFileLog)
			{
				//CoreWorkQueue.LogFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ProcessedFiles.log");
                CoreWorkQueue.LogFile = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), System.Windows.Forms.Application.ProductName), "ProcessedFiles.log");

				if (Properties.Settings.Default.LogGood)
					Program.CoreWorkQueue.LogLevel |= ProcessLogLevel.Good;
				if (Properties.Settings.Default.LogBad)
					Program.CoreWorkQueue.LogLevel |= ProcessLogLevel.Bad;
				if (Properties.Settings.Default.LogMissing)
					Program.CoreWorkQueue.LogLevel |= ProcessLogLevel.Missing;
				if (Properties.Settings.Default.LogSkipped)
					Program.CoreWorkQueue.LogLevel |= ProcessLogLevel.Skipped;
				if (Properties.Settings.Default.LogLocked)
					Program.CoreWorkQueue.LogLevel |= ProcessLogLevel.Locked;
			}

			ProcessArguments(args);

			Application.Run(MainForm);
		}

		/// <summary>
		/// Second instance event to handle singleton application arguments.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		static void SingletonApplication_SecondInstanceLoaded(object sender, SingletonApplicationEventArgs e)
		{
			if (!Program.CoreWorkQueue.Working)
				Program.CoreWorkQueue.Reset();

			ProcessArguments(e.Args);

			if (!Program.CoreWorkQueue.Working && e.Args.Length > 0 && Program.CoreWorkQueue.RemainingRecords > 0)
			{
				if (e.Args.Length == 1 && HashFile.IsHashFile(e.Args[0]))
					MainForm.CurrentHashFile = new HashFile(e.Args[0]);
				MainForm.ResetInfoAndLaunchWorker();
			}
		}

		/// <summary>
		/// Processes command line argument sent to the application.
		/// </summary>
		/// <param name="args">String array of arguments.</param>
		static void ProcessArguments(string[] args)
		{
			foreach (string arg in args)
			{
				if (!String.IsNullOrEmpty(arg))
				{
					FileInfo fi = new FileInfo(arg);
					if (fi.Exists)
					{
						if (HashFile.IsHashFile(fi))
						{
							HashFile sfv = new HashFile(fi.FullName);
							Program.CoreWorkQueue.Add(sfv.ReadAll());
						}
						else
						{
							Program.CoreWorkQueue.Add(new QueueItem(fi, StringUtilities.FindCRC(fi.Name), QueueItemAction.TestHash, HashType.CRC));
						}
					}
				}
			}
		}

		#endregion

		#region Exception Handling

		static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			try
			{
				Exception ex = (Exception)e.ExceptionObject;
				string LogFile = Logger.LogError(ex);

				MessageBox.Show(
					"The application encountered a fatal error and must exit. This error has been logged and should be reported using the Error Report utility.\n\n" +
						"Error:\n" +
						ex.Message +
						"\n\nStack Trace:\n" +
						ex.StackTrace,
					"Fatal Error",
					MessageBoxButtons.OK,
					MessageBoxIcon.Stop);

				Process proc = new Process();
				proc.EnableRaisingEvents = false;
				proc.StartInfo.FileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UltraSFVError.exe");
				proc.StartInfo.Arguments = LogFile;
				proc.Start();
			}
			finally
			{
				Application.Exit();
			}
		}

		public static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
		{
			DialogResult result = DialogResult.Abort;
			try
			{
				string LogFile = Logger.LogError(e.Exception);

				result = MessageBox.Show(
					"The application encountered a error. This error has been logged and should be reported using the Error Report utility.\n\n" +
						"Error:\n" +
						e.Exception.Message +
						"\n\nStack Trace:\n" +
						e.Exception.StackTrace,
					"Application Error",
					MessageBoxButtons.AbortRetryIgnore,
					MessageBoxIcon.Stop);

				Process proc = new Process();
				proc.EnableRaisingEvents = false;
				proc.StartInfo.FileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UltraSFVError.exe");
				proc.StartInfo.Arguments = LogFile;
				proc.Start();
			}
			finally
			{
				if (result == DialogResult.Abort)
				{
					Application.Exit();
				}
			}
		}

		#endregion
	}
}

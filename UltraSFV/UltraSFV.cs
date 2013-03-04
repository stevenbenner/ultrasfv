using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Media;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Win32;
using UltraSFV.Core;

namespace UltraSFV
{
	public partial class UltraSFV : Form
	{
		private string DefaultToolStripText = "Welcome to UltraSFV - Ultra Simple File Verification";

		#region Properties

		private HashFile _CurrentHashFile = null;
		public HashFile CurrentHashFile
		{
			get
			{
				return _CurrentHashFile;
			}
			set
			{
				_CurrentHashFile = value;
				if (value != null)
				{
					this.Text = "UltraSFV - " + value.Name;
					viewCommentsToolStripMenuItem.Enabled = true;
				}
				else
				{
					this.Text = "UltraSFV";
					viewCommentsToolStripMenuItem.Enabled = false;
				}
			}
		}

		#endregion

		#region Constructor

		public UltraSFV()
		{
			InitializeComponent();
		}

		#endregion

		#region Form Events

		private void UltraSFV_Load(object sender, EventArgs e)
		{
			toolStripStatusLabel1.Text = DefaultToolStripText;
			LoadUserSettings();
			if (Properties.Settings.Default.CheckForUpdates)
				backgroundWorker1.RunWorkerAsync();

			// Bring the main window to the front
			this.TopMost = true;
			this.Show();
			this.TopMost = Properties.Settings.Default.AlwaysOnTop;
		}

		private void UltraSFV_Shown(object sender, EventArgs e)
		{
			if (Program.CoreWorkQueue.RemainingRecords > 0)
				ResetInfoAndLaunchWorker();
		}

		private void UltraSFV_Activated(object sender, EventArgs e)
		{
			if (timer1.Enabled)
				timer1.Stop();
		}

		private void UltraSFV_FormClosed(object sender, FormClosedEventArgs e)
		{
			SaveUserSettings();
		}

		#endregion

		#region List View Events

		private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (Properties.Settings.Default.DoubleClickToOpen)
			{
				ListViewItem item = listView1.GetItemAt(e.X, e.Y);
				QueueItem qi = item.Tag as QueueItem;
				Help.ShowHelp(this, qi.File.FullName);
			}
		}

		#endregion

		#region Menu Items

		#region File Menu

		private void sFVToolStripMenuItem_Click(object sender, EventArgs e)
		{
			NewFile(HashType.CRC);
		}

		private void mD5ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			NewFile(HashType.MD5);
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			openFileDialog1.Title = "Select the file you want to process";
			openFileDialog1.Filter = "Hash Files (*.sfv, *.md5)|*.sfv;*.md5|SFV File (*.sfv)|*.sfv|MD5 File (*.md5)|*.md5";

			if (openFileDialog1.ShowDialog() != DialogResult.Cancel)
			{
				HashFile TargetFile = new HashFile(openFileDialog1.FileName);

				if (TargetFile.Exists)
				{
					QueueItem[] HashItems = TargetFile.ReadAll();

					if (HashItems.Length > 0)
					{
						Program.CoreWorkQueue.Reset();
						Program.CoreWorkQueue.Add(HashItems);
						CurrentHashFile = TargetFile;
						ResetInfoAndLaunchWorker();
					}
					else
					{
						MessageBox.Show("The file does not appear to contain any file/checksum pairs.", "Unable to parse file", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					}
				}
			}
		}

		private void viewLogToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (File.Exists(Program.CoreWorkQueue.LogFile))
			{
				LogViewer frm = new LogViewer();
				frm.ShowDialog();
				frm.Dispose();
			}
			else
			{
				MessageBox.Show("No log file exists.\n\nTo enable logging go to Tools->Options and select the logging options you would like.", "No log file", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		private void quitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		#endregion

		#region Hash Check Menu

		private void filesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			openFileDialog1.Title = "Select the files you want to check";
			openFileDialog1.Filter = "All Files|*.*";

			if (openFileDialog1.ShowDialog() != DialogResult.Cancel)
			{
				Program.CoreWorkQueue.Reset();
				ScanHashFilesAndAdd(openFileDialog1.FileNames);
				if (openFileDialog1.FileNames.Length == 1 && HashFile.IsHashFile(openFileDialog1.FileNames[0]))
				{
					CurrentHashFile = new HashFile(openFileDialog1.FileNames[0]);
				}
				else
				{
					CurrentHashFile = null;
				}
				ResetInfoAndLaunchWorker();
			}
		}

		private void directoryToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (folderBrowserDialog1.ShowDialog() != DialogResult.Cancel)
			{
				DirectoryInfo dir = new DirectoryInfo(folderBrowserDialog1.SelectedPath);

				if (dir.Exists)
				{
					FileInfo[] DirectoryFiles = dir.GetFiles();
					List<FileInfo> HashFiles = HashFile.GetHashFiles(dir);

					Program.CoreWorkQueue.Reset();

					// Load hash files first
					foreach (FileInfo file in HashFiles)
					{
						HashFile hf = new HashFile(file.FullName);
						Program.CoreWorkQueue.Add(hf.ReadAll());
					}

					// Add all the files in the base directory
					foreach (FileInfo TargetFile in DirectoryFiles)
					{
						if (!HashFile.IsHashFile(TargetFile))
						{
							Program.CoreWorkQueue.Add(new QueueItem(TargetFile, StringUtilities.FindCRC(TargetFile.Name), QueueItemAction.TestHash, HashType.CRC));
						}
					}

					// Recursive grab
					RecursiveDirectoryGrab(dir.FullName);

					if (DirectoryFiles.Length == 1 && HashFile.IsHashFile(DirectoryFiles[0]))
					{
						CurrentHashFile = new HashFile(DirectoryFiles[0].Name);
					}
					else
					{
						CurrentHashFile = null;
					}
					ResetInfoAndLaunchWorker();
				}
				else
				{
					MessageBox.Show("Target directory does not exist!", "Directory does not exist", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void manualCheckToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ManualCheck frm = new ManualCheck();
			if (frm.ShowDialog(this) == DialogResult.OK)
			{
				Program.CoreWorkQueue.Reset();
				Program.CoreWorkQueue.Add(new QueueItem(new FileInfo(frm.FileName), frm.HashCode, QueueItemAction.TestHash, frm.HashAlgorithm));
				ResetInfoAndLaunchWorker();
			}
			frm.Dispose();
		}

		#endregion

		#region Tools Menu

		private void addCRCToNamesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			openFileDialog1.Title = "Select the files you append CRCs to";
			openFileDialog1.Filter = "All Files|*.*";

			if (openFileDialog1.ShowDialog() != DialogResult.Cancel)
			{
				// Pre-sort file list
				Array.Sort(openFileDialog1.FileNames);

				// Add the files to the work queue
				Program.CoreWorkQueue.Reset();
				foreach (string File in openFileDialog1.FileNames)
				{
					Program.CoreWorkQueue.Add(new QueueItem(new FileInfo(File), QueueItemAction.AppendHash, HashType.CRC));
				}
				CurrentHashFile = null;
				ResetInfoAndLaunchWorker();
			}
		}

		private void sFVFromNamesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Form1 frm = new Form1();
			frm.Show();
		}

		private void viewCommentsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Comments frm = new Comments(CurrentHashFile);
			frm.ShowDialog();
			frm.Dispose();
		}

		private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveUserSettings();
			Options frm = new Options(this);
			if (frm.ShowDialog() == DialogResult.OK)
			{
				LoadUserSettings();
			}
			frm.Dispose();
		}

		private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Updater frm = new Updater();
			frm.ShowDialog();
			frm.Dispose();
		}

		#endregion

		#region Help Menu

		private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			string HelpFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UltraSFV.chm");
			FileInfo fi = new FileInfo(HelpFilePath);

			if (fi.Exists)
			{
				Help.ShowHelp(this, HelpFilePath);
			}
			else
			{
				string SubText;
				if (Utilities.IsWindowsVista())
				{
					SubText = "You can do this from the \"Programs and Features\" tool in your control panel.";
				}
				else
				{
					SubText = "You can do this from the \"Add or Remove Programs\" tool in your control panel.";
				}

				MessageBox.Show("Could not locate the UltraSFV help file!\n\n" +
					HelpFilePath +
					"\n\nIf you deleted that file you will have to Repair or Reinstall the application. " + SubText,
					"File Not Found",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
			}
		}

		private void supportToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Help.ShowHelp(this, "http://www.ultrasfv.com/support/");
		}

		private void webSiteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Help.ShowHelp(this, "http://www.ultrasfv.com/");
		}

		private void licenseAgreementToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string LicenseFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UltraSFV EULA.rtf");
			FileInfo fi = new FileInfo(LicenseFilePath);

			if (fi.Exists)
			{
				LicenseAgreement frm = new LicenseAgreement();
				frm.ShowDialog();
				frm.Dispose();
			}
			else
			{
				string SubText;
				if (Utilities.IsWindowsVista())
				{
					SubText = "You can do this from the \"Programs and Features\" tool in your control panel.";
				}
				else
				{
					SubText = "You can do this from the \"Add or Remove Programs\" tool in your control panel.";
				}

				MessageBox.Show("Could not locate the UltraSFV license agreement file!\n\n" +
					LicenseFilePath +
					"\n\nIf you deleted that file you will have to Repair or Reinstall the application. " + SubText,
					"File Not Found",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
			}
		}

		private void aboutUltraSFVToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AboutBox frm = new AboutBox();
			frm.ShowDialog(this);
			frm.Dispose();
		}

		#endregion

		#region Menu Item Mouseover Tips

		private void menuItem_MouseLeave(object sender, EventArgs e)
		{
			toolStripStatusLabel1.Text = DefaultToolStripText;
		}

		#region File Menu

		private void newToolStripMenuItem_MouseEnter(object sender, EventArgs e)
		{
			toolStripStatusLabel1.Text = "Create a new SFV file for all files in a directory.";
		}

		private void openToolStripMenuItem_MouseEnter(object sender, EventArgs e)
		{
			toolStripStatusLabel1.Text = "Open an SFV and attempt to verify the files.";
		}

		private void viewLogToolStripMenuItem_MouseEnter(object sender, EventArgs e)
		{
			toolStripStatusLabel1.Text = "View the file check log.";
		}

		private void quitToolStripMenuItem_MouseEnter(object sender, EventArgs e)
		{
			toolStripStatusLabel1.Text = "Exit UltraSFV.";
		}

		#endregion

		#region CRC Check Menu

		private void directoryToolStripMenuItem_MouseEnter(object sender, EventArgs e)
		{
			toolStripStatusLabel1.Text = "Attempt to CRC check all files in a directory.";
		}

		private void fileToolStripMenuItem1_MouseEnter(object sender, EventArgs e)
		{
			toolStripStatusLabel1.Text = "Attempt to CRC check a single file.";
		}

		private void manualCheckToolStripMenuItem_MouseEnter(object sender, EventArgs e)
		{
			toolStripStatusLabel1.Text = "Simple copy/paste form to check a single file.";
		}

		#endregion

		#region Tools Menu

		private void sFVFromNamesToolStripMenuItem_MouseEnter(object sender, EventArgs e)
		{
			toolStripStatusLabel1.Text = "Attempt to create an SFV file with CRCs in their names.";
		}

		private void addCRCToNamesToolStripMenuItem_MouseEnter(object sender, EventArgs e)
		{
			toolStripStatusLabel1.Text = "Append CRC checksum information to the name of all files in a directory.";
		}

		private void viewCommentsToolStripMenuItem_MouseEnter(object sender, EventArgs e)
		{
			toolStripStatusLabel1.Text = "View or edit comments in this SFV file.";
		}

		private void optionsToolStripMenuItem_MouseEnter(object sender, EventArgs e)
		{
			toolStripStatusLabel1.Text = "UltraSFV options, configuration and preferences.";
		}

		private void checkForUpdatesToolStripMenuItem_MouseEnter(object sender, EventArgs e)
		{
			toolStripStatusLabel1.Text = "Check for UltraSFV software updates.";
		}

		#endregion

		#region Help Menu

		private void helpToolStripMenuItem1_MouseEnter(object sender, EventArgs e)
		{
			toolStripStatusLabel1.Text = "General UltraSFV help topics.";
		}

		private void supportToolStripMenuItem_MouseEnter(object sender, EventArgs e)
		{
			toolStripStatusLabel1.Text = "UltraSFV support is avaliable through the web site.";
		}

		private void webSiteToolStripMenuItem_MouseEnter(object sender, EventArgs e)
		{
			toolStripStatusLabel1.Text = "Go to the UltraSFV web site (www.UltraSFV.com).";
		}

		private void licenseAgreementToolStripMenuItem_MouseEnter(object sender, EventArgs e)
		{
			toolStripStatusLabel1.Text = "View the UltraSFV license agreement.";
		}

		private void aboutUltraSFVToolStripMenuItem_MouseEnter(object sender, EventArgs e)
		{
			toolStripStatusLabel1.Text = "About UltraSFV.";
		}

		#endregion

		#endregion

		#endregion

		#region Drag-Drop support

		private void listView1_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				e.Effect = DragDropEffects.All;
			}
			else
			{
				e.Effect = DragDropEffects.None;
			}
		}

		private void listView1_DragDrop(object sender, DragEventArgs e)
		{
			string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);

			Program.CoreWorkQueue.Reset();

			// Load the files
			ScanHashFilesAndAdd(s);

			if (Program.CoreWorkQueue.RemainingRecords > 0 && s.Length > 0)
			{
				if (s.Length == 1 && HashFile.IsHashFile(s[0]))
				{
					CurrentHashFile = new HashFile(s[0]);
				}
				//ResetInfoAndLaunchWorker();
				timer1.Start();
			}
		}

		private void timer2_Tick(object sender, EventArgs e)
		{
			// Delay processing hack for drag and drop support.
			// TODO: Find a better way!
			// The exporer process is not released until the drag drop process is finished.
			// So when we load the WorkingDialog in the same process the explorer window
			// that the user was viewing will be locked up until the processing is complete.
			// This timer lets us release that process while still launching the modal
			timer1.Stop();
			ResetInfoAndLaunchWorker();
		}

		#endregion

		#region User Settings Support

		private void LoadUserSettings()
		{
			// Window size and position
			if (Properties.Settings.Default.RememberWindoLocation)
			{
				this.Location = Properties.Settings.Default.MainWindowLocation;
			}
			this.Size = Properties.Settings.Default.MainWindowSize;
			this.WindowState = Properties.Settings.Default.MainWindowState;
			this.TopMost = Properties.Settings.Default.AlwaysOnTop;

			// Column widths
			this.columnName.Width = Properties.Settings.Default.Column1Width;
			this.columnFileHash.Width = Properties.Settings.Default.Column2Width;
			this.columnFileSize.Width = Properties.Settings.Default.Column3Width;
			this.columnStatus.Width = Properties.Settings.Default.Column4Width;
			this.columnPath.Width = Properties.Settings.Default.Column5Width;

			this.listView1.GridLines = Properties.Settings.Default.ListShowGridLines;
			this.listView1.FullRowSelect = Properties.Settings.Default.ListFullRowSelect;

			// Processing Log
			if (Properties.Settings.Default.KeepFileLog)
			{
				//Program.CoreWorkQueue.LogFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ProcessedFiles.log");
                Program.CoreWorkQueue.LogFile = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), System.Windows.Forms.Application.ProductName), "ProcessedFiles.log");

				Program.CoreWorkQueue.LogLevel = ProcessLogLevel.None;

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


			UpdateColumnsFromSettings();
		}

		private void SaveUserSettings()
		{
			// Window size and position
			Properties.Settings.Default.MainWindowLocation = this.Location;
			Properties.Settings.Default.MainWindowSize = this.Size;
			Properties.Settings.Default.MainWindowState = this.WindowState;

			// Column widths
			Properties.Settings.Default.Column1Width = this.columnName.Width;
			Properties.Settings.Default.Column2Width = this.columnFileHash.Width;
			Properties.Settings.Default.Column3Width = this.columnFileSize.Width;
			Properties.Settings.Default.Column4Width = this.columnStatus.Width;
			Properties.Settings.Default.Column5Width = this.columnPath.Width;

			Properties.Settings.Default.Save();
		}

		public void UpdateColumnsFromSettings()
		{
			int index = 1;

			// Column 2 (CRC)
			if (Properties.Settings.Default.ShowColumn2)
			{
				ToggleColumn(columnFileHash, index, true);
				index++;
			}
			else
			{
				ToggleColumn(columnFileHash, 1, false);
			}

			// Column 3 (File Size)
			if (Properties.Settings.Default.ShowColumn3)
			{
				ToggleColumn(columnFileSize, index, true);
				index++;
			}
			else
			{
				ToggleColumn(columnFileSize, 2, false);
			}

			// Column 4 (Status)
			if (Properties.Settings.Default.ShowColumn4)
			{
				ToggleColumn(columnStatus, index, true);
				index++;
			}
			else
			{
				ToggleColumn(columnStatus, 3, false);
			}

			// Column 5 (Path)
			if (Properties.Settings.Default.ShowColumn5)
			{
				ToggleColumn(columnPath, index, true);
				index++;
			}
			else
			{
				ToggleColumn(columnPath, 4, false);
			}

			ReloadCurrentData();
		}

		private void ToggleColumn(ColumnHeader column, int index, bool visible)
		{
			if (visible && !this.listView1.Columns.Contains(column))
			{
				this.listView1.Columns.Insert(index, column);
			}
			else if (!visible && this.listView1.Columns.Contains(column))
			{
				this.listView1.Columns.Remove(column);
			}
		}

		#endregion

		#region Auto Updater

		private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				Program.AutoUpdate.GetUpdateInfo();
			}
			catch (Exception)
			{

			}
		}

		private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (Program.AutoUpdate.ServerVersion != new Version(0, 0, 0, 0))
			{
				if (Program.AppVersion < Program.AutoUpdate.ServerVersion)
				{
					if (MessageBox.Show("There is an UltraSFV software update avaliable (v " + Program.AutoUpdate.ServerVersion.ToString() + " / " + Program.AutoUpdate.UpdateInfo.FileSize + ").\n\nWould you like to download it now?", "Update Avaliable", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
					{
						Updater frm = new Updater(true);
						frm.ShowDialog();
						frm.Dispose();
					}
				}
			}
		}

		#endregion

		#region Helper Methods

		#region File Loaders

		private void ScanHashFilesAndAdd(string[] fileNames)
		{
			if (fileNames.Length == 0)
				return;

			// Pre-sort file list
			Array.Sort(fileNames);

			// Load all hash files in this directory
			List<FileInfo> HashFileList = HashFile.GetHashFiles(new DirectoryInfo(Path.GetDirectoryName(fileNames[0])));
			HashFile[] HashFiles = new HashFile[HashFileList.Count];
			for (int i = 0; i < HashFileList.Count; i++)
			{
				HashFiles[i] = new HashFile(HashFileList[i].FullName);
			}

			// Add the contents of any selected hash files to the work queue
			foreach (string FileName in fileNames)
			{
				if (HashFile.IsHashFile(FileName))
				{
					HashFile hf = new HashFile(FileName);
					Program.CoreWorkQueue.Add(hf.ReadAll());
				}
			}

			// Add the selected files to the work queue
			foreach (string FileName in fileNames)
			{
				// Check the SFV files in the directory for information on this file
				bool FoundCRCInSFV = false;
				foreach (HashFile sfv in HashFiles)
				{
					string FileHash = sfv.GetHash(Path.GetFileName(FileName));
					if (!String.IsNullOrEmpty(FileHash))
					{
						Program.CoreWorkQueue.Add(new QueueItem(new FileInfo(FileName), FileHash, QueueItemAction.TestHash, HashType.CRC));
						FoundCRCInSFV = true;
						break;
					}
				}

				// If there was no information found in the SFVs then try to parse the file name
				if (!FoundCRCInSFV)
				{
					// Only add hash files if they have an embedded hash... any other file will be added simply because it's not a hash file
					string EmbeddedCRC = StringUtilities.FindCRC(FileName);
					if (!String.IsNullOrEmpty(EmbeddedCRC) || !HashFile.IsHashFile(FileName))
					{
						Program.CoreWorkQueue.Add(new QueueItem(new FileInfo(FileName), EmbeddedCRC, QueueItemAction.TestHash, HashType.CRC));
					}
				}
			}
		}

		private void RecursiveDirectoryGrab(string directory)
		{
			DirectoryInfo RootDirectory = new DirectoryInfo(directory);
			foreach (DirectoryInfo dir in RootDirectory.GetDirectories())
			{
				// Load hash files first
				List<FileInfo> HashFiles = HashFile.GetHashFiles(dir);
				foreach (FileInfo file in HashFiles)
				{
					HashFile hf = new HashFile(file.FullName);
					Program.CoreWorkQueue.Add(hf.ReadAll());
				}

				foreach (FileInfo TargetFile in dir.GetFiles())
				{
					if (!HashFile.IsHashFile(TargetFile))
					{
						Program.CoreWorkQueue.Add(new QueueItem(TargetFile, StringUtilities.FindCRC(TargetFile.Name), QueueItemAction.TestHash, HashType.CRC));
					}
				}

				RecursiveDirectoryGrab(dir.FullName);
			}
		}

		#endregion

		#region ListView Data Loaders

		public void LoadResults()
		{
			QueueItem qi;
			while ((qi = Program.CoreWorkQueue.GetResult()) != null)
			{
				AddQueueItemToListView(qi);
				if (qi.Action == QueueItemAction.TestHash)
					DefaultToolStripText = String.Format("{0} Files Processed ({1} Good / {2} Bad / {3} Missing / {4} Skipped)", Program.CoreWorkQueue.CompletedRecords, Program.CoreWorkQueue.GoodRecords, Program.CoreWorkQueue.BadRecords, Program.CoreWorkQueue.MissingFiles, Program.CoreWorkQueue.SkippedRecords);
				else
					DefaultToolStripText = String.Format("{0} Files Processed", Program.CoreWorkQueue.CompletedRecords);
			}
			toolStripStatusLabel1.Text = DefaultToolStripText;
			if (Program.CoreWorkQueue.BadRecords > 0)
			{
				toolStripStatusLabelStatus.Image = Properties.Resources.stop;
				toolStripStatusLabelStatus.ToolTipText = String.Format("There were {0} bad files found.", Program.CoreWorkQueue.BadRecords);
			}
			else if (Program.CoreWorkQueue.MissingFiles > 0)
			{
				toolStripStatusLabelStatus.Image = Properties.Resources.slow;
				toolStripStatusLabelStatus.ToolTipText = String.Format("There were {0} missing files.", Program.CoreWorkQueue.MissingFiles);
			}
		}

		private void AddQueueItemToListView(QueueItem qi)
		{
			string Status;
			int ImageIndex;

			switch (qi.Results)
			{
				case QueueItemResult.HashMatch:
					Status = "File OK";
					ImageIndex = 0;
					break;
				case QueueItemResult.HashMisMatch:
					Status = "File Corrupt";
					ImageIndex = 1;
					if (Properties.Settings.Default.EnableSounds)
					{
						SystemSounds.Exclamation.Play();
					}
					if (Properties.Settings.Default.RenameBadFiles && qi.File.Extension.ToLower() != ".corrupt")
					{
						if (qi.File.Exists)
						{
							qi.File.MoveTo(qi.File.FullName + ".corrupt");
						}
						Status += " (File Renamed)";
					}
					if (Properties.Settings.Default.DeleteBadFiles)
					{
						if (qi.File.Exists)
						{
							if (Properties.Settings.Default.UseRecycleBinWhenDeleting)
							{
								RecycleBin.SendToRecycleBin(qi.File.FullName + "\0");
							}
							else
							{
								qi.File.Delete();
							}
						}
						Status += " (File Deleted)";
					}
					break;
				case QueueItemResult.NoTeshHash:
					Status = "No Checksum Found";
					ImageIndex = 2;
					break;
				case QueueItemResult.HashGenerated:
					Status = qi.Type.ToString() + " Generated";
					ImageIndex = 3;
					break;
				case QueueItemResult.HashAppended:
					Status = qi.Type.ToString() + " Appended";
					ImageIndex = 3;
					break;
				case QueueItemResult.HashAppendSkipped:
					Status = qi.Type.ToString() + " already in name";
					ImageIndex = 4;
					break;
				case QueueItemResult.HashAppendFailed:
					Status = "Could not rename file";
					ImageIndex = 4;
					break;
				case QueueItemResult.FileNotFound:
					Status = "File Not Found";
					ImageIndex = 4;
					break;
				case QueueItemResult.FileAccessViolation:
					Status = "File Access Violation (Could not open file for read)";
					ImageIndex = 4;
					break;

				default:
					Status = "Unknown";
					ImageIndex = 2;
					break;
			}

			ArrayList TempArray = new ArrayList();

			foreach (ColumnHeader ch in listView1.Columns)
			{
				if (ch.Text == "File Name")
					TempArray.Add(qi.File.Name);
				else if (ch.Text == "File Hash")
					TempArray.Add((!String.IsNullOrEmpty(qi.FileHash) ? qi.FileHash : "n/a"));
				else if (ch.Text == "File Size")
					TempArray.Add((qi.File.Exists ? StringUtilities.GetFileSizeAsString(qi.File.Length) : "n/a"));
				else if (ch.Text == "Status")
					TempArray.Add(Status);
				else if (ch.Text == "Path")
					TempArray.Add(qi.File.Directory.FullName);
			}

			string[] ListItemData = (string[])TempArray.ToArray(typeof(string));
			ListViewItem ListItem = new ListViewItem(ListItemData);
			ListItem.ImageIndex = ImageIndex;
			ListItem.ToolTipText = qi.File.Name + " - " + Status;
			ListItem.Tag = qi;
			if (qi.Results == QueueItemResult.HashMisMatch)
			{
				ListItem.ForeColor = Color.Red;
			}
			this.listView1.Items.Add(ListItem);

			// Alternating row colors on even rows
			if (Properties.Settings.Default.AlternateRowColors && ListItem.Index % 2 != 0)
			{
				ListItem.BackColor = Properties.Settings.Default.AlternateRowColor;
			}

			if (Properties.Settings.Default.AutomaticallyScrollFileList)
			{
				ListItem.EnsureVisible();
			}
		}

		private void ReloadCurrentData()
		{
			listView1.Items.Clear();
			foreach (QueueItem qi in Program.CoreWorkQueue.ResultsList)
				AddQueueItemToListView(qi);
		}

		#endregion

		#region Launch Worker

		public void ResetInfoAndLaunchWorker()
		{
			// Reset the list and main window information
			listView1.Items.Clear();
			toolStripStatusLabel1.Text = "Processing files...";
			toolStripStatusLabelStatus.Image = Properties.Resources.go;
			toolStripStatusLabelStatus.ToolTipText = "All tested files are OK.";

			// Operation begins now
			DateTime StartTime = DateTime.Now;

			// Show the dialog and do the processing
			WorkingDialog WaitDialog = new WorkingDialog(this);
			WaitDialog.ShowDialog(this);
			WaitDialog.Dispose();

			// Beep when done if processing takes 30 seconds or more
			TimeSpan WorkingTime = DateTime.Now.Subtract(StartTime);
			if (WorkingTime.TotalSeconds >= 30)
			{
				if (Properties.Settings.Default.EnableSounds)
				{
					SystemSounds.Beep.Play();
				}
			}

			// Auto-close support
			if (Program.CoreWorkQueue.ResultsList.Count > 0 && (Properties.Settings.Default.CloseWhenCheckingFinished || Properties.Settings.Default.CloseWhenCreatingFinished))
			{
				QueueItem FirstResult = Program.CoreWorkQueue.ResultsList[0] as QueueItem;
				if (FirstResult.Action == QueueItemAction.TestHash)
				{
					if (Properties.Settings.Default.CloseWhenCheckingFinished)
					{
						if (Properties.Settings.Default.CloseOnlyWhenAllGood)
						{
							if (Program.CoreWorkQueue.GoodRecords == Program.CoreWorkQueue.TotalRecords)
							{
								Application.Exit();
							}
						}
						else
						{
							Application.Exit();
						}
					}
				}
				else if (FirstResult.Action == QueueItemAction.GenerateHash || FirstResult.Action == QueueItemAction.AppendHash)
				{
					if (Properties.Settings.Default.CloseWhenCreatingFinished)
					{
						Application.Exit();
					}
				}
			}

			// Bring the main window to the front
			this.TopMost = true;
			this.Show();
			this.TopMost = Properties.Settings.Default.AlwaysOnTop;
		}

		#endregion

		#region New File

		private void NewFile(HashType type)
		{
			string Extension = String.Empty;
			switch (type)
			{
				case HashType.CRC:
					Extension = "sfv";
					break;
				case HashType.MD5:
					Extension = "md5";
					break;
			}

			openFileDialog1.Title = "Select the files you want to create an " + Extension + " for";
			openFileDialog1.Filter = "All Files|*.*";

			if (openFileDialog1.ShowDialog() != DialogResult.Cancel)
			{
				FileInfo fi = new FileInfo(openFileDialog1.FileNames[0]);
				FileInfo NewFile;

				if (Properties.Settings.Default.PromptForFileName)
				{
					FileNameDialog fn = new FileNameDialog(fi.Directory.FullName, fi.Directory.Name + "." + Extension);
					if (fn.ShowDialog() == DialogResult.OK)
					{
						NewFile = new FileInfo(Path.Combine(fi.Directory.FullName, fn.FileName));
						fn.Dispose();
					}
					else
					{
						fn.Dispose();
						return;
					}
				}
				else
				{
					// Check for existing SFV file, warn if replacing
					NewFile = new FileInfo(Path.Combine(fi.Directory.FullName, fi.Directory.Name + "." + Extension));
					if (NewFile.Exists)
					{
						if (MessageBox.Show("The file \"" + fi.Directory.Name + "." + Extension + "\" already exists.\nDo you wish to replace it?", "Overwrite File?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
						{
							FileNameDialog fn = new FileNameDialog(fi.Directory.FullName, fi.Directory.Name + "." + Extension);
							if (fn.ShowDialog() == DialogResult.OK)
							{
								NewFile = new FileInfo(Path.Combine(fi.Directory.FullName, fn.FileName));
								fn.Dispose();
							}
							else
							{
								fn.Dispose();
								return;
							}
						}
					}
				}

				// Pre-sort file list
				Array.Sort(openFileDialog1.FileNames);

				// Add files to work queue
				Program.CoreWorkQueue.Reset();
				foreach (string FileName in openFileDialog1.FileNames)
				{
					Program.CoreWorkQueue.Add(new QueueItem(new FileInfo(FileName), QueueItemAction.GenerateHash, type));
				}
				Program.CoreWorkQueue.OutputFile = NewFile.FullName;
				ResetInfoAndLaunchWorker();
			}
		}

		#endregion

		#endregion}
	}
}

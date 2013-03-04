using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Win32;
using UltraSFV.Core;

namespace UltraSFV
{
	public partial class Updater : Form
	{
		private bool ReadyToDownload = false;

		#region Constructors

		public Updater()
		{
			InitializeComponent();
		}

		public Updater(bool downloadNow)
		{
			InitializeComponent();
			ReadyToDownload = downloadNow;
		}

		#endregion

		#region Form Events

		private void Updater_Load(object sender, EventArgs e)
		{
			if (!ReadyToDownload)
			{
				this.TopMost = Properties.Settings.Default.AlwaysOnTop;
				timer1.Start();
				backgroundWorker1.RunWorkerAsync();
			}
			else
			{
				labelStatus.Text = "Downloading update...";
				progressBar1.Value = 0;
				backgroundWorker2.RunWorkerAsync();
			}
		}

		#endregion

		#region Background Workers

		#region Update Checker

		// backgroundWorker1 - The Update Checker
		private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			//ui = Updater.CheckForUpdates(AppVersion, Registry.CurrentUser.OpenSubKey("Software\\UltraSFV\\Settings").GetValue("guid", "null").ToString());
			Program.AutoUpdate.GetUpdateInfo();
		}

		private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			timer1.Stop();
			progressBar1.Value = progressBar1.Maximum;
			if (Program.AutoUpdate.ServerVersion != new Version(0, 0, 0, 0))
			{
				if (Program.AppVersion < Program.AutoUpdate.ServerVersion)
				{
					labelStatus.Text = "Update Avaliable";
					if (MessageBox.Show("There is an UltraSFV software update avaliable (v " + Program.AutoUpdate.ServerVersion.ToString() + " / " + Program.AutoUpdate.UpdateInfo.FileSize + ").\n\nWould you like to download it now?", "Update Avaliable", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
					{
						labelStatus.Text = "Downloading update...";
						progressBar1.Value = 0;
						backgroundWorker2.RunWorkerAsync();
					}
					else
					{
						this.Close();
					}
				}
				else if (Program.AppVersion == Program.AutoUpdate.ServerVersion)
				{
					labelStatus.Text = "Software is up to date";
					MessageBox.Show("You have the latest version (" + Program.AppVersion.ToString() + ").", "Version Current");
					this.Close();
				}
				else if (Program.AppVersion > Program.AutoUpdate.ServerVersion)
				{
					labelStatus.Text = "Software is up to date";
					MessageBox.Show("You have a newer version (" + Program.AppVersion.ToString() + ").", "Version Current");
					this.Close();
				}
			}
			else
			{
				labelStatus.Text = "Check error.";
				MessageBox.Show("An error has occured. Please try again later.", "Error");
				this.Close();
			}
		}

		#endregion

		#region Update Downloader

		// backgroundWorker2 - The Update Downloader
		private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
		{
			string PatchDirectory = Path.Combine(Application.StartupPath, "Patches");
			BackgroundWorker bw = sender as BackgroundWorker;
			Program.AutoUpdate.DownloadPatch(bw);
		}

		private void backgroundWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			progressBar1.Value = e.ProgressPercentage;
		}

		private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			labelStatus.Text = "Download Complete";
			MessageBox.Show("Download Complete. The application will now apply the update.", "Download Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
			Program.AutoUpdate.ApplyUpdate();
			Program.AutoUpdate.RelaunchExe(true);
		}

		#endregion

		#endregion

		#region Timeout Timer

		private void timer1_Tick(object sender, EventArgs e)
		{
			if (progressBar1.Value == progressBar1.Maximum)
			{
				timer1.Stop();
				MessageBox.Show("The connection to the server timed out. Either your internet connection is not active or the update server is currently down.", "Connection Timeout", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				this.Close();
			}
			else
			{
				progressBar1.Value += 2;
			}
		}

		#endregion
	}
}

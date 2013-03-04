using System;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Win32;
using UltraSFV.Core;

namespace UltraSFV
{
	public partial class WorkingDialog : Form
	{
		private UltraSFV _parent;
		private DateTime StartDate;
		private TimeSpan ts;
		private string _Status = "working";

		#region Constructor

		public WorkingDialog(UltraSFV parent)
		{
			InitializeComponent();
			_parent = parent;
		}

		#endregion

		#region Form Events

		private void WorkingDialog_Load(object sender, EventArgs e)
		{
			this.TopMost = Properties.Settings.Default.AlwaysOnTop;
			StartDate = DateTime.Now;
			timer1.Start();
			backgroundWorker1.RunWorkerAsync();
			SetButtonStatus();
			UpdateStatsLabels();
		}

		private void WorkingDialog_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (_Status == "working")
			{
				e.Cancel = true;
			}
		}

		private void WorkingDialog_FormClosed(object sender, FormClosedEventArgs e)
		{
			UpdateStatisticsInRegistry();
			Properties.Settings.Default.Save();
			if (timer1.Enabled)
				timer1.Stop();
			if (timer2.Enabled)
				timer2.Stop();
			if (backgroundWorker1.IsBusy)
				backgroundWorker1.CancelAsync();
		}

		#endregion

		#region Button Click Events

		private void buttonPause_Click(object sender, EventArgs e)
		{
			if (_Status == "working")
			{
				_Status = "paused";
				backgroundWorker1.CancelAsync();
				this.Text = "UltraSFV - Pausing...";
				buttonPause.Enabled = false;
				buttonStop.Enabled = false;
			}
			else if (_Status == "paused")
			{
				_Status = "working";
				backgroundWorker1.RunWorkerAsync();
				this.Text = "UltraSFV - Processing";
				buttonPause.Text = "Pause";
				buttonStop.Text = "Stop";
				StartDate = DateTime.Now.Subtract(ts);
				timer1.Start();
				timer2.Start();
			}
		}

		private void buttonStop_Click(object sender, EventArgs e)
		{
			if (_Status == "working")
			{
				_Status = "stopped";
				backgroundWorker1.CancelAsync();
				this.Text = "UltraSFV - Stopping...";
				buttonStop.Enabled = false;
				buttonPause.Enabled = false;
			}
			else if (_Status == "paused")
			{
				if (!String.IsNullOrEmpty(Program.CoreWorkQueue.OutputFile))
				{
					MessageBox.Show("The CRC processing has been stopped. No file saved.", "No File Saved", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
				this.Close();
			}
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		#endregion

		#region Background Worker

		private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			BackgroundWorker bw = sender as BackgroundWorker;
			Program.CoreWorkQueue.Start(bw);
			if (bw.CancellationPending)
			{
				e.Cancel = true;
			}
		}

		private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			//SetButtonStatus();
			labelFileName.Text = Program.CoreWorkQueue.CurrentFileName;
			progressBar1.Value = Program.CoreWorkQueue.PercentageComplete;
			labelTotalPercentage.Text = String.Format("{0}%", Program.CoreWorkQueue.PercentageComplete);
			progressBar2.Value = e.ProgressPercentage;
			labelFilePercentage.Text = String.Format("{0}%", e.ProgressPercentage);
		}

		private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			timer1_Tick(null, null);
			timer1.Stop();
			timer2_Tick(null, null);
			timer2.Stop();

			labelFileName.Text = Program.CoreWorkQueue.CurrentFileName;
			progressBar1.Value = Program.CoreWorkQueue.PercentageComplete;
			labelTotalPercentage.Text = String.Format("{0}%", Program.CoreWorkQueue.PercentageComplete);
			progressBar2.Value = 100;
			labelFilePercentage.Text = "100%";

			_parent.LoadResults();

			if (!String.IsNullOrEmpty(Program.CoreWorkQueue.OutputFile) && e.Cancelled && _Status == "stopped")
			{
				MessageBox.Show("The CRC processing has been stopped. No file saved.", "No File Saved", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			else if (!String.IsNullOrEmpty(Program.CoreWorkQueue.OutputFile) && !e.Cancelled)
			{
				HashFile file = Program.CoreWorkQueue.SaveResultsToFile();
				if (file != null)
				{
					_parent.CurrentHashFile = file;
					if (Properties.Settings.Default.AlertWhenFileCreated)
						MessageBox.Show("File Created!\n\n" + Program.CoreWorkQueue.OutputFile, "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				else
				{
					MessageBox.Show("There was an error attempting to save the file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}

			if (_Status == "working")
				_Status = "finished";

			SetButtonStatus();
		}

		#endregion

		#region Timers

		private void timer1_Tick(object sender, EventArgs e)
		{
			ts = DateTime.Now.Subtract(StartDate);
			string sTime = ts.Hours.ToString("00") + ":" + ts.Minutes.ToString("00") + ":" + ts.Seconds.ToString("00");
			labelTime.Text = sTime;
		}

		private void timer2_Tick(object sender, EventArgs e)
		{
			//UpdateStatsLabels();
			ts = DateTime.Now.Subtract(StartDate);
			if (Program.CoreWorkQueue.BytesProcessed > 0)
				labelPerformance.Text = StringUtilities.GetFileSizeAsString((long)(Program.CoreWorkQueue.BytesProcessed / ts.TotalSeconds)) + "/s";
			_parent.LoadResults();
		}

		#endregion

		#region Helper Methods

		private void SetButtonStatus()
		{
			if (_Status == "working")
			{
				if (Program.CoreWorkQueue.RemainingRecords <= 1)
				{
					buttonPause.Enabled = false;
					buttonStop.Enabled = false;
				}
				else
				{
					buttonPause.Enabled = true;
					buttonStop.Enabled = true;
				}
			}
			else if (_Status == "paused")
			{
				this.Text = "UltraSFV - Paused";
				buttonPause.Text = "Resume";
				buttonStop.Text = "Cancel";
				buttonPause.Enabled = true;
				buttonStop.Enabled = true;
			}
			else if (_Status == "stopped")
			{
				this.Text = "UltraSFV - Stopped";
				this.Close();
				buttonPause.Visible = false;
				buttonStop.Visible = false;
			}
			else if (_Status == "finished")
			{
				this.Text = "UltraSFV - Finished";
				this.Close();
				buttonPause.Visible = false;
				buttonStop.Visible = false;
			}
		}

		private void UpdateStatsLabels()
		{
			labelTotal.Text = Program.CoreWorkQueue.TotalRecords.ToString();
			labelChecked.Text = Program.CoreWorkQueue.CompletedRecords.ToString();
			labelRemain.Text = Program.CoreWorkQueue.RemainingRecords.ToString();
		}

		private void UpdateStatisticsInRegistry()
		{
			RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\UltraSFV\\Statistics", true);

			key.SetValue("TotalProcessed", ((int)key.GetValue("TotalProcessed", 0) + Program.CoreWorkQueue.CompletedRecords));
			key.SetValue("TotalGood", ((int)key.GetValue("TotalGood", 0) + Program.CoreWorkQueue.GoodRecords));
			key.SetValue("TotalBad", ((int)key.GetValue("TotalBad", 0) + Program.CoreWorkQueue.BadRecords));
			key.SetValue("TotalSkipped", ((int)key.GetValue("TotalSkipped", 0) + Program.CoreWorkQueue.SkippedRecords));
			key.SetValue("TotalMissing", ((int)key.GetValue("TotalMissing", 0) + Program.CoreWorkQueue.MissingFiles));
			key.SetValue("TotalLocked", ((int)key.GetValue("TotalLocked", 0) + Program.CoreWorkQueue.HashesGenerated));

			long val;
			if (long.TryParse(key.GetValue("TotalBytes", 0).ToString(), out val))
			{
				val = val + Program.CoreWorkQueue.BytesProcessed;
			}
			else
			{
				val = Program.CoreWorkQueue.BytesProcessed;
			}
			key.SetValue("TotalBytes", val);

			if (long.TryParse(key.GetValue("TotalTime", 0).ToString(), out val))
			{
				val = val + ts.Ticks;
			}
			else
			{
				val = ts.Ticks;
			}
			key.SetValue("TotalTime", val);

			key.Close();
		}

		#endregion
	}
}

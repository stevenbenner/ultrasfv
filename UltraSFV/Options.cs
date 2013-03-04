using System;
using System.Windows.Forms;

namespace UltraSFV
{
	public partial class Options : Form
	{
		private UltraSFV _fm;

		#region Constructor

		public Options(UltraSFV parent)
		{
			_fm = parent;
			InitializeComponent();
			ParseUserSettings();
		}

		#endregion

		#region Form Events

		private void Options_Load(object sender, EventArgs e)
		{
			this.TopMost = Properties.Settings.Default.AlwaysOnTop;
			this.checkBoxUseRecycleBin.CheckedChanged += new EventHandler(checkBoxUseRecycleBin_CheckedChanged);
		}

		#endregion

		#region Button Events

		private void buttonOK_Click(object sender, EventArgs e)
		{
			SaveUserSettings();
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		#endregion

		#region CheckBox Events

		private void checkBoxDeleteBadFiles_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxDeleteBadFiles.Checked)
			{
				checkBoxUseRecycleBin.Enabled = true;
			}
			else
			{
				checkBoxUseRecycleBin.Enabled = false;
			}
		}

		private void checkBoxUseRecycleBin_CheckedChanged(object sender, EventArgs e)
		{
			if (!checkBoxUseRecycleBin.Checked)
			{
				if (MessageBox.Show("Warning: Disabling this feature will cause bad files to be permanently deleted!\n\nAre you sure you want to do this?", "Dangerous Option", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
					checkBoxUseRecycleBin.Checked = true;
			}
		}

		private void checkBoxCloseWhenCheckingFinished_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxCloseWhenCheckingFinished.Checked)
			{
				checkBoxCloseOnlyWhenAllGood.Enabled = true;
			}
			else
			{
				checkBoxCloseOnlyWhenAllGood.Enabled = false;
			}
		}

		private void checkBoxKeepFileLog_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxKeepFileLog.Checked)
			{
				checkBoxLogGood.Enabled = true;
				checkBoxLogBad.Enabled = true;
				checkBoxLogMissing.Enabled = true;
				checkBoxLogSkipped.Enabled = true;
				checkBoxLogLocked.Enabled = true;
			}
			else
			{
				checkBoxLogGood.Enabled = false;
				checkBoxLogBad.Enabled = false;
				checkBoxLogMissing.Enabled = false;
				checkBoxLogSkipped.Enabled = false;
				checkBoxLogLocked.Enabled = false;
			}
		}

		#endregion

		#region Parse/Save User Settings

		private void ParseUserSettings()
		{
			// General Tab
			checkBoxRememberWindowLocation.Checked = Properties.Settings.Default.RememberWindoLocation;
			checkBoxReuseWindows.Checked = Properties.Settings.Default.ReuseWindows;
			checkBoxEnableSounds.Checked = Properties.Settings.Default.EnableSounds;

			checkBoxAlwaysOnTop.Checked = Properties.Settings.Default.AlwaysOnTop;
			checkBoxCheckForUpdates.Checked = Properties.Settings.Default.CheckForUpdates;

			checkBoxKeepFileLog.Checked = Properties.Settings.Default.KeepFileLog;
			checkBoxLogGood.Checked = Properties.Settings.Default.LogGood;
			checkBoxLogBad.Checked = Properties.Settings.Default.LogBad;
			checkBoxLogMissing.Checked = Properties.Settings.Default.LogMissing;
			checkBoxLogSkipped.Checked = Properties.Settings.Default.LogSkipped;
			checkBoxLogLocked.Checked = Properties.Settings.Default.LogLocked;

			// File List Tab
			checkBoxShowColumn1.Checked = Properties.Settings.Default.ShowColumn1;
			checkBoxShowColumn2.Checked = Properties.Settings.Default.ShowColumn2;
			checkBoxShowColumn3.Checked = Properties.Settings.Default.ShowColumn3;
			checkBoxShowColumn4.Checked = Properties.Settings.Default.ShowColumn4;
			checkBoxShowColumn5.Checked = Properties.Settings.Default.ShowColumn5;

			checkBoxAutomaticallyScrollFileList.Checked = Properties.Settings.Default.AutomaticallyScrollFileList;
			checkBoxDoubleClickToOpen.Checked = Properties.Settings.Default.DoubleClickToOpen;

			checkBoxFullRowSelect.Checked = Properties.Settings.Default.ListFullRowSelect;
			checkBoxShowGridLines.Checked = Properties.Settings.Default.ListShowGridLines;
			checkBoxAlternateRowColors.Checked = Properties.Settings.Default.AlternateRowColors;
			labelColorSample.BackColor = Properties.Settings.Default.AlternateRowColor;

			// Checking Tab
			checkBoxRenameBadFiles.Checked = Properties.Settings.Default.RenameBadFiles;
			checkBoxDeleteBadFiles.Checked = Properties.Settings.Default.DeleteBadFiles;
			checkBoxUseRecycleBin.Checked = Properties.Settings.Default.UseRecycleBinWhenDeleting;

			checkBoxCloseWhenCheckingFinished.Checked = Properties.Settings.Default.CloseWhenCheckingFinished;
			checkBoxCloseOnlyWhenAllGood.Checked = Properties.Settings.Default.CloseOnlyWhenAllGood;

			// Creating Tab
			checkBoxPromptForFileName.Checked = Properties.Settings.Default.PromptForFileName;
			checkBoxAlertWhenFileCreated.Checked = Properties.Settings.Default.AlertWhenFileCreated;
			checkBoxCloseWhenCreatingFinished.Checked = Properties.Settings.Default.CloseWhenCreatingFinished;
		}

		private void SaveUserSettings()
		{
			// General Tab
			Properties.Settings.Default.RememberWindoLocation = checkBoxRememberWindowLocation.Checked;
			Properties.Settings.Default.ReuseWindows = checkBoxReuseWindows.Checked;
			Properties.Settings.Default.EnableSounds = checkBoxEnableSounds.Checked;

			Properties.Settings.Default.AlwaysOnTop = checkBoxAlwaysOnTop.Checked;
			Properties.Settings.Default.CheckForUpdates = checkBoxCheckForUpdates.Checked;

			Properties.Settings.Default.KeepFileLog = checkBoxKeepFileLog.Checked;
			Properties.Settings.Default.LogGood = checkBoxLogGood.Checked;
			Properties.Settings.Default.LogBad = checkBoxLogBad.Checked;
			Properties.Settings.Default.LogMissing = checkBoxLogMissing.Checked;
			Properties.Settings.Default.LogSkipped = checkBoxLogSkipped.Checked;
			Properties.Settings.Default.LogLocked = checkBoxLogLocked.Checked;

			// File List Tab
			Properties.Settings.Default.ShowColumn1 = checkBoxShowColumn1.Checked;
			Properties.Settings.Default.ShowColumn2 = checkBoxShowColumn2.Checked;
			Properties.Settings.Default.ShowColumn3 = checkBoxShowColumn3.Checked;
			Properties.Settings.Default.ShowColumn4 = checkBoxShowColumn4.Checked;
			Properties.Settings.Default.ShowColumn5 = checkBoxShowColumn5.Checked;

			Properties.Settings.Default.AutomaticallyScrollFileList = checkBoxAutomaticallyScrollFileList.Checked;
			Properties.Settings.Default.DoubleClickToOpen = checkBoxDoubleClickToOpen.Checked;

			Properties.Settings.Default.ListShowGridLines = checkBoxShowGridLines.Checked;
			Properties.Settings.Default.ListFullRowSelect = checkBoxFullRowSelect.Checked;
			Properties.Settings.Default.AlternateRowColors = checkBoxAlternateRowColors.Checked;
			Properties.Settings.Default.AlternateRowColor = labelColorSample.BackColor;

			// Checking Tab
			Properties.Settings.Default.RenameBadFiles = checkBoxRenameBadFiles.Checked;
			Properties.Settings.Default.DeleteBadFiles = checkBoxDeleteBadFiles.Checked;
			Properties.Settings.Default.UseRecycleBinWhenDeleting = checkBoxUseRecycleBin.Checked;

			Properties.Settings.Default.CloseWhenCheckingFinished = checkBoxCloseWhenCheckingFinished.Checked;
			Properties.Settings.Default.CloseOnlyWhenAllGood = checkBoxCloseOnlyWhenAllGood.Checked;

			// Creating Tab
			Properties.Settings.Default.PromptForFileName = checkBoxPromptForFileName.Checked;
			Properties.Settings.Default.AlertWhenFileCreated = checkBoxAlertWhenFileCreated.Checked;
			Properties.Settings.Default.CloseWhenCreatingFinished = checkBoxCloseWhenCreatingFinished.Checked;

			// Save it and update the main form
			Properties.Settings.Default.Save();
			_fm.UpdateColumnsFromSettings();
		}

		#endregion

		private void labelColorSample_Click(object sender, EventArgs e)
		{
			colorDialog1.Color = Properties.Settings.Default.AlternateRowColor;
			if (colorDialog1.ShowDialog() == DialogResult.OK)
			{
				labelColorSample.BackColor = colorDialog1.Color;
			}
		}
	}
}

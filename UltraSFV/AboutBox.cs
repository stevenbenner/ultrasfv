using System;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Win32;
using UltraSFV.Core;

namespace UltraSFV
{
	public partial class AboutBox : Form
	{
		public AboutBox()
		{
			InitializeComponent();

			labelVersion.Text = String.Format("v {0}", Assembly.GetExecutingAssembly().GetName().Version);

			RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\UltraSFV\\Statistics");
			if (key != null)
			{
				if (key.GetValue("TotalProcessed") != null)
					labelTotal.Text = key.GetValue("TotalProcessed").ToString();
				if (key.GetValue("TotalBytes") != null)
					labelBytes.Text = StringUtilities.GetFileSizeAsString(Convert.ToInt64(key.GetValue("TotalBytes")));
				if (key.GetValue("TotalGood") != null)
					labelGood.Text = key.GetValue("TotalGood").ToString();
				if (key.GetValue("TotalBad") != null)
					labelBad.Text = key.GetValue("TotalBad").ToString();
				if (key.GetValue("TotalSkipped") != null)
					labelSkipped.Text = key.GetValue("TotalSkipped").ToString();
				if (key.GetValue("TotalMissing") != null)
					labelMissing.Text = key.GetValue("TotalMissing").ToString();
				if (key.GetValue("TotalLocked") != null)
					labelLocked.Text = key.GetValue("TotalLocked").ToString();

				if (key.GetValue("TotalTime") != null)
				{
					TimeSpan ts = new TimeSpan(Convert.ToInt64(key.GetValue("TotalTime").ToString()));
					labelTime.Text = ts.Hours.ToString("00") + ":" + ts.Minutes.ToString("00") + ":" + ts.Seconds.ToString("00");
				}
				key.Close();
			}
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void linkLabelUrl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Help.ShowHelp(this, "http://www.ultrasfv.com/");
		}
	}
}

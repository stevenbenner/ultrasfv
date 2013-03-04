using System;
using System.Windows.Forms;
using UltraSFV.Core;

namespace UltraSFV
{
	public partial class LogViewer : Form
	{
		public LogViewer()
		{
			InitializeComponent();
			foreach (string LogLine in new TextFileReader(Program.CoreWorkQueue.LogFile))
			{
				textBoxLog.Text += LogLine + Environment.NewLine;
			}
			textBoxLog.Select(0, 0);
		}

		private void LogViewer_Load(object sender, EventArgs e)
		{
		}

		private void buttonDone_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}

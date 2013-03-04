using System;
using System.IO;
using System.Windows.Forms;

namespace UltraSFV
{
	public partial class LicenseAgreement : Form
	{
		public LicenseAgreement()
		{
			InitializeComponent();
		}

		private void LicenseAgreement_Load(object sender, EventArgs e)
		{
			richTextBox1.LoadFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UltraSFV EULA.rtf"));
		}

		private void buttonDone_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}

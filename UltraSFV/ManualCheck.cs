using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using UltraSFV.Core;

namespace UltraSFV
{
	public partial class ManualCheck : Form
	{
		private Regex CRCMatch = new Regex(@"^[A-F0-9]{8}$", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.RightToLeft);
		private Regex MD5Match = new Regex(@"^[A-F0-9]{32}$", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.RightToLeft);

		public string FileName = String.Empty;
		public string HashCode = String.Empty;
		public HashType HashAlgorithm = HashType.Unknown;

		public ManualCheck()
		{
			InitializeComponent();
		}

		private void buttonCheck_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void buttonBrowse_Click(object sender, EventArgs e)
		{
			if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
			{
				textBoxFileName.Text = Path.GetFileName(openFileDialog1.FileName);
				FileName = openFileDialog1.FileName;
				CheckInput();
			}
		}

		private void textBoxChecksum_TextChanged(object sender, EventArgs e)
		{
			if (String.IsNullOrEmpty(textBoxChecksum.Text))
			{
				labelChecksumStatus.Text = "No Checksum Entered";
				labelChecksumStatus.ForeColor = Color.Firebrick;
				HashCode = String.Empty;
				HashAlgorithm = HashType.Unknown;
				CheckInput();
				return;
			}

			string TrimString = textBoxChecksum.Text.Trim().Replace(" ", "");

			Match crc = CRCMatch.Match(TrimString);
			Match md5 = MD5Match.Match(TrimString);

			if (crc.Success)
			{
				labelChecksumStatus.Text = "CRC32 Checksum";
				labelChecksumStatus.ForeColor = Color.ForestGreen;
				HashCode = TrimString;
				HashAlgorithm = HashType.CRC;
				
			}
			else if (md5.Success)
			{
				labelChecksumStatus.Text = "MD5 Hash";
				labelChecksumStatus.ForeColor = Color.ForestGreen;
				HashCode = TrimString;
				HashAlgorithm = HashType.MD5;
			}
			else
			{
				labelChecksumStatus.Text = "Unrecognized Checksum";
				labelChecksumStatus.ForeColor = Color.Firebrick;
				HashCode = String.Empty;
				HashAlgorithm = HashType.Unknown;
			}

			CheckInput();
		}

		private void CheckInput()
		{
			if (!String.IsNullOrEmpty(openFileDialog1.FileName) && HashAlgorithm != HashType.Unknown)
			{
				buttonCheck.Enabled = true;
			}
			else
			{
				buttonCheck.Enabled = false;
			}
		}
	}
}

using System;
using System.IO;
using System.Windows.Forms;

namespace UltraSFV
{
	public partial class FileNameDialog : Form
	{
		private string _BaseDirectory;
		public string FileName
		{
			get
			{
				return textBoxFileName.Text;
			}
		}

		public FileNameDialog(string directory, string defaultFileName)
		{
			InitializeComponent();
			_BaseDirectory = directory;
			textBoxFileName.Text = defaultFileName;
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			if (String.IsNullOrEmpty(textBoxFileName.Text))
			{
				MessageBox.Show("You must enter a name for the file!", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Stop);
				textBoxFileName.Focus();
				return;
			}
			else
			{
				if (textBoxFileName.Text.IndexOf('.') == -1)
				{
					textBoxFileName.Text = textBoxFileName.Text + ".sfv";
				}
				FileInfo fi = new FileInfo(Path.Combine(_BaseDirectory, textBoxFileName.Text));
				if (fi.Exists)
				{
					if (MessageBox.Show("The file \"" + textBoxFileName.Text + "\" already exists.\n\nDo you want to overwrite it?", "File Exists", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
					{
						textBoxFileName.Focus();
						return;
					}
				}

				this.DialogResult = DialogResult.OK;
				this.Close();
			}
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}

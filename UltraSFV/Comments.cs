using System;
using System.Windows.Forms;
using UltraSFV.Core;

namespace UltraSFV
{
	public partial class Comments : Form
	{
		private HashFile sfv;

		#region Contrsuctor

		public Comments(HashFile file)
		{
			InitializeComponent();
			sfv = file;
			if (sfv != null)
				textBoxComments.Text = sfv.Comments;
		}

		#endregion

		#region Form Events

		private void Comments_Load(object sender, EventArgs e)
		{
			this.Location = Properties.Settings.Default.CommentsWindowLocation;
			this.Size = Properties.Settings.Default.CommentsWindowSize;
			this.WindowState = Properties.Settings.Default.CommentsWindowState;
		}

		private void Comments_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (textBoxComments.Text != sfv.Comments)
			{
				DialogResult dr = MessageBox.Show("The comments have been modified. Do you wish to save your changes?", "Save Changes?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

				switch (dr)
				{
					case DialogResult.Yes:
						SaveComments();
						break;
					case DialogResult.No:
						break;
					case DialogResult.Cancel:
						e.Cancel = true;
						break;
				}
			}
		}

		private void Comments_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (this.WindowState == FormWindowState.Normal)
			{
				Properties.Settings.Default.CommentsWindowLocation = this.Location;
				Properties.Settings.Default.CommentsWindowSize = this.Size;
			}
			Properties.Settings.Default.CommentsWindowState = this.WindowState;
			Properties.Settings.Default.Save();
		}

		#endregion

		#region Button Events

		private void buttonSave_Click(object sender, EventArgs e)
		{
			SaveComments();
			this.Close();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		#endregion

		#region Helper Methods

		private void SaveComments()
		{
			sfv.Comments = textBoxComments.Text;
			sfv.Save();
		}

		#endregion
	}
}

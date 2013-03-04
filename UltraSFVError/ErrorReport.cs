using System;
using System.Text;
using System.IO;
using System.Net;
using System.Web;
using System.Windows.Forms;
using Microsoft.Win32;

namespace UltraSFVError
{
	public partial class ErrorReport : Form
	{
		private FileInfo _ErrorFile;

		public ErrorReport(FileInfo errorFile)
		{
			_ErrorFile = errorFile;

			InitializeComponent();

			using (StreamReader reader = File.OpenText(errorFile.FullName))
			{
				textBox2.Text = reader.ReadToEnd();
			}
		}

		private void ErrorReport_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (MessageBox.Show("Are you sure you want to exit without sending the error report?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
				e.Cancel = true;
		}

		private void buttonSend_Click(object sender, EventArgs e)
		{
			RegistryKey SettingsKey = Registry.CurrentUser.OpenSubKey("Software\\UltraSFV\\Settings");

			string data = "guid=" + HttpUtility.UrlEncode(SettingsKey.GetValue("guid", "null").ToString()) +
				"&name=" + HttpUtility.UrlEncode(_ErrorFile.Name) +
				"&desc=" + HttpUtility.UrlEncode(textBox1.Text) +
				"&log=" + HttpUtility.UrlEncode(textBox2.Text);

			string response = HttpPost("http://www.ultrasfv.com/_system/reporterror.php", data);

			if (response.IndexOf("success") > -1)
			{
				MessageBox.Show("Thank you for sending your error report!", "Error Report Sent", MessageBoxButtons.OK, MessageBoxIcon.Information);
				Application.Exit();
			}
			else
			{
				MessageBox.Show("There appears to be an error with the system. Please try again later.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private string HttpPost(string uri, string parameters)
		{
			WebRequest webRequest = WebRequest.Create(uri);

			webRequest.ContentType = "application/x-www-form-urlencoded";
			webRequest.Method = "POST";
			byte[] bytes = Encoding.ASCII.GetBytes(parameters);
			Stream os = null;

			try
			{
				// send the Post
				webRequest.ContentLength = bytes.Length; //Count bytes to send
				os = webRequest.GetRequestStream();
				os.Write(bytes, 0, bytes.Length); //Send it
			}
			catch (WebException ex)
			{
				MessageBox.Show(ex.Message, "HttpPost: Request error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				if (os != null)
				{
					os.Close();
				}
			}

			try
			{
				// get the response
				WebResponse webResponse = webRequest.GetResponse();
				if (webResponse == null)
				{
					return null;
				}
				StreamReader sr = new StreamReader(webResponse.GetResponseStream());
				return sr.ReadToEnd().Trim();
			}
			catch (WebException ex)
			{
				MessageBox.Show(ex.Message, "HttpPost: Response error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			return null;
		}
	}
}

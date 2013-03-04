using System;
using System.IO;
using System.Windows.Forms;

namespace UltraSFVError
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			if (args.Length == 1)
			{
				FileInfo fi = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.Combine("Errors", args[0])));
				if (fi.Exists)
				{
					Application.EnableVisualStyles();
					Application.SetCompatibleTextRenderingDefault(false);
					Application.Run(new ErrorReport(fi));
				}
			}
		}
	}
}

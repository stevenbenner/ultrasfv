using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;

namespace UltraSFV.Core
{
	/// <summary>
	/// Provides a way to ensure that only one instance of an application is loaded and if a second instance tries to load provides
	/// it's arguments to the first instance.
	/// </summary>
	public class SingletonApplication : IDisposable
	{
		#region PInvoke

		[DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
		static extern System.IntPtr FindWindowByCaption(int ZeroOnly, string lpWindowName);
		[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		static extern bool SendNotifyMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

		#endregion

		#region Events

		/// <summary>
		/// Fires when a Second Instance is loaded.
		/// </summary>
		public static event EventHandler<SingletonApplicationEventArgs> SecondInstanceLoaded;

		#endregion

		#region Fields

		SpecialNativeWindow sw;
		static SingletonApplication _instance;
		static Mutex myMutex;
		static string appID;

		#endregion

		#region Constants

		/// <summary>
		/// Custom/Fake message # to wake up the NativeWindow.
		/// </summary>
		const int CMESSAGE = 93956;

		#endregion

		#region Constructor

		/// <summary>
		/// Private constructor, use SingletonApplication.Instance.
		/// </summary>
		SingletonApplication()
		{
		}

		#endregion

		#region Methods

		/// <summary>
		/// The first method that should be called when using SingletonApplication.
		/// </summary>
		/// <param name="args">Pass the startup arguments to this.</param>
		/// <param name="sharedID">Can be anything you choose, must be the same in both instances.</param>
		/// <returns>Returns true if it is the first instance otherwise returns false.</returns>
		public static bool Instance(string[] args, string sharedID)
		{
			appID = sharedID;
			_instance = new SingletonApplication();
			SetArgs(args);
			bool owned = false;
			myMutex = new Mutex(true, sharedID, out owned);
			GC.KeepAlive(myMutex);
			if (owned)
			{
				_instance.sw = new SpecialNativeWindow(sharedID);
				_instance.sw.ApplicationOpened += new EventHandler<EventArgs>(sw_ApplicationOpened);
				GC.KeepAlive(_instance);
				GC.KeepAlive(_instance.sw);
			}
			else
			{
				IntPtr foundWin = FindWindowByCaption(0, sharedID);
				SendNotifyMessage(foundWin, CMESSAGE, IntPtr.Zero, IntPtr.Zero);
			}

			return owned;
		}

		/// <summary>
		/// Fired from SpecialNativeWindow notifying us the a second instance tried to load.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		static void sw_ApplicationOpened(object sender, EventArgs e)
		{
			_instance.OnSecondInstanceLoaded();
		}

		/// <summary>
		/// Fires when we receive sw_ApplicationOpened from SpecialNativeWindow and sends the arguments sent to that second instance
		/// via SingletonApplicationEventArgs.Args for the primary instance to use.
		/// </summary>
		protected void OnSecondInstanceLoaded()
		{
			if (SecondInstanceLoaded != null)
			{
				SecondInstanceLoaded(this, new SingletonApplicationEventArgs(GetArgs()));
			}
		}

		/// <summary>
		/// Used to retrieve the arguments passed to the second instance of the application.
		/// </summary>
		/// <returns>A string[] array containing the arguments.</returns>
		static string[] GetArgs()
		{
			try
			{
				string[] tempString = (string[])Registry.CurrentUser.OpenSubKey(@"Software\UltraSFV\" + appID, false).GetValue("Args", null);
				Registry.CurrentUser.OpenSubKey("Software\\UltraSFV", true).DeleteSubKey(appID);
				return tempString;
			}
			catch
			{
				return null;
			}
		}

		/// <summary>
		/// Used to store arguments in the registry temporarily.
		/// </summary>
		/// <param name="args"></param>
		static void SetArgs(string[] args)
		{
			Registry.CurrentUser.OpenSubKey("Software\\UltraSFV", true).CreateSubKey(appID).SetValue("Args", args);
		}

		#endregion

		#region MessengerWindow

		/// <summary>
		/// NativeWindow to process messages.
		/// </summary>
		class SpecialNativeWindow : NativeWindow
		{
			public event EventHandler<EventArgs> ApplicationOpened;

			private void OnApplicationOpened()
			{
				if (ApplicationOpened != null)
				{
					ApplicationOpened(this, EventArgs.Empty);
				}
			}

			protected override void WndProc(ref Message m)
			{
				switch (m.Msg)
				{
					case CMESSAGE:
						OnApplicationOpened();
						break;
					default:
						break;
				}
				base.WndProc(ref m);
			}

			public SpecialNativeWindow(string ID)
			{
				CreateParams cp = new CreateParams();
				cp.Caption = ID;
				this.CreateHandle(cp);
			}
		}

		#endregion

		#region IDisposable Members

		public void Dispose()
		{
			if (myMutex != null)
			{
				myMutex.ReleaseMutex();
			}

			if (_instance.sw != null)
			{
				_instance.sw.DestroyHandle();
				_instance.sw = null;
			}

			_instance = null;
		}

		#endregion
	}
}

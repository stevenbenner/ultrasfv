using System;
using System.Runtime.InteropServices;

namespace UltraSFV.Core
{
	public enum FLASHWINFOFLAGS
	{
		FLASHW_STOP = 0,
		FLASHW_CAPTION = 0x00000001,
		FLASHW_TRAY = 0x00000002,
		FLASHW_ALL = (FLASHW_CAPTION | FLASHW_TRAY),
		FLASHW_TIMER = 0x00000004,
		FLASHW_TIMERNOFG = 0x0000000C
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct FLASHWINFO
	{
		public UInt32 cbSize;
		public IntPtr hwnd;
		public Int32 dwFlags;
		public UInt32 uCount;
		public Int32 dwTimeout;
	}

	public static class FlashWInfo
	{
		[DllImport("user32.dll")]
		public static extern int FlashWindowEx(ref FLASHWINFO pfwi);

		public static void FlashWindow(IntPtr hwnd)
		{
			FLASHWINFO fw = new FLASHWINFO();
			fw.cbSize = Convert.ToUInt32(Marshal.SizeOf(typeof(FLASHWINFO)));
			fw.hwnd = hwnd;
			fw.dwFlags = (Int32)(FLASHWINFOFLAGS.FLASHW_ALL | FLASHWINFOFLAGS.FLASHW_TIMERNOFG);
			fw.dwTimeout = 0;

			FlashWindowEx(ref fw);
		}
	}
}

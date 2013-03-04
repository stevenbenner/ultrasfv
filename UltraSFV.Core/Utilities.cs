using System;

namespace UltraSFV.Core
{
	public static class Utilities
	{
		public static bool IsWindowsVista()
		{
			bool IsVista = false;

			System.OperatingSystem osInfo = System.Environment.OSVersion;

			switch (osInfo.Platform)
			{
				case System.PlatformID.Win32NT:
					if (osInfo.Version.Major == 6)
						IsVista = true;
					break;
			}

			return IsVista;
		}
	}
}

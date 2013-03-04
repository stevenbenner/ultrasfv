using System;

namespace UltraSFV.Core
{
	[Flags]
	public enum ProcessLogLevel
	{
		None = 0,
		Good = 1,
		Bad = 2,
		Missing = 4,
		Skipped = 8,
		Locked = 16
	}
}

using System;

namespace UltraSFV.Core
{
	/// <summary>
	/// UpdateStatus: Enumerated type used to indicate the status of updates.  
	/// ApplyUpdate means that an update is available to be applied
	/// </summary>
	public enum UpdateStatus
	{
		ContinueExecution,
		ApplyUpdate
	}
}

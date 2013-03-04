using System;

namespace UltraSFV.Core
{
	public enum QueueItemResult
	{
		NotTested,
		NoTeshHash,
		HashMatch,
		HashMisMatch,
		HashGenerated,
		HashAppended,
		HashAppendFailed,
		HashAppendSkipped,
		FileNotFound,
		FileAccessViolation
	}
}

using System;

namespace UltraSFV.Core
{
	/// <summary>
	/// Used to provide an easy way of accessing the received arguments when the SecondInstanceLoaded event fires.
	/// </summary>
	public class SingletonApplicationEventArgs : EventArgs
	{
		public string[] Args;

		public SingletonApplicationEventArgs(string[] args)
		{
			Args = args;
		}
	}
}

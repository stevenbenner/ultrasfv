using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UltraSFV.Core
{
	public class TextFileReader : IEnumerable<string>, IDisposable
	{
		// The inner StreamReader object
		StreamReader sr;

		public TextFileReader(string path)
		{
			sr = new StreamReader(path);
		}

		public void Dispose()
		{
			// close the file stream
			if (sr != null) sr.Close();
		}

		// the IEnumerable interface
		public IEnumerator<string> GetEnumerator()
		{
			while (sr.Peek() != -1)
			{
				yield return sr.ReadLine();
			}
			Dispose();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}

using System;
using System.Collections;

namespace UltraSFV.Core
{
	public class QueueItemCollection : IEnumerable
	{
		private QueueItem[] items;

		public QueueItemCollection()
		{
			items = new QueueItem[0];
		}

		public void Add(QueueItem qi)
		{

		}

		// IEnumerable Interface Implementation:
		//   Declaration of the GetEnumerator() method required by IEnumerable
		public IEnumerator GetEnumerator()
		{
			return new QueueItemCollectionEnumerator(this);
		}

		// Inner class implements IEnumerator interface:
		private class QueueItemCollectionEnumerator : IEnumerator
		{
			private int position = -1;
			private QueueItemCollection qic;

			public QueueItemCollectionEnumerator(QueueItemCollection qic)
			{
				this.qic = qic;
			}

			// Declare the MoveNext method required by IEnumerator:
			public bool MoveNext()
			{
				if (position < qic.items.Length - 1)
				{
					position++;
					return true;
				}
				else
				{
					return false;
				}
			}

			// Declare the Reset method required by IEnumerator:
			public void Reset()
			{
				position = -1;
			}

			// Declare the Current property required by IEnumerator:
			public object Current
			{
				get
				{
					return qic.items[position];
				}
			}
		}
	}
}

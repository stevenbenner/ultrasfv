using System;
using System.IO;

namespace UltraSFV.Core
{
	public class QueueItem
	{
		#region Properties

		private FileInfo _File;
		/// <summary>
		/// Gets the file object to work on.
		/// </summary>
		public FileInfo File
		{
			get
			{
				return _File;
			}
		}

		private HashType _Type;
		/// <summary>
		/// Gets the HashType of the queue item.
		/// </summary>
		public HashType Type
		{
			get
			{
				return _Type;
			}
		}

		private string _FileHash;
		/// <summary>
		/// Gets the file hash of the queue item.
		/// </summary>
		public string FileHash
		{
			get
			{
				if (String.IsNullOrEmpty(_FileHash))
					return _FileHash;

				switch (Type)
				{
					case HashType.CRC:
						return _FileHash.ToUpper();

					case HashType.MD5:
						return _FileHash.ToLower();

					default:
						return _FileHash;
				}
			}
		}

		private string _TestHash;
		/// <summary>
		/// Gets the CRC32 value to test against.
		/// </summary>
		public string TestHash
		{
			get
			{
				if (String.IsNullOrEmpty(_TestHash))
					return _TestHash;

				switch (Type)
				{
					case HashType.CRC:
						return _TestHash.ToUpper();

					case HashType.MD5:
						return _TestHash.ToLower();

					default:
						return _TestHash;
				}
			}
		}

		private QueueItemAction _Action;
		/// <summary>
		/// Gets the action to be performed on the queue item.
		/// </summary>
		public QueueItemAction Action
		{
			get
			{
				return _Action;
			}
		}

		private QueueItemResult _Results;
		/// <summary>
		/// Gets the results of the queue item.
		/// </summary>
		public QueueItemResult Results
		{
			get
			{
				if (_Results == QueueItemResult.NotTested)
				{
					if (this.File.Exists)
					{
						if (this.Action == QueueItemAction.GenerateHash)
						{
							return QueueItemResult.HashGenerated;
						}
						else
						{
							if (String.IsNullOrEmpty(this.TestHash))
							{
								return QueueItemResult.NoTeshHash;
							}
							else if (this.IsValid)
							{
								return QueueItemResult.HashMatch;
							}
							else
							{
								return QueueItemResult.HashMisMatch;
							}
						}
					}
					else
					{
						return QueueItemResult.FileNotFound;
					}
				}
				else
				{
					return _Results;
				}
			}
		}

		/// <summary>
		/// Does the queue item CRC match the test hash?
		/// </summary>
		public bool IsValid
		{
			get
			{
				return (FileHash == TestHash);
			}
		}

		#endregion

		#region Constructors

		public QueueItem(FileInfo file, QueueItemAction requestedAction, HashType type)
		{
			_File = file;
			_Type = type;
			_TestHash = String.Empty;
			_Action = requestedAction;
			_Results = QueueItemResult.NotTested; // this is wrong... do it different
		}

		public QueueItem(FileInfo file, string testHash, QueueItemAction requestedAction, HashType type)
		{
			_File = file;
			_Type = type;
			_TestHash = testHash;
			_Action = requestedAction;
			_Results = QueueItemResult.NotTested;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Sets the result status of the queue item.
		/// </summary>
		/// <param name="result">Result Status.</param>
		public void SetResultStatus(QueueItemResult result)
		{
			_Results = result;
		}

		/// <summary>
		/// Sets the file has of the queue item.
		/// </summary>
		/// <param name="hash"></param>
		public void SetFileHash(string hash)
		{
			_FileHash = hash;
		}

		#endregion
	}
}

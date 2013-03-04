using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using UltraSFV.HashFunctions;

namespace UltraSFV.Core
{
	/// <summary>
	/// Provides queue management and file processing methods for performing file hash computation and verification. This class cannot be inherited.
	/// </summary>
	public sealed class ProcessManager
	{
		private Queue _ItemQueue;
		private Queue _ResultsQueue;
		private bool _RequestReset;

		public string LogFile;

		#region Properties

		private string _CurrentFileName;
		/// <summary>
		/// Name of the file currently being processed.
		/// </summary>
		public string CurrentFileName
		{
			get
			{
				return _CurrentFileName;
			}
		}

		private string _OutputFile;
		/// <summary>
		/// Output file to save the SFV formatted results to.
		/// </summary>
		public string OutputFile
		{
			get
			{
				return _OutputFile;
			}
			set
			{
				_OutputFile = value;
			}
		}

		private int _TotalRecords;
		/// <summary>
		/// Total number of records currently in the queue.
		/// </summary>
		public int TotalRecords
		{
			get
			{
				return _TotalRecords;
			}
		}

		private int _CompletedRecords;
		/// <summary>
		/// Total number of records that have been completed.
		/// </summary>
		public int CompletedRecords
		{
			get
			{
				return _CompletedRecords;
			}
		}

		private int _SkippedRecords;
		/// <summary>
		/// Total number of records skipped because the file didn't exist or no TestHash was provided.
		/// </summary>
		public int SkippedRecords
		{
			get
			{
				return _SkippedRecords;
			}
		}

		/// <summary>
		/// Total number of records remaining to be processed.
		/// </summary>
		public int RemainingRecords
		{
			get
			{
				return _TotalRecords - _CompletedRecords;
			}
		}

		private long _BytesProcessed;
		/// <summary>
		/// Total number of bytes processed (updated on file completion).
		/// </summary>
		public long BytesProcessed
		{
			get
			{
				return _BytesProcessed;
			}
		}

		private int _GoodRecords;
		/// <summary>
		/// Total number of CRC matched records.
		/// </summary>
		public int GoodRecords
		{
			get
			{
				return _GoodRecords;
			}
		}

		private int _BadRecords;
		/// <summary>
		/// Total number of CRC mis-match or failed records.
		/// </summary>
		public int BadRecords
		{
			get
			{
				return _BadRecords;
			}
		}

		private int _MissingFiles;
		/// <summary>
		/// Total number of files that didn't exist.
		/// </summary>
		public int MissingFiles
		{
			get
			{
				return _MissingFiles;
			}
		}

		private int _HashesGenerated;
		/// <summary>
		/// Total number of records that had CRCs created
		/// </summary>
		public int HashesGenerated
		{
			get
			{
				return _HashesGenerated;
			}
		}

		private bool _Working;
		/// <summary>
		/// Is the process manager currently working on the queue?
		/// </summary>
		public bool Working
		{
			get
			{
				return _Working;
			}
		}

		private ArrayList _ResultsList;
		/// <summary>
		/// List of QueueItems that have been processed.
		/// </summary>
		public ArrayList ResultsList
		{
			get
			{
				return _ResultsList;
			}
		}

		/// <summary>
		/// Total percentage of the queue that has been completed.
		/// </summary>
		public int PercentageComplete
		{
			get
			{
				if (_CompletedRecords > 0 && _TotalRecords > 0)
				{
					return (int)(((float)_CompletedRecords / (float)_TotalRecords) * 100);
				}
				else
				{
					return 0;
				}
			}
		}

		private ProcessLogLevel _LogLevel;
		/// <summary>
		/// Logging details.
		/// </summary>
		public ProcessLogLevel LogLevel
		{
			get
			{
				return _LogLevel;
			}
			set
			{
				_LogLevel = value;
			}
		}

		public bool LoggingEnabled
		{
			get
			{
				if (!String.IsNullOrEmpty(LogFile) && _LogLevel != ProcessLogLevel.None)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}

		#endregion

		#region Constructor

		public ProcessManager()
		{
			_ItemQueue = new Queue();
			_ResultsQueue = new Queue();
			_ResultsList = new ArrayList();
			_RequestReset = false;

			_CurrentFileName = String.Empty;
			_OutputFile = String.Empty;
			_TotalRecords = 0;
			_CompletedRecords = 0;
			_SkippedRecords = 0;
			_BytesProcessed = 0;
			_GoodRecords = 0;
			_BadRecords = 0;
			_HashesGenerated = 0;

			_Working = false;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Adds each QueueItem in the QueueItem array to the queue. Automatically skips duplicate entries.
		/// </summary>
		/// <param name="item">Array of QueueItems to add.</param>
		public void Add(QueueItem[] items)
		{
			foreach (QueueItem qi in items)
			{
				Add(qi);
			}
		}

		/// <summary>
		/// Adds the QueueItem to the queue. Automatically skips duplicate entries.
		/// </summary>
		/// <param name="item">QueueItem to add.</param>
		public void Add(QueueItem item)
		{
			if (item == null)
				throw new ArgumentNullException("item");

			// De-dupe
			foreach (QueueItem qi in _ItemQueue)
			{
				if (qi.File.FullName == item.File.FullName)
				{
					return;
				}
			}

			_ItemQueue.Enqueue(item);
			_TotalRecords++;
		}

		/// <summary>
		/// Returns the first completed QueueItem in the completed queue.
		/// </summary>
		/// <returns>The first completed QueueItem in the completed queue.</returns>
		public QueueItem GetResult()
		{
			if (_ResultsQueue.Count > 0)
			{
				return _ResultsQueue.Dequeue() as QueueItem;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Begin processing of the queue.
		/// </summary>
		public void Start()
		{
			Start(null);
		}

		/// <summary>
		/// Begin processing of the queue.
		/// </summary>
		/// <param name="bw">BackgroundWorker to report progress to.</param>
		public void Start(BackgroundWorker bw)
		{
			_Working = true;

			while (_ItemQueue.Count > 0 && !bw.CancellationPending && !_RequestReset)
			{
				QueueItem qi = _ItemQueue.Dequeue() as QueueItem;
				_CurrentFileName = qi.File.Name;

				// To work, or to skip it. That is the question.
				if (!String.IsNullOrEmpty(qi.TestHash) || qi.Action == QueueItemAction.GenerateHash || qi.Action == QueueItemAction.AppendHash)
				{
					// Don't append a hash if the file name already has one
					if (qi.Action == QueueItemAction.AppendHash &&
						(
							(qi.Type == HashType.CRC && !String.IsNullOrEmpty(StringUtilities.FindCRC(qi.File.Name))) ||
							(qi.Type == HashType.MD5 && !String.IsNullOrEmpty(StringUtilities.FindMD5(qi.File.Name)))
						))
					{
						qi.SetResultStatus(QueueItemResult.HashAppendSkipped);
					}
					else
					{
						// We have everything we need to use this record... do work
						if (CalculateFileHash(qi, bw))
						{
							// Hash calculated
							if (qi.Action == QueueItemAction.AppendHash)
							{
								AppendHashToFileName(qi);
							}
						}
					}
				}
				//System.Windows.Forms.MessageBox.Show("If End");

				// Increment record counts -- TODO: switch {case}
				if (qi.Results == QueueItemResult.HashMatch)
				{
					_GoodRecords++;
				}
				else if (qi.Results == QueueItemResult.HashMisMatch)
				{
					_BadRecords++;
				}
				else if (qi.Results == QueueItemResult.HashGenerated || qi.Results == QueueItemResult.HashAppended)
				{
					_HashesGenerated++;
				}
				else if (qi.Results == QueueItemResult.FileNotFound)
				{
					_MissingFiles++;
				}
				else
				{
					_SkippedRecords++;
				}
				
				_CompletedRecords++;

				_ResultsQueue.Enqueue(qi);
				_ResultsList.Add(qi);

				// Log result to file
				if (LoggingEnabled)
				{
					LogToFile(qi);
				}
			}

			// A reset request was sent while we were working, purge session now
			if (_RequestReset)
			{
				PurgeSession();
			}

			_Working = false;
		}

		/// <summary>
		/// Request a purge and reset of the process queue and all statistics.
		/// </summary>
		public void Reset()
		{
			_RequestReset = true;
			if (!_Working)
			{
				PurgeSession();
			}
		}

		public HashFile SaveResultsToFile()
		{
			HashFile file = new HashFile(_OutputFile);
			// Clear all current results incase the file already existed and had some of these entries
			file.Clear();
			file.Add(this._ResultsList);
			if (file.Save())
			{
				return file;
			}
			else
			{
				return null;
			}
		}

		#endregion

		#region Private Methods

		private bool CalculateFileHash(QueueItem item, BackgroundWorker bw)
		{
			if (item == null)
				throw new ArgumentNullException("item");

			if (item.File.Exists)
			{
				string FileHash = String.Empty;

				if (item.Type == HashType.CRC)
				{
					uint crc;
					try
					{
						crc = Crc32.GetFileCRC32(item.File.FullName, ref _BytesProcessed, bw);
					}
					catch (Exception ex)
					{
						item.SetResultStatus(QueueItemResult.FileAccessViolation);
						return false;
					}
					FileHash = StringUtilities.UInt32ToHex(crc);
				}
				else if (item.Type == HashType.MD5)
				{
					byte[] md5;
					try
					{
						md5 = Md5.GetFileMD5(item.File.FullName, ref _BytesProcessed, bw);
					}
					catch (Exception ex)
					{
						item.SetResultStatus(QueueItemResult.FileAccessViolation);
						return false;
					}
					FileHash = StringUtilities.ByteArrayToHex(md5);
				}

				item.SetFileHash(FileHash);

				return true;
			}
			else
			{
				item.SetResultStatus(QueueItemResult.FileNotFound);
			}

			return false;
		}

		private bool AppendHashToFileName(QueueItem item)
		{
			if (item.File.Exists)
			{
				// Determine which QueueItem hash value is avaliable
				string FileHash;
				if (!String.IsNullOrEmpty(item.FileHash))
				{
					FileHash = item.FileHash;
				}
				else if (!String.IsNullOrEmpty(item.TestHash))
				{
					FileHash = item.TestHash;
				}
				else
				{
					// This would be pretty pointless if we didnt have a hash to append
					item.SetResultStatus(QueueItemResult.HashAppendFailed);
					return false;
				}

				// Generate the new file name
				string NewFileName;
				if (item.File.Name.IndexOf('.') > -1)
				{
					NewFileName = item.File.Name.Substring(0, item.File.Name.LastIndexOf('.')) + " [" + FileHash + "]" + item.File.Extension;
				}
				else
				{
					NewFileName = item.File.Name + "[" + FileHash + "]";
				}

				// Rename the file, catching common exceptions
				try
				{
					item.File.MoveTo(Path.Combine(item.File.DirectoryName, NewFileName));
				}
				catch (FileLoadException)
				{
					item.SetResultStatus(QueueItemResult.FileAccessViolation);
					return false;
				}
				catch (Exception ex)
				{
					item.SetResultStatus(QueueItemResult.HashAppendFailed);
					return false;
				}
			}
			else
			{
				// File didnt exist
				item.SetResultStatus(QueueItemResult.FileNotFound);
				return false;
			}

			// Success
			item.SetResultStatus(QueueItemResult.HashAppended);
			return true;
		}

		private void LogToFile(QueueItem qi)
		{
			if (String.IsNullOrEmpty(LogFile) || _LogLevel == ProcessLogLevel.None)
				return;

			bool LogIt = false;
			string LogLine = DateTime.Now.ToString() + "|" + qi.Action.ToString() + "|" + qi.Type.ToString() + "|" + qi.Results.ToString() + "|" + qi.File.FullName;

			if (qi.Results == QueueItemResult.HashMatch)
			{
				if ((_LogLevel & ProcessLogLevel.Good) == ProcessLogLevel.Good)
				{
					LogLine += "|Hash Match - File OK (" + qi.FileHash + ")";
					LogIt = true;
				}
			}
			else if (qi.Results == QueueItemResult.HashMisMatch)
			{
				if ((_LogLevel & ProcessLogLevel.Bad) == ProcessLogLevel.Bad)
				{
					LogLine += "|Hash Mis-Match - File Corrupt (Hash Should be: " + qi.TestHash + " - Actual Hash: " + qi.FileHash + ")";
					LogIt = true;
				}
			}
			else if (qi.Results == QueueItemResult.HashGenerated || qi.Results == QueueItemResult.HashAppended)
			{
				if ((_LogLevel & ProcessLogLevel.Locked) == ProcessLogLevel.Locked)
				{
					LogLine += "|Hash Generated - File Locked (" + qi.FileHash + ")";
					LogIt = true;
				}
			}
			else if (qi.Results == QueueItemResult.FileNotFound)
			{
				if ((_LogLevel & ProcessLogLevel.Missing) == ProcessLogLevel.Missing)
				{
					LogLine += "|Missing File - File Not Found (" + qi.File.Name + ")";
					LogIt = true;
				}
			}
			else
			{
				if ((_LogLevel & ProcessLogLevel.Skipped) == ProcessLogLevel.Skipped)
				{
					LogLine += "|File Skipped (" + qi.Results.ToString() + ")";
					LogIt = true;
				}
			}

			if (LogIt)
			{
				File.AppendAllText(LogFile, LogLine + Environment.NewLine);
			}
		}

		private void PurgeSession()
		{
			_ItemQueue.Clear();
			_ResultsQueue.Clear();
			_ResultsList.Clear();
			_RequestReset = false;

			_CurrentFileName = String.Empty;
			_OutputFile = String.Empty;
			_TotalRecords = 0;
			_CompletedRecords = 0;
			_SkippedRecords = 0;
			_BytesProcessed = 0;
			_GoodRecords = 0;
			_BadRecords = 0;
			_MissingFiles = 0;
			_HashesGenerated = 0;
		}

		#endregion
	}
}

using System;
using System.ComponentModel;
using System.IO;
using System.Security.Cryptography;

namespace UltraSFV.HashFunctions
{
	public sealed class Md5
	{
		#region Public Methods

		public static byte[] GetFileMD5(string path)
		{
			if (String.IsNullOrEmpty(path))
				throw new ArgumentNullException("path");

			MD5 md5p = MD5CryptoServiceProvider.Create();
			byte[] md5 = md5p.ComputeHash(ReadBinaryFile(path));

			return md5;
		}

		public static byte[] GetFileMD5(string filePath, ref long byteCount, BackgroundWorker bw)
		{
			if (String.IsNullOrEmpty(filePath))
				throw new ArgumentNullException("filePath");

			byte[] FileHash = null;

			using (Stream stream = File.OpenRead(filePath))
			{
				using (HashAlgorithm hashAlgorithm = MD5.Create())
				{
					byte[] oldBuffer;
					int oldBytesRead;

					long size = stream.Length;

					byte[] buffer = new byte[4096];

					int bytesRead = stream.Read(buffer, 0, buffer.Length);
					long totalBytesRead = bytesRead;
					byteCount += bytesRead;

					int ticks = 0;

					do
					{
						oldBytesRead = bytesRead;
						oldBuffer = buffer;

						buffer = new byte[4096];
						bytesRead = stream.Read(buffer, 0, buffer.Length);

						ticks++;
						totalBytesRead += bytesRead;
						byteCount += bytesRead;

						if (bytesRead == 0)
						{
							hashAlgorithm.TransformFinalBlock(oldBuffer, 0, oldBytesRead);
						}
						else
						{
							hashAlgorithm.TransformBlock(oldBuffer, 0, oldBytesRead, oldBuffer, 0);
						}

						if (bw != null && ticks == 10) // Only report once every 10 loops so we dont hose the CPU
						{
							bw.ReportProgress((int)((double)totalBytesRead * 100 / size));
							ticks = 0;
						}
					}
					while (bytesRead != 0);

					FileHash = hashAlgorithm.Hash;
				}
			}

			return FileHash;
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Read a file and return the data in binary format
		/// </summary>
		/// <param name="sFileName">file name</param>
		/// <returns>a byte array</returns>
		private static byte[] ReadBinaryFile(string sFileName)
		{
			byte[] data;
			using (FileStream fs = File.OpenRead(sFileName))
			{
				data = new byte[fs.Length];
				fs.Position = 0;
				fs.Read(data, 0, data.Length);
			}
			return data;
		}

		#endregion
	}
}

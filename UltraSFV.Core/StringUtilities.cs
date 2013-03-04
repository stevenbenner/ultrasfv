using System;
using System.Text;
using System.Text.RegularExpressions;

namespace UltraSFV.Core
{
	/// <summary>
	/// Provides methods for the parsing, manipulating and converting data into strings.
	/// </summary>
	public static class StringUtilities
	{
		#region Public Methods

		/// <summary>
		/// Searches a string for an 8 character alpha-numeric CRC value in breackets or parentheses.
		/// </summary>
		/// <param name="textToSearch">String to search.</param>
		/// <returns>An 8 character CRC or String.Empty.</returns>
		public static string FindCRC(string textToSearch)
		{
			if (String.IsNullOrEmpty(textToSearch))
				throw new ArgumentNullException("textToSearch");

			Regex CRCMatch = new Regex(@"[\(\[](?<crc>[A-F0-9]{8})[\]\)]", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.RightToLeft);

			Match m = CRCMatch.Match(textToSearch);

			if (m.Success)
			{
				return m.Groups["crc"].Value;
			}
			else
			{
				return String.Empty;
			}
		}

		/// <summary>
		/// Searches a string for an 32 character alpha-numeric MD5 value in breackets or parentheses.
		/// </summary>
		/// <param name="textToSearch">String to search.</param>
		/// <returns>An 32 character MD5 or String.Empty.</returns>
		public static string FindMD5(string textToSearch)
		{
			if (String.IsNullOrEmpty(textToSearch))
				throw new ArgumentNullException("textToSearch");

			Regex MD5Match = new Regex(@"[\(\[](?<md5>[A-F0-9]{32})[\]\)]", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.RightToLeft);

			Match m = MD5Match.Match(textToSearch);

			if (m.Success)
			{
				return m.Groups["md5"].Value;
			}
			else
			{
				return String.Empty;
			}
		}

		/// <summary>
		/// Pads a string with whitespace until it reaches the desired length.
		/// </summary>
		/// <param name="textToPad">String to add whitespace to.</param>
		/// <param name="textLength">Desired string length.</param>
		/// <returns>A string that is at least as long as requested.</returns>
		public static string AddWhiteSpace(string textToPad, int textLength)
		{
			if (String.IsNullOrEmpty(textToPad))
				throw new ArgumentNullException("textToPad");

			return AddWhiteSpace(textToPad, textLength, false);
		}


		/// <summary>
		/// Pads a string with whitespace until it reaches the desired length.
		/// </summary>
		/// <param name="textToPad">String to add whitespace to.</param>
		/// <param name="textLength">Desired string length.</param>
		/// <param name="padToLeft">Add whitespace to left instead of right.</param>
		/// <returns>A string that is at least as long as requested.</returns>
		public static string AddWhiteSpace(string textToPad, int textLength, bool padToLeft)
		{
			if (String.IsNullOrEmpty(textToPad))
				throw new ArgumentNullException("textToPad");

			if (!padToLeft)
			{
				while (textToPad.Length < textLength)
				{
					textToPad = textToPad + " ";
				}
			}
			else
			{
				while (textToPad.Length < textLength)
				{
					textToPad = " " + textToPad;
				}
			}

			return textToPad;
		}

		/// <summary>
		/// Formats a byte-length file size into standard measurements (1 KB, 1 MB, etc).
		/// </summary>
		/// <param name="size">File size in bytes.</param>
		/// <returns>A formatted file size in a string.</returns>
		public static string GetFileSizeAsString(long size)
		{
			string[] format = new string[] { "{0} bytes", "{0} KB", "{0} MB", "{0} GB", "{0} TB", "{0} PB", "{0} EB", "{0} ZB", "{0} YB" };
			int i = 0;
			if (size < Int32.MaxValue)
			{
				double s = size;
				while (i < format.Length - 1 && s >= 1024)
				{
					s = (int)(100 * s / 1024) / 100.0;
					i++;
				}
				return string.Format(format[i], s.ToString("###,###,###.##"));
			}
			else
			{
				long s = size;
				while (i < format.Length - 1 && s >= 1024)
				{
					s = (100L * s / 1024L) / 100L;
					i++;
				}
				return string.Format(format[i], s.ToString("###,###,###.##"));
			}
		}

		/// <summary>
		/// Parse a UInt32 and return the value as a hexadecimal string.
		/// </summary>
		/// <param name="uintToConvert">UInt32 value to convert.</param>
		/// <returns>A string containing a hex value.</returns>
		public static string UInt32ToHex(UInt32 uintToConvert)
		{
			string hash = String.Empty;

			foreach (byte b in UInt32ToBigEndianBytes(uintToConvert))
			{
				hash += b.ToString("X2");
			}
			return hash;
		}

		/// <summary>
		/// Dump binary data in hexadecimal.
		/// </summary>
		/// <param name="data">byte array</param>
		/// <returns>string in hexadecimal format</returns>
		public static string ByteArrayToHex(byte[] data)
		{
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < data.Length; i++)
			{
				sb.AppendFormat("{0:X2}", data[i]);
			}
			return sb.ToString();
		}

		#endregion

		#region Private Methods

		private static byte[] UInt32ToBigEndianBytes(UInt32 uintToConvert)
		{
			return new byte[] {
				(byte)((uintToConvert >> 24) & 0xff),
				(byte)((uintToConvert >> 16) & 0xff),
				(byte)((uintToConvert >> 8) & 0xff),
				(byte)(uintToConvert & 0xff)
			};
		}

		#endregion
	}
}

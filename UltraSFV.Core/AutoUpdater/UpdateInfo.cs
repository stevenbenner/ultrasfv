using System;
using System.Xml;
using System.Xml.Serialization;

namespace UltraSFV.Core
{
	[XmlRoot("updateInfo")]
	public class UpdateInfo
	{
		[XmlElement("fileName")]
		public string FileName;

		[XmlElement("fileSize")]
		public string FileSize;

		[XmlElement("crc")]
		public string CRC;

		[XmlElement("url")]
		public string Url;

		[XmlElement("version")]
		public string Version;

		[XmlElement("releaseDate")]
		public string ReleaseDate;

		public UpdateInfo()
		{
		}
	}
}

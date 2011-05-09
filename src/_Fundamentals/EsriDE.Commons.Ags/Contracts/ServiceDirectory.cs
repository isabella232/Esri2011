using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace EsriDE.Commons.Ags.Contracts
{
	[DataContract]
	public class ServiceDirectory
	{
		[DataMember(Name = "currentVersion")]
		public string CurrentVersion { get; set; }

		[DataMember(Name = "folders")]
		public List<string> Folders { get; set; }

		[DataMember(Name = "services")]
		public List<Service> Services { get; set; }

		public string Folder { get; set; }

		public Uri Url { get; set; }
	}
 }
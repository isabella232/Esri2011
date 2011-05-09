using System;
using System.Runtime.Serialization;

namespace EsriDE.Commons.Ags.Contracts
{
	[DataContract]
	public class Service
	{
		[DataMember(Name = "name")]
		public string Name { get; set; }

		[DataMember(Name = "type")]
		public string Type { get; set; }

		public string Folder { get; set; }

		public Uri RESTServerUrl { get; set; }

		public Uri RESTServiceUrl { get; set; }

		public Uri SOAPServiceUrl
		{
			get { return new Uri(RESTServiceUrl.AbsoluteUri.Replace(@"rest/", string.Empty)); }
		}

		public Uri SOAPServerUrl
		{
			get { return new Uri(RESTServerUrl.AbsoluteUri.Replace(@"rest/", string.Empty)); }
		}
	}
}
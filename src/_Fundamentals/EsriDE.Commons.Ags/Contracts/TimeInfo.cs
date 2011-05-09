using System.Runtime.Serialization;

namespace EsriDE.Commons.Ags.Contracts
{
	[DataContract]
	public class TimeInfo
	{
		[DataMember(Name = "timeExtent")]
		public object TimeExtent { get; set; }
	}
}
using System.Runtime.Serialization;

namespace EsriDE.Commons.Ags.Contracts
{
	[DataContract]
	public class SpatialReference
	{
		[DataMember(Name = "wkid")]
		public int wkid { get; set; }
	}
}
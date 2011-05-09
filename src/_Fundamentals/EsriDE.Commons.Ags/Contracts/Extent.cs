using System.Runtime.Serialization;

namespace EsriDE.Commons.Ags.Contracts
{
	[DataContract]
	public class Extent
	{
		[DataMember(Name = "xmin")]
		public double XMin { get; set; }

		[DataMember(Name = "ymin")]
		public double YMin { get; set; }

		[DataMember(Name = "xmax")]
		public double XMax { get; set; }

		[DataMember(Name = "ymax")]
		public double YMax { get; set; }

		[DataMember(Name = "spatialReference")]
		public SpatialReference SpatialReference { get; set; }
	}
}
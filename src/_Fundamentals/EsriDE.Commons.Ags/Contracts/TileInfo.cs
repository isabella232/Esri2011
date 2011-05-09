using System.Collections.Generic;
using System.Runtime.Serialization;

namespace EsriDE.Commons.Ags.Contracts
{
	[DataContract]
	public class TileInfo
	{
		[DataMember(Name = "rows")]
		public int Rows { get; set; }

		[DataMember(Name = "cols")]
		public int Cols { get; set; }

		[DataMember(Name = "dpi")]
		public int DPI { get; set; }

		[DataMember(Name = "format")]
		public string Format { get; set; }

		[DataMember(Name = "compressionQuality")]
		public int CompressionQuality { get; set; }

		[DataMember(Name = "origin")]
		public Point Origin { get; set; }

		[DataMember(Name = "spatialReference")]
		public SpatialReference SpatialReference { get; set; }

		[DataMember(Name = "lods")]
		public List<Lod> Lods { get; set; }
	}
}
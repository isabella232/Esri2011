using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace EsriDE.Commons.Ags.Contracts
{
	[DataContract]
	public class MapServiceInfo
	{
		// Properties
		[DataMember(Name = "serviceDescription")]
		public string ServiceDescription { get; set; }

		[DataMember(Name = "mapName")]
		public string Name { get; set; }

		[DataMember(Name = "description")]
		public string Description { get; set; }

		[DataMember(Name = "layers")]
		public List<LayerInfo> Layers { get; set; }

		[DataMember(Name = "tables")]
		public List<TableInfo> Tables { get; set; }

		[DataMember(Name = "spatialReference")]
		public SpatialReference SpatialReference { get; set; }

		[DataMember(Name = "singleFusedMapCache")]
		public bool SingleFusedMapCache { get; set; }

		[DataMember(Name = "tileInfo")]
		public TileInfo TileInfo { get; set; }

		[DataMember(Name = "initialExtent")]
		public Extent InitialExtent { get; set; }

		[DataMember(Name = "fullExtent")]
		public Extent FullExtent { get; set; }

		[DataMember(Name = "units")]
		public string Units { get; set; }

		[DataMember(Name = "supportedImageFormatTypes")]
		public string SupportedImageFormatTypes { get; set; }

		[DataMember(Name = "documentInfo")]
		public DocumentInfo DocumentInfo { get; set; }

		//public Service Service { get; set; }

		public Uri Uri { get; set; }
	}
}
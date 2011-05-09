using System.Collections.Generic;
using System.Runtime.Serialization;

namespace EsriDE.Commons.Ags.Contracts
{
	[DataContract]
	public class ImageServiceInfo
	{
		// Properties
		[DataMember(Name = "serviceDescription")]
		public string ServiceDescription { get; set; }

		[DataMember(Name = "name")]
		public string Name { get; set; }

		[DataMember(Name = "description")]
		public string Description { get; set; }

		[DataMember(Name = "extent")]
		public Extent Extent { get; set; }

		[DataMember(Name = "timeInfo")]
		public TimeInfo TimeInfo { get; set; }

		[DataMember(Name = "pixelSizeX")]
		public double PixelSizeX { get; set; }

		[DataMember(Name = "pixelSizeY")]
		public double PixelSizeY { get; set; }

		[DataMember(Name = "bandCount")]
		public int BandCount { get; set; }

		[DataMember(Name = "pixelType")]
		public string PixelType { get; set; }

		[DataMember(Name = "minPixelSize")]
		public double MinPixelSize { get; set; }

		[DataMember(Name = "maxPixelSize")]
		public double MaxPixelSize { get; set; }

		[DataMember(Name = "copyrightText")]
		public string CopyrightText { get; set; }

		[DataMember(Name = "serviceDataType")]
		public string ServiceDataType { get; set; }

		[DataMember(Name = "minValues")]
		public List<double> MinValues { get; set; }

		[DataMember(Name = "maxValues")]
		public List<double> MaxValues { get; set; }

		[DataMember(Name = "meanValues")]
		public List<double> MeanValues { get; set; }

		[DataMember(Name = "stdvValues")]
		public List<double> StdvValues { get; set; }

		[DataMember(Name = "objectIdField")]
		public string ObjectIdField { get; set; }

		[DataMember(Name = "fields")]
		public List<Field> Fields { get; set; }

		public Service Service { get; set; }
	}
}
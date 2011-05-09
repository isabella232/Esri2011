using System.Runtime.Serialization;

namespace EsriDE.Commons.Ags.Contracts
{
	[DataContract]
	public class ImageInfo
	{
		[DataMember(Name = "href")]
		public string Url { get; set; }

		[DataMember(Name = "width")]
		public int Width { get; set; }

		[DataMember(Name = "height")]
		public int Height { get; set; }

		[DataMember(Name = "extent")]
		public Extent Extent { get; set; }

		[DataMember(Name = "scale")]
		public decimal Scale { get; set; }
	}
}
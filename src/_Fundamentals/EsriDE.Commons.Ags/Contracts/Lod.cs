using System.Runtime.Serialization;

namespace EsriDE.Commons.Ags.Contracts
{
	[DataContract]
	public class Lod
	{
		[DataMember(Name = "level")]
		public int Level { get; set; }

		[DataMember(Name = "resolution")]
		public double Resolution { get; set; }

		[DataMember(Name = "scale")]
		public double Scale { get; set; }
	}
}
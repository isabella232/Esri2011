using System.Runtime.Serialization;

namespace EsriDE.Commons.Ags.Contracts
{
	[DataContract]
	public class Field
	{
		[DataMember(Name = "name")]
		public string Name { get; set; }

		[DataMember(Name = "type")]
		public string Type { get; set; }

		[DataMember(Name = "alias")]
		public string Alias { get; set; }
	}
}
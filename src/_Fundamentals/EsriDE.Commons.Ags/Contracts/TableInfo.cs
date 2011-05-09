using System.Runtime.Serialization;

namespace EsriDE.Commons.Ags.Contracts
{
	[DataContract]
	public class TableInfo
	{
		[DataMember(Name = "id")]
		public int Id { get; set; }

		[DataMember(Name = "name")]
		public string Name { get; set; }
	}
}
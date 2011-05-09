using System.Collections.Generic;
using System.Runtime.Serialization;

namespace EsriDE.Commons.Ags.Contracts
{
	[DataContract]
	public class LayerInfo
	{
		[DataMember(Name = "id")]
		public int Id { get; set; }

		[DataMember(Name = "name")]
		public string Name { get; set; }

		[DataMember(Name = "parentLayerId")]
		public int ParentLayerId { get; set; }

		[DataMember(Name = "defaultVisibility")]
		public bool defaultVisibility { get; set; }

		[DataMember(Name = "subLayerIds")]
		public List<int> SubLayerIds { get; set; }
	}
}
using System.Runtime.Serialization;

namespace EsriDE.Commons.Ags.Contracts
{
	[DataContract]
	public class DocumentInfo
	{
		[DataMember(Name = "Title")]
		public string Title { get; set; }

		[DataMember(Name = "Author")]
		public string Author { get; set; }

		[DataMember(Name = "Comments")]
		public string Comments { get; set; }

		[DataMember(Name = "Subject")]
		public string Subject { get; set; }

		[DataMember(Name = "Category")]
		public string Category { get; set; }

		[DataMember(Name = "Keywords")]
		public string Keywords { get; set; }
	}
}
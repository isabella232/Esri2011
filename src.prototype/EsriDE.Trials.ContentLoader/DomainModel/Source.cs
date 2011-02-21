using System;

namespace EsriDE.Trials.ContentLoader.DomainModel
{
	public class Source
	{
		public Uri Uri { get; set; }
		public RecursivityPolicy RecursivityPolicy { get; set; }

		public Source() { }

		public Source(Uri uri, RecursivityPolicy policy)
		{
			Uri = uri;
			RecursivityPolicy = policy;
		}
	}
}
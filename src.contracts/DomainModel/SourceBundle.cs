using System.Collections.Generic;

namespace EsriDE.Samples.ContentFinder.DomainModel
{
	public class SourceBundle
	{
		public string Type { get; private set; }
		public IEnumerable<Source> Sources { get; private set; }

		public SourceBundle(string type, IEnumerable<Source> sources)
		{
			Type = type;
			Sources = sources;
		}
	}
}
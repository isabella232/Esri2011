using System.Collections.Generic;

namespace EsriDE.Trials.ContentLoader.DomainModel
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
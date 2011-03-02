using EsriDE.Samples.ContentFinder.ContentAdapter.Contract;
using EsriDE.Samples.ContentFinder.DomainModel;

namespace EsriDE.Samples.ContentFinder.ContentAdapter
{
	public class ContentLocatorCreatorFilter : IContentLocatorCreatorFilter
	{
		public ContentLocatorCreatorFilter(string type, IContentLocatorCreator contentLocatorCreator)
		{
			Type = type;
			ContentLocatorCreator = contentLocatorCreator;
		}

		public bool IsResponsibleFor(SourceBundle sourceBundle)
		{
			return Type == sourceBundle.Type;
		}

		public string Type { get; private set; }
		public IContentLocatorCreator ContentLocatorCreator { get; private set; }
	}
}
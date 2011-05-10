using EsriDE.Samples.ContentFinder.ContentAdapter.Contract;
using EsriDE.Samples.ContentFinder.DomainModel;

namespace EsriDE.Samples.ContentFinder.ContentAdapter
{
	internal class NullContentLocatorCreator : IContentLocatorCreator
	{
		public bool IsResponsibleFor(SourceBundle sourceBundle)
		{
			return false;
		}

		public IContentLocator CreateContentLocator(SourceBundle sourceBundle)
		{
			return new NullContentLocator();
		}
	}
}

using EsriDE.Samples.ContentFinder.ContentAdapter.Contract;
using EsriDE.Samples.ContentFinder.ContentAdapter.Mxd;
using EsriDE.Samples.ContentFinder.DomainModel;

namespace EsriDE.Samples.ContentFinder.ContentAdapter
{
	public class MxdContentLocatorCreator : IContentLocatorCreator
	{
		public IContentLocator CreateContentLocator(SourceBundle sourceBundle)
		{
			return new MxdContentLocator(sourceBundle);
		}
	}
}
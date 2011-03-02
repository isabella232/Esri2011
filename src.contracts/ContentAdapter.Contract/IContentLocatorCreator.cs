using EsriDE.Samples.ContentFinder.DomainModel;

namespace EsriDE.Samples.ContentFinder.ContentAdapter.Contract
{
	public interface IContentLocatorCreator
	{
		IContentLocator CreateContentLocator(SourceBundle sourceBundle);
	}
}
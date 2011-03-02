using EsriDE.Samples.ContentFinder.DomainModel;

namespace EsriDE.Samples.ContentFinder.ContentAdapter.Contract
{
	public interface IContentLocatorFilter
	{
		IContentLocator ResolveContentLocator(SourceBundle sourceBundle);
	}
}
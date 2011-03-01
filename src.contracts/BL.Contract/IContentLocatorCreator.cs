using EsriDE.Samples.ContentFinder.ContentAdapter.Contract;
using EsriDE.Samples.ContentFinder.DomainModel;

namespace EsriDE.Samples.ContentFinder.BL.Contract
{
	public interface IContentLocatorCreator
	{
		IContentLocator CreateContentLocator(SourceBundle sourceBundle);
	}
}
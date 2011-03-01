using EsriDE.Samples.ContentFinder.ContentAdapter.Contract;
using EsriDE.Samples.ContentFinder.DomainModel;

namespace EsriDE.Samples.ContentFinder.BL.Contract
{
	public interface IContentLocatorCreatorFilter
	{
		IContentLocatorCreator ContentLocatorCreator { get; }
		bool IsResponsibleFor(SourceBundle sourceBundle);
	}
}
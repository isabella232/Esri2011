using EsriDE.Samples.ContentFinder.DomainModel;

namespace EsriDE.Samples.ContentFinder.ContentAdapter.Contract
{
	public interface IContentLocatorCreatorFilter
	{
		IContentLocatorCreator ContentLocatorCreator { get; }
		bool IsResponsibleFor(SourceBundle sourceBundle);
	}
}
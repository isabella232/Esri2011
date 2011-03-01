using EsriDE.Samples.ContentFinder.ContentAdapter.Contract;
using EsriDE.Samples.ContentFinder.DomainModel;

namespace EsriDE.Samples.ContentFinder.BL
{
	public class CorBasedMxdContentLocatorFilter : CorBasedContentLocatorFilter
	{
		public CorBasedMxdContentLocatorFilter(CorBasedContentLocatorFilter successor, IContentLocator contentLocator) 
			: base(successor, contentLocator)
		{
		}

		protected override bool IsResponsibleFor(SourceBundle sourceBundle)
		{
			var isResponsible = sourceBundle.Type == "mxd";
			return isResponsible;
		}
	}
}
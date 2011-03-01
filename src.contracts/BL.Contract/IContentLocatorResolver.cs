using System.Collections.Generic;
using EsriDE.Samples.ContentFinder.ContentAdapter.Contract;
using EsriDE.Samples.ContentFinder.DomainModel;

namespace EsriDE.Samples.ContentFinder.BL.Contract
{
	public interface IContentLocatorResolver
	{
		IContentLocator ResolveContentLocator(SourceBundle sourceBundle);

		IEnumerable<IContentLocator> ResolveContentLocators(IEnumerable<SourceBundle> sourceBundles);
	}
}
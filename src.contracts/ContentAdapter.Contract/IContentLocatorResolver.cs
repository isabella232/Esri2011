using System.Collections.Generic;
using EsriDE.Samples.ContentFinder.DomainModel;

namespace EsriDE.Samples.ContentFinder.ContentAdapter.Contract
{
	public interface IContentLocatorResolver
	{
		IContentLocator ResolveContentLocator(SourceBundle sourceBundle);

		IEnumerable<IContentLocator> ResolveContentLocators(IEnumerable<SourceBundle> sourceBundles);
	}
}
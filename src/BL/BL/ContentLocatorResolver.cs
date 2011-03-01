using System;
using System.Collections.Generic;
using System.Linq;
using EsriDE.Samples.ContentFinder.BL.Contract;
using EsriDE.Samples.ContentFinder.ContentAdapter.Contract;
using EsriDE.Samples.ContentFinder.DomainModel;

namespace EsriDE.Samples.ContentFinder.BL
{
	public class ContentLocatorResolver : IContentLocatorResolver
	{
		private readonly IEnumerable<IContentLocatorCreatorFilter> _contentLocatorFilters;

		public ContentLocatorResolver(IEnumerable<IContentLocatorCreatorFilter> contentLocatorCreatorFilters)
		{
			_contentLocatorFilters = contentLocatorCreatorFilters;
		}

		public IContentLocator ResolveContentLocator(SourceBundle sourceBundle)
		{
			foreach (var contentLocatorFilter in _contentLocatorFilters)
			{
				if (contentLocatorFilter.IsResponsibleFor(sourceBundle))
				{
					var creator = contentLocatorFilter.ContentLocatorCreator;
					return creator.CreateContentLocator(sourceBundle);
				}
			}

			// NullObject Pattern (http://en.wikipedia.org/wiki/Null_Object_pattern)
			return new NullContentLocator();
		}

		public IEnumerable<IContentLocator> ResolveContentLocators(IEnumerable<SourceBundle> sourceBundles)
		{
			return sourceBundles.Select(ResolveContentLocator);
		}
	}
}
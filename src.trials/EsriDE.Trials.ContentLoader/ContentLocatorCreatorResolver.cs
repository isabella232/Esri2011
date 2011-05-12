using System;
using System.Collections.Generic;
using EsriDE.Trials.ContentLoader.DomainModel;

namespace EsriDE.Trials.ContentLoader
{
	public class ContentLocatorCreatorResolver
	{
		private IEnumerable<ContentLocatorCreator> _contentLocatorCreators;

		public ContentLocatorCreatorResolver()
		{
			var creatorLocator = new ContentLocatorCreatorLocator();
			_contentLocatorCreators = creatorLocator.GetAllContentLocatorCreators();
		}

		public IEnumerable<ContentFinder.ContentFinder> GetContentLocators(IEnumerable<SourceBundle> sourceBundles)
		{
			foreach (var sourceBundle in sourceBundles)
			{
				var creator = ResolveContentLocatorCreator(sourceBundle);
				var contentLocator = creator.CreateContentLocator(sourceBundle);
				yield return contentLocator;
			}

			yield break;
		}

		private ContentLocatorCreator ResolveContentLocatorCreator(SourceBundle sourceBundle)
		{
			foreach (var creator in _contentLocatorCreators)
			{
				if (creator.CanHandleContentType(sourceBundle.Type))
				{
					return creator;
				}
			}

			throw new ArgumentException("Unmanageable Contenttype");
		}

	}
}
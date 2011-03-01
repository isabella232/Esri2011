using System;
using EsriDE.Samples.ContentFinder.BL.Contract;
using EsriDE.Samples.ContentFinder.ContentAdapter.Contract;
using EsriDE.Samples.ContentFinder.DomainModel;

namespace EsriDE.Samples.ContentFinder.BL
{
	public abstract class CorBasedContentLocatorFilter : IContentLocatorFilter
	{
		private readonly CorBasedContentLocatorFilter _successor;
		private readonly IContentLocator _contentLocator;

		protected CorBasedContentLocatorFilter(CorBasedContentLocatorFilter successor, IContentLocator contentLocator)
		{
			_successor = successor;
			_contentLocator = contentLocator;
		}

		public CorBasedContentLocatorFilter(IContentLocator contentLocator)
		{
			_successor = null;
		}

		public virtual IContentLocator ResolveContentLocator(SourceBundle sourceBundle)
		{
			if (IsResponsibleFor(sourceBundle))
			{
				return _contentLocator;
			}

			if (null != _successor)
			{
				return _successor.ResolveContentLocator(sourceBundle);
			}
			
			return new NullContentLocator();
		}

		protected abstract bool IsResponsibleFor(SourceBundle sourceBundle);

	}
}

/*
if (Responsible(content))
				return _factory;
			else
			{
				return Sucsessor.Resolve(content);
			}
*/
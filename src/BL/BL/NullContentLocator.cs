using System;
using EsriDE.Samples.ContentFinder.ContentAdapter.Contract;
using EsriDE.Samples.ContentFinder.DomainModel;

namespace EsriDE.Samples.ContentFinder.BL
{
	internal class NullContentLocator : IContentLocator
	{
		public void LocateContent(SourceBundle sourceBundle)
		{
			//Do nothing;
		}

		public void StartSearch()
		{
			//Do nothing;
		}

		public void StopSearch()
		{
			//Do nothing;
		}

		public event Action<Content> FoundContent;
		public event Action FinishedSearch;
	}
}
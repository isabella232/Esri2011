using System;
using EsriDE.Samples.ContentFinder.DomainModel;

namespace EsriDE.Samples.ContentFinder.ContentAdapter.Contract
{
	public interface IContentLocator
	{
		void StartSearch();
		void StopSearch();

		event Action<Content> FoundContent;
		event Action FinishedSearch;
	}
}
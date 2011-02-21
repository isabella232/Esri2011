using System;
using EsriDE.Trials.ContentLoader.DomainModel;

namespace EsriDE.Trials.ContentLoader.ContentFinder
{
	public interface IContentFinder
	{
		void StartSearch();
		void StopSearch();
		event Action<Content> FoundContent;
		event Action FinishedSearch;
	}
}
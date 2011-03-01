using System;
using System.IO;
using EsriDE.Samples.ContentFinder.DomainModel;

namespace EsriDE.Samples.ContentFinder.ContentAdapter
{
	public abstract class FileContentLocator : ContentLocator
	{
		private readonly Predicate<FileInfo> _predicate;

		protected FileContentLocator(SourceBundle sourceBundle, Predicate<FileInfo> predicate) : base(sourceBundle)
		{
			_predicate = predicate;
		}

		protected override void Search()
		{
			var files = new FileUriAnalyzer(_predicate);
			var uris = files.GetRecursivUris(SourceBundle.Sources);

			foreach (var uri in uris)
			{
				if (RunningState.Stopped == ActualRunningState)
				{
					return;
				}

				Process(uri);
			}

			OnFinishedSearch();
		}

		protected abstract void Process(Uri uri);
	}
}
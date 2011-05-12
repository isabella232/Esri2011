using System;
using System.Collections.Generic;
using System.IO;
using EsriDE.Trials.ContentLoader.DomainModel;

namespace EsriDE.Trials.ContentLoader.ContentFinder
{
	public abstract class FileContentFinder : ContentFinder
	{
		private readonly Predicate<FileInfo> _predicate;

		protected FileContentFinder(SourceBundle sourceBundle, Predicate<FileInfo> predicate) : base(sourceBundle)
		{
			_predicate = predicate;
		}

		protected override void Search()
		{
			var files = new FileUriAnalyzer(_predicate);
			var mxdUris = files.GetRecursivUris(SourceBundle.Sources);

			foreach (var mxdUri in mxdUris)
			{
				if (RunningState.Stopped == ActualRunningState)
				{
					return;
				}

				Process(mxdUri);
			}

			OnFinishedSearch();
		}

		//protected abstract Predicate<FileInfo> GetPredicate(FileInfo fileInfo);
		protected abstract void Process(Uri uri);
	}
}
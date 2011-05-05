using System;
using System.Threading;
using EsriDE.Samples.ContentFinder.DomainModel;

namespace EsriDE.Samples.ContentFinder.ContentAdapter.Ags
{
	public abstract class AgsContentLocator : ContentLocator
	{
		public AgsContentLocator(SourceBundle sourceBundle)
			: base(sourceBundle)
		{
		}

		protected override void Search()
		{
			var analyzer = new AgsUriAnalyzer();
			var uris = analyzer.GetRecursivUris(SourceBundle.Sources);

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

		

		protected override void ConfigureThread(Thread thread)
		{
			thread.Priority = ThreadPriority.Lowest;
			//thread.SetApartmentState(ApartmentState.STA);
		}

		//private readonly Predicate<FileInfo> _predicate;

		//protected FileContentLocator(SourceBundle sourceBundle, Predicate<FileInfo> predicate) : base(sourceBundle)
		//{
		//    _predicate = predicate;
		//}

		protected abstract void Process(Uri uri);
	}
}
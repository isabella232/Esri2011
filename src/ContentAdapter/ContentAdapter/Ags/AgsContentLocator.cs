using System;
using System.Threading;
using EsriDE.Commons.Ags;
using EsriDE.Samples.ContentFinder.DomainModel;

namespace EsriDE.Samples.ContentFinder.ContentAdapter.Ags
{
	public class AgsContentLocator : ContentLocator
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

		protected void Process(Uri uri)
		{
			var serviceType = AgsUtil.GetServiceType(uri);
			var reader = AgsContentReader.CreateAgsContentReader(serviceType);

			var agsContent = reader.ReadContent(uri);
			OnFoundContent(agsContent);
		}
	}
}
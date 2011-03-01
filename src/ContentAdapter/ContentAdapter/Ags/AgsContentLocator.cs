using System.Threading;
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
		}

		protected override void ConfigureThread(Thread thread)
		{
			thread.Priority = ThreadPriority.Lowest;
			//thread.SetApartmentState(ApartmentState.STA);
		}
	}
}
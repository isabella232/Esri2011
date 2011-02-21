using System.Threading;
using EsriDE.Trials.ContentLoader.DomainModel;

namespace EsriDE.Trials.ContentLoader.ContentFinder
{
	public class AgsContentFinder : ContentFinder
	{
		public AgsContentFinder(SourceBundle sourceBundle)
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
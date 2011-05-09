using System;
using EsriDE.Samples.ContentFinder.ContentAdapter.Ags;
using EsriDE.Samples.ContentFinder.ContentAdapter.Contract;
using EsriDE.Samples.ContentFinder.DomainModel;

namespace EsriDE.Samples.ContentFinder.ContentAdapter
{
	public class AgsContentLocatorCreator : IContentLocatorCreator
	{
		public IContentLocator CreateContentLocator(SourceBundle sourceBundle)
		{
			//return new AgsContentLocator(sourceBundle);
			throw new NotImplementedException();
			return new AgsContentLocator(sourceBundle);

		}
	}
}
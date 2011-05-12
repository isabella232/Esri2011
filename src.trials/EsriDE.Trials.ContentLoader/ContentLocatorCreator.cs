using System;
using System.Collections.Generic;
using System.Linq;
using EsriDE.Trials.ContentLoader.ContentFinder;
using EsriDE.Trials.ContentLoader.DomainModel;

namespace EsriDE.Trials.ContentLoader
{
	public abstract class ContentLocatorCreator
	{
		public abstract ContentFinder.ContentFinder CreateContentLocator(SourceBundle sourceBundle);

		public bool CanHandleContentType(string type)
		{
			var supportedTypes = GetSupportedTypes();
			return supportedTypes.Any(supportedType => supportedType == type);
		}

		protected abstract IEnumerable<string> GetSupportedTypes();
	}

	class MxdContentLocatorCreator : ContentLocatorCreator
	{
		public override ContentFinder.ContentFinder CreateContentLocator(SourceBundle sourceBundle)
		{
			return new MxdContentFinder(sourceBundle);
		}

		protected override IEnumerable<string> GetSupportedTypes()
		{
			return new List<string> {"mxd"};
		}
	}

	class AgsContentLocatorCreator : ContentLocatorCreator
	{
		public override ContentFinder.ContentFinder CreateContentLocator(SourceBundle sourceBundle)
		{
			return new AgsContentFinder(sourceBundle);
		}

		protected override IEnumerable<string> GetSupportedTypes()
		{
			return new List<string> { "ags" };
		}
	}
}
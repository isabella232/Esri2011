using System.Collections.Generic;
using EsriDE.Samples.ContentFinder.BL.Contract;
using EsriDE.Samples.ContentFinder.ConfigurationAdapter.Contract;
using EsriDE.Samples.ContentFinder.ContentAdapter.Contract;
using EsriDE.Samples.ContentFinder.DomainModel;

namespace EsriDE.Samples.ContentFinder.BL
{
	public class Controller : IController
	{
		private readonly IConfigurationReader _configurationReader;
		private readonly IContentLocatorResolver _contentLocatorResolver;
		private IEnumerable<IContentLocator> _contentLocators;

		public Controller(IConfigurationReader configurationReader, IContentLocatorResolver contentLocatorResolver)
		{
			_configurationReader = configurationReader;
			_contentLocatorResolver = contentLocatorResolver;
		}

		public void Start()
		{
			var sourceBundles = _configurationReader.ReadConfiguration(null);
			_contentLocators = GetContentLocators(sourceBundles);
			foreach (var contentLocator in _contentLocators)
			{
				contentLocator.StartSearch();
			}
		}

		public void Stop()
		{
			//throw new NotImplementedException();
			foreach (var contentLocator in _contentLocators)
			{
				contentLocator.StopSearch();
			}
		}

		private IEnumerable<IContentLocator> GetContentLocators(IEnumerable<SourceBundle> sourceBundles)
		{
			foreach (var sourceBundle in sourceBundles)
			{
				IContentLocator contentLocator = _contentLocatorResolver.ResolveContentLocator(sourceBundle);
				yield return contentLocator;
			}
		}
	}
}
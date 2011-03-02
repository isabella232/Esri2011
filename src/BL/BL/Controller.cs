using System;
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
		private volatile int _runningLocators;
		private bool _areAllLocatorsStarted;

		private object _lockObject = new object();

		public Controller(IConfigurationReader configurationReader, IContentLocatorResolver contentLocatorResolver)
		{
			_configurationReader = configurationReader;
			_contentLocatorResolver = contentLocatorResolver;
		}

		public void Start()
		{
			_areAllLocatorsStarted = false;

			var sourceBundles = _configurationReader.ReadConfiguration(null);
			_contentLocators = GetContentLocators(sourceBundles);
			foreach (var contentLocator in _contentLocators)
			{
				contentLocator.FoundContent += HandleFoundContent;
				contentLocator.FinishedSearch += HandleFinishedSearch;

				lock (_lockObject)
				{
					contentLocator.StartSearch();
					_runningLocators++;
				}
			}

			_areAllLocatorsStarted = true;
		}

		public event Action SearchComplete = delegate { };
		public event Action<Content> ContentFound = delegate { };

		private void HandleFinishedSearch()
		{
			lock (_lockObject)
			{
				_runningLocators--;
			}

			if (_areAllLocatorsStarted && 0 == _runningLocators)
			{
				SearchComplete();
			}
		}

		private void HandleFoundContent(Content obj)
		{
			ContentFound(obj);
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
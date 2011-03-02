using System;
using System.Collections.Generic;
using System.Linq;
using EsriDE.Samples.ContentFinder.ConfigurationAdapter.Contract;
using EsriDE.Samples.ContentFinder.ContentAdapter.Contract;
using EsriDE.Samples.ContentFinder.DomainModel;
using NUnit.Framework;

namespace EsriDE.Samples.ContentFinder.ContentAdapter.Tests
{
	[TestFixture]
	public class ContentLocatorResolverFixture
	{
		private IConfigurationReader _configurationReader;
		private IEnumerable<IContentLocatorCreatorFilter> _contentLocatorCreatorFilters;

		[TestFixtureSetUp]
		private void Setup()
		{
			_configurationReader = new SampleConfigurationReader();
			_contentLocatorCreatorFilters = GetContentLocatorCreatorFilters();
		}

		private IEnumerable<IContentLocatorCreatorFilter> GetContentLocatorCreatorFilters()
		{
			yield return new ContentLocatorCreatorFilter("mxd", new MxdContentLocatorCreator());
			yield return new ContentLocatorCreatorFilter("ags", new AgsContentLocatorCreator());
		}

		[Test]
		public void ResolvingContentLocators_WithValidInput_Returns3ContentLocators()
		{
			var sourceBundles = _configurationReader.ReadConfiguration(null);
			IContentLocatorResolver resolver = new ContentLocatorResolver(_contentLocatorCreatorFilters);
			foreach (var sourceBundle in sourceBundles)
			{
				var locator = resolver.ResolveContentLocator(sourceBundle);
				Assert.That(locator, !Is.Null);
			}


			var locators = resolver.ResolveContentLocators(sourceBundles);
			Assert.That(locators, !Is.Null);
			Assert.That(locators.ToList().Count, Is.EqualTo(3));
		}

		[Test]
		public void Do()
		{
			var configurations = _configurationReader.ReadConfiguration(null);
			var adapters = configurations.GetAdapters();

			foreach (var adapter in adapters)
			{
				adapter.FoundContent += ProcessContent;
				adapter.StartSearch();
			}

		}

		private void ProcessContent(Content content)
		{
			Console.WriteLine(content.Title);
		}
	}

	public static class ConfigurationExtensions
	{
		public static IEnumerable<IContentLocator> GetAdapters(this IEnumerable<SourceBundle> sourceBundles)
		{
			//var contentLocatorCreatorResolver = new ContentLocatorCreatorResolver();
			//var contentLocators = contentLocatorCreatorResolver.GetContentLocators(sourceBundles);
			//return contentLocators;
			yield break;
		}
	}
}
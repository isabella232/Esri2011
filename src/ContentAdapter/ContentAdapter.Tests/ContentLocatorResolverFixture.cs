using System.Collections.Generic;
using System.Linq;
using EsriDE.Samples.ContentFinder.ConfigurationAdapter.Contract;
using EsriDE.Samples.ContentFinder.ContentAdapter.Contract;
using NUnit.Framework;

namespace EsriDE.Samples.ContentFinder.ContentAdapter.Tests
{
	// ReSharper disable InconsistentNaming

	[TestFixture]
	public class ContentLocatorResolverFixture
	{
		private IConfigurationReader _configurationReader;
		private IEnumerable<IContentLocatorCreatorFilter> _contentLocatorCreatorFilters;

		[SetUp]
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
			// siehe SampleConfigurationReader
			var sourceBundles = _configurationReader.ReadConfiguration(null);
			IContentLocatorResolver resolver = new ContentLocatorResolver(_contentLocatorCreatorFilters);

			// einzelne Auflösung testen
			foreach (var sourceBundle in sourceBundles)
			{
				var locator = resolver.ResolveContentLocator(sourceBundle);
				Assert.That(locator, !Is.Null);
			}

			// Auflösung als Liste testen
			var locators = resolver.ResolveContentLocators(sourceBundles);
			Assert.That(locators, !Is.Null);
			Assert.That(locators.ToList().Count, Is.EqualTo(3));
		}

		// ReSharper restore InconsistentNaming
	}
}
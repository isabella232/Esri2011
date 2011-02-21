using System;
using System.Collections.Generic;
using System.Linq;
using EsriDE.Trials.ContentLoader.DomainModel;
using NUnit.Framework;

namespace EsriDE.Trials.ContentLoader
{
	[TestFixture]
	public class Fixture : FixtureBase
	{
		private IConfigurationManager _configurationManager = new SampleConfigurationManager();

		[Test]
		public void Do()
		{
			var configurations = _configurationManager.GetContentsToSearch();
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
		public static IEnumerable<ContentFinder.ContentFinder> GetAdapters(this IEnumerable<SourceBundle> sourceBundles)
		{
			var contentLocatorCreatorResolver = new ContentLocatorCreatorResolver();
			var contentLocators = contentLocatorCreatorResolver.GetContentLocators(sourceBundles);
			return contentLocators;
		}
	}
}
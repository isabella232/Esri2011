using System;
using System.Collections.Generic;
using EsriDE.Samples.ContentFinder.ConfigurationAdapter.Contract;
using EsriDE.Samples.ContentFinder.DomainModel;

namespace EsriDE.Samples.ContentFinder.SystemBuild.Tests
{
	public class FakeConfigurationReader : IConfigurationReader
	{
			public IEnumerable<SourceBundle> ReadConfiguration(Uri uri)
			{
				var s = new SourceBundle("mxd",
				                         new List<Source>
				                         	{
				                         		new Source(
				                         			new Uri(
				                         				@"C:\Projects\_Esri2011\Workshop\src\ContentAdapter\ContentAdapter.Tests\TestData"),
				                         			RecursivityPolicy.Recursiv)
				                         	});
				yield return s;
				yield break;
			}
	}
}
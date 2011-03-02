using System;
using System.Collections.Generic;
using EsriDE.Samples.ContentFinder.ConfigurationAdapter.Contract;
using EsriDE.Samples.ContentFinder.DomainModel;

namespace EsriDE.Samples.ContentFinder.ContentAdapter.Tests
{
	public class SampleConfigurationReader : IConfigurationReader
	{
		public IEnumerable<SourceBundle> ReadConfiguration(Uri uri)
		{
			yield return
				new SourceBundle("mxd",
				            new List<Source>
				            	{
				            		new Source(new Uri(@"c:\temp\"), RecursivityPolicy.Recursiv),
				            		new Source(new Uri(@"c:\users\"), RecursivityPolicy.NotRecursiv)
				            	});
			yield return
				new SourceBundle("mxd",
				            new List<Source>
				            	{
				            		new Source(new Uri(@"z:\"), RecursivityPolicy.NotRecursiv),
				            	});
			yield return
				new SourceBundle("ags",
							new List<Source>
				            	{
				            		new Source(new Uri(@"http://server.arcgisonline.com/ArcGIS/rest/services"), RecursivityPolicy.Recursiv),
				            		new Source(new Uri(@"http://server.arcgisonline.com/ArcGIS/soap/services"), RecursivityPolicy.NotRecursiv)
				            	});
			yield break;
		}
	}
}
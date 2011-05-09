using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using EsriDE.Samples.ContentFinder.DomainModel;

namespace EsriDE.Samples.ContentFinder.ContentAdapter.Tests
{
	internal static class TestDataUtils
	{
		internal static SourceBundle GetMapDocumentConfigItemsForSingleFolderRecursiv()
		{
			var location = Assembly.GetExecutingAssembly().Location;
			var path = Path.GetDirectoryName(location);
			var filename = Path.Combine(path, @"TestData");

			var uri = new Uri(filename);

			var source = new Source(uri, RecursivityPolicy.Recursiv);
			var sourceBundle = new SourceBundle("mxd", new List<Source> { source });

			return sourceBundle;
		}

		internal static SourceBundle GetMapDocumentConfigItemsForSingleFolderNonRecursiv()
		{
			string location = Assembly.GetExecutingAssembly().Location;
			string path = Path.GetDirectoryName(location);
			string filename = Path.Combine(path, @"TestData");

			var uri = new Uri(filename);

			var source = new Source(uri, RecursivityPolicy.NotRecursiv);
			var sourceBundle = new SourceBundle("mxd", new List<Source> { source });

			return sourceBundle;
		}

		internal static IEnumerable<SourceBundle> GetTwoSourceBundlesForTestData()
		{
			yield return GetMapDocumentConfigItemsForSingleFolderNonRecursiv();
			yield return GetMapDocumentConfigItemsForSingleFolderRecursiv();
		}

		internal static IEnumerable<Source> GetDemoSources(RecursivityPolicy policy)
		{
			yield return new Source(new Uri(@"http://vsdp1001.srvc.esri-de.com/ArcGIS/rest/services/"), policy);
		}

		internal static IEnumerable<Source> GetAgoSources(RecursivityPolicy policy)
		{
			yield return new Source(new Uri(@"http://server.arcgisonline.com/ArcGIS/rest/services"), policy);
		}

		internal static SourceBundle GetDemoSourceBundle(RecursivityPolicy policy)
		{
			var sourceBundle = new SourceBundle("ags", GetDemoSources(policy));
			return sourceBundle;
		}

		internal static SourceBundle GetAgoSourceBundle(RecursivityPolicy policy)
		{
			var sourceBundle = new SourceBundle("ags", GetAgoSources(policy));
			return sourceBundle;
		}
	}
}
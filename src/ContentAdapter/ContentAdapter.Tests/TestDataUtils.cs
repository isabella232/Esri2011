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
	}
}
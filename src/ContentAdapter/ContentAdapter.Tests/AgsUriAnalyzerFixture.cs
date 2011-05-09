using System;
using EsriDE.Samples.ContentFinder.ContentAdapter.Ags;
using EsriDE.Samples.ContentFinder.DomainModel;
using NUnit.Framework;

//using NUnit.Framework.SyntaxHelpers;

namespace EsriDE.Samples.ContentFinder.ContentAdapter.Tests
{
	// ReSharper disable InconsistentNaming

	[TestFixture]
	public class AgsUriAnalyzerFixture
	{
		[Test]
		public void ReadingNotRecursiv()
		{
			var sources = TestDataUtils.GetDemoSources(RecursivityPolicy.NotRecursiv);

			var sut = new AgsUriAnalyzer();
			var uris = sut.GetRecursivUris(sources);

			int count = 0;
			foreach (var uri in uris)
			{
				count++;
				Console.WriteLine(uri);
			}

			Console.WriteLine("AgsUriAnalyzer.GetRecursivUris (NotRecursiv) lieferte Uris: " + count);
			Assert.That(count, Is.GreaterThan(0));
		}

		[Test]
		public void ReadingRecursiv()
		{
			var sources = TestDataUtils.GetDemoSources(RecursivityPolicy.Recursiv);

			var sut = new AgsUriAnalyzer();
			var uris = sut.GetRecursivUris(sources);

			int count = 0;
			foreach (var uri in uris)
			{
				count++;
				Console.WriteLine(uri);
			}

			Console.WriteLine("AgsUriAnalyzer.GetRecursivUris (Recursiv) lieferte Uris: " + count);
			Assert.That(count, Is.GreaterThan(0));
		}
	}

	// ReSharper restore InconsistentNaming
}

using System;
using System.Collections.Generic;
using System.Threading;
using EsriDE.Commons.Testing;
using EsriDE.Samples.ContentFinder.ContentAdapter.Ags;
using EsriDE.Samples.ContentFinder.DomainModel;
using NUnit.Framework;

//using NUnit.Framework.SyntaxHelpers;

namespace EsriDE.Samples.ContentFinder.ContentAdapter.Tests
{
	// ReSharper disable InconsistentNaming

	[TestFixture]
	public class AgsContentLocatorFixture : FixtureBase
	{
		private IList<AgsContent> _agsContents;
		private bool _finished;
		private int _counter;
		private Content _content;

		public override void Setup()
		{
			_finished = false;
			_agsContents = new List<AgsContent>();
		}
		
		[Test]
		public void Spike()
		{
			//var sourceBunde = TestDataUtils.GetDemoSourceBundle(RecursivityPolicy.NotRecursiv);
			var sourceBunde = TestDataUtils.GetAgoSourceBundle(RecursivityPolicy.NotRecursiv);
			var sut = new AgsContentLocator(sourceBunde);

			sut.FoundContent += FoundContent;
			sut.FinishedSearch += FinishedSearch;

			sut.StartSearch();

			while (!_finished)
			{
			}

			Assert.That(_counter, Is.GreaterThan(0));
		}
		
		private void FinishedSearch()
		{
			_finished = true;
		}

		private void FoundContent(Content content)
		{
			Console.WriteLine("ThreadID=" + Thread.CurrentThread.ManagedThreadId);
			_counter++;
			_content = content;
			if (content is AgsContent)
			{
				_agsContents.Add((AgsContent)content);
			}
		}
	}

	// ReSharper restore InconsistentNaming
}

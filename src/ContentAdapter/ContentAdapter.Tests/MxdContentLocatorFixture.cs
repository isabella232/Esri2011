using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using EsriDE.Samples.ContentFinder.ContentAdapter.Mxd;
using EsriDE.Samples.ContentFinder.DomainModel;
using NUnit.Framework;

namespace EsriDE.Samples.ContentFinder.ContentAdapter.Tests
{
	[TestFixture]
	public class MxdContentLocatorFixture : FixtureBase
	{
		public override void Setup()
		{
			_finished = false;
			_counter = 0;
			_content = null;

			_mxdContents = new List<MxdContent>();
		}

		private bool _finished;
		private int _counter;
		private Content _content;
		private IList<MxdContent> _mxdContents;

		private static SourceBundle GetMapDocumentConfigItemsForSingleFolderRecursiv()
		{
			var location = Assembly.GetExecutingAssembly().Location;
			var path = Path.GetDirectoryName(location);
			var filename = Path.Combine(path, @"TestData");

			var uri = new Uri(filename);

			var source = new Source(uri, RecursivityPolicy.Recursiv);
			var sourceBundle = new SourceBundle("mxd", new List<Source> {source});

			return sourceBundle;
		}

		private static SourceBundle GetMapDocumentConfigItemsForSingleFolderNonRecursiv()
		{
			string location = Assembly.GetExecutingAssembly().Location;
			string path = Path.GetDirectoryName(location);
			string filename = Path.Combine(path, @"TestData");

			var uri = new Uri(filename);

			var source = new Source(uri, RecursivityPolicy.NotRecursiv);
			var sourceBundle = new SourceBundle("mxd", new List<Source> {source});

			return sourceBundle;
		}

		private void FinishedSearch()
		{
			_finished = true;
		}

		private void FoundContent(Content content)
		{
			_counter++;
			_content = content;
			if (content is MxdContent)
			{
				_mxdContents.Add((MxdContent) content);
			}
		}

		[Test]
		[STAThread]
		public void ReadingTestMxd_SingleFolderNonRecursiv_Runs()
		{
			var s = GetMapDocumentConfigItemsForSingleFolderNonRecursiv();
			var sut = new MxdContentLocator(s);
			sut.FoundContent += FoundContent;
			sut.FinishedSearch += FinishedSearch;

			sut.StartSearch();
			while (!_finished)
			{
			}

			Assert.That(_counter, Is.EqualTo(1));

			Assert.That(_content, !Is.Null);
			Assert.That(_content.Title, Is.EqualTo("Title of MapDocument1"));
			Assert.That(_content.Bitmap, !Is.Null);

			var mxdContent = (MxdContent) _content;

			Assert.That(mxdContent.Subject, Is.EqualTo("Summary of MapDocument1"));
			Assert.That(mxdContent.Comments, Is.EqualTo("Description of MapDocument1"));
		}

		[Test]
		[STAThread]
		public void ReadingTestMxd_SingleFolderRecursiv_Runs()
		{
			var s = GetMapDocumentConfigItemsForSingleFolderRecursiv();
			var sut = new MxdContentLocator(s);
			sut.FoundContent += FoundContent;
			sut.FinishedSearch += FinishedSearch;

			sut.StartSearch();
			while (!_finished)
			{
			}

			Assert.That(_mxdContents.Count, Is.EqualTo(7));

			IEnumerable<string> topTexts = _mxdContents.Select(_ => _.Title);
			Assert.True(topTexts.Contains("Title of MapDocument1"));
			Assert.True(topTexts.Contains("Title of MapDocumentA1"));
			Assert.True(topTexts.Contains("Title of MapDocumentAA1"));
			Assert.True(topTexts.Contains("Title of MapDocumentAA2"));
			Assert.True(topTexts.Contains("Title of MapDocumentAB1"));
			Assert.True(topTexts.Contains("Title of MapDocumentAB2"));
			Assert.True(topTexts.Contains("Title of MapDocumentB1"));

			IEnumerable<string> bottomTexts = _mxdContents.Select(_ => _.Subject);
			Assert.True(bottomTexts.Contains("Summary of MapDocument1"));
			Assert.True(bottomTexts.Contains("Summary of MapDocumentA1"));
			Assert.True(bottomTexts.Contains("Summary of MapDocumentAA1"));
			Assert.True(bottomTexts.Contains("Summary of MapDocumentAA2"));
			Assert.True(bottomTexts.Contains("Summary of MapDocumentAB1"));
			Assert.True(bottomTexts.Contains("Summary of MapDocumentAB2"));
			Assert.True(bottomTexts.Contains("Summary of MapDocumentB1"));

			IEnumerable<string> tooltipTexts = _mxdContents.Select(_ => _.Comments);
			Assert.True(tooltipTexts.Contains("Description of MapDocument1"));
			Assert.True(tooltipTexts.Contains("Description of MapDocumentA1"));
			Assert.True(tooltipTexts.Contains("Description of MapDocumentAA1"));
			Assert.True(tooltipTexts.Contains("Description of MapDocumentAA2"));
			Assert.True(tooltipTexts.Contains("Description of MapDocumentAB1"));
			Assert.True(tooltipTexts.Contains("Description of MapDocumentAB2"));
			Assert.True(tooltipTexts.Contains("Description of MapDocumentB1"));

			foreach (MxdContent mxdContent in _mxdContents)
			{
				Assert.That(mxdContent.Bitmap.Size.Width, Is.GreaterThan(0));
				Assert.That(mxdContent.Bitmap.Size.Height, Is.GreaterThan(0));
			}
		}

		[Test]
		[STAThread]
		[Explicit]
		public void SmokeTest()
		{
			//var sut = new MapDocumentReaderAdapter();
			//sut.DataReaded += HandleDataReaded;
			//sut.ImageReaded += HandleImageReaded;
			//sut.ReadingCompleted += HandleReadingCompleted;

			//IEnumerable<MapDocumentConfigItem> uris = ConfigReaderUtil.GetMapDocumentUris(null);
			//sut.Read(uris);
			//while (!_finished)
			//{
			//}
		}
	}
}
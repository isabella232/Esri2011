using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using EsriDE.Samples.ContentFinder.ContentAdapter.Mxd;
using EsriDE.Samples.ContentFinder.DomainModel;
using NUnit.Framework;

namespace EsriDE.Samples.ContentFinder.ContentAdapter.Tests
{
	// ReSharper disable InconsistentNaming
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

		

		private void FinishedSearch()
		{
			_finished = true;
		}

		private void FoundContent(Content content)
		{
			Console.WriteLine("ThreadID=" + Thread.CurrentThread.ManagedThreadId);
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
			var s = TestDataUtils.GetMapDocumentConfigItemsForSingleFolderNonRecursiv();
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
		public void ReadingTestMxd_SingleFolderRecursive_Runs()
		{
			// Arrange
			var sourceBundle = TestDataUtils.GetMapDocumentConfigItemsForSingleFolderRecursiv();
			var sut = new MxdContentLocator(sourceBundle);
			sut.FoundContent += FoundContent;
			sut.FinishedSearch += FinishedSearch;

			// Act
			sut.StartSearch();
			while (!_finished)
			{
			}

			// Assert
			Assert.That(_mxdContents.Count, Is.EqualTo(7));

			var topTexts = _mxdContents.Select(_ => _.Title);
			Assert.True(topTexts.Contains("Title of MapDocument1"));
			Assert.True(topTexts.Contains("Title of MapDocumentA1"));
			Assert.True(topTexts.Contains("Title of MapDocumentAA1"));
			Assert.True(topTexts.Contains("Title of MapDocumentAA2"));
			Assert.True(topTexts.Contains("Title of MapDocumentAB1"));
			Assert.True(topTexts.Contains("Title of MapDocumentAB2"));
			Assert.True(topTexts.Contains("Title of MapDocumentB1"));

			var bottomTexts = _mxdContents.Select(_ => _.Subject);
			Assert.True(bottomTexts.Contains("Summary of MapDocument1"));
			Assert.True(bottomTexts.Contains("Summary of MapDocumentA1"));
			Assert.True(bottomTexts.Contains("Summary of MapDocumentAA1"));
			Assert.True(bottomTexts.Contains("Summary of MapDocumentAA2"));
			Assert.True(bottomTexts.Contains("Summary of MapDocumentAB1"));
			Assert.True(bottomTexts.Contains("Summary of MapDocumentAB2"));
			Assert.True(bottomTexts.Contains("Summary of MapDocumentB1"));

			var tooltipTexts = _mxdContents.Select(_ => _.Comments);
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
	}
	// ReSharper restore InconsistentNaming
}
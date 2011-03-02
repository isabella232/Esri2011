using System;
using System.Collections.Generic;
using System.Threading;
using EsriDE.Samples.ContentFinder.ContentAdapter.Mxd;
using EsriDE.Samples.ContentFinder.DomainModel;
using NUnit.Framework;

namespace EsriDE.Samples.ContentFinder.ContentAdapter.Tests
{
	[TestFixture]
	public class ContentLocatorFixture : FixtureBase
	{
		private static volatile IList<int> _threadIds = new List<int>();
		private object _lockObject = new object();

		private int _runningLocatorCount;

		[Test]
		public void Running100Times_TwoLocators_SpawnsTwoThreads()
		{
			for (int i = 0; i < 100; i++)
			{
				_threadIds = new List<int>();
				Running_TwoLocators_SpawnsTwoThreads();
			}
		}

		[Test]
		public void Running_TwoLocators_SpawnsTwoThreads()
		{
			//var sourceBundles = TestDataUtils.GetTwoSourceBundlesForTestData();
			//var locators = 
			var firstSourceBundle = TestDataUtils.GetMapDocumentConfigItemsForSingleFolderRecursiv();
			var firstLocator = new MxdContentLocator(firstSourceBundle);
			firstLocator.FoundContent += TrackSourceThread;
			firstLocator.FinishedSearch += TrackSearchFinish;

			var secondSourceBundle = TestDataUtils.GetMapDocumentConfigItemsForSingleFolderNonRecursiv();
			var secondLocator = new MxdContentLocator(secondSourceBundle);
			secondLocator.FoundContent += TrackSourceThread;
			secondLocator.FinishedSearch += TrackSearchFinish;

			_runningLocatorCount = 2;
			firstLocator.StartSearch();
			secondLocator.StartSearch();

			while (0 != _runningLocatorCount)
			{
			}

			Assert.That(_threadIds.Count, Is.EqualTo(2));
		}

		private void TrackSearchFinish()
		{
			lock (_lockObject)
			{
				_runningLocatorCount--;
			}
		}

		private void TrackSourceThread(Content content)
		{
			Console.WriteLine(content.Title);
			lock (_lockObject)
			{
				int i = Thread.CurrentThread.ManagedThreadId;
				if (!_threadIds.Contains(i))
				{
					Console.WriteLine("ThreadID: " + i);
					_threadIds.Add(i);
				}
			}
		}
	}
}
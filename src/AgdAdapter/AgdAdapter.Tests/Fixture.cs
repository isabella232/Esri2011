using System.Windows.Threading;
using NUnit.Framework;

namespace EsriDE.Samples.ContentFinder.AgdAdapter.Tests
{
	// ReSharper disable InconsistentNaming

	[TestFixture]
	public class Fixture
	{
		[Test]
		public void Creating_ContentFinderButton_Works()
		{
			var sut = new ContentFinderButton();
			Assert.That(sut, !Is.Null, "ContentFinderButton konnte nicht erzeugt werden.");
		}

		[Test]
		[RequiresSTA]
		public void Clicking_ContentFinderButton_TogglesForm()
		{
			var sut = new FakedContentFinderButton();

			sut.EmulateClick();

			Dispatcher.CurrentDispatcher.InvokeShutdown();
		}
	}

	// ReSharper restore InconsistentNaming
}

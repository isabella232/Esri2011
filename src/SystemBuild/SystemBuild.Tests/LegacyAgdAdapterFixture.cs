using System.Windows.Threading;
using EsriDE.Samples.ContentFinder.LegacyAgdAdapter;
using Moq;
using NUnit.Framework;
using Rhino.Mocks;
using MockRepository = Rhino.Mocks.MockRepository;

//using NUnit.Framework.SyntaxHelpers;

namespace EsriDE.Samples.ContentFinder.SystemBuild.Tests
{
	// ReSharper disable InconsistentNaming

	[TestFixture]
	public class LegacyAgdAdapterFixture
	{
		[Test]
		[Explicit]
		public void MoqTest()
		{
			var app = new Mock<IApplicationAndMxApplication>();
			app.Setup(a => a.hWnd).Returns(0);

			var v = new ContentFinderButton();
			v.OnCreate(app);

			Assert.That(v, !Is.Null);
		}

		[Test]
		[RequiresSTA]
		public void SmokeTest()
		{
			var mockRepository = new MockRepository();
			var app = mockRepository.StrictMock<IApplicationAndMxApplication>();

			With.Mocks(mockRepository)
				.Expecting(delegate { SetupResult.For(app.hWnd).Return(0); })
				.Verify(delegate
				{
					var v = new ContentFinderButton();
					v.OnCreate(app);

					Assert.That(v, !Is.Null);

					v.OnClick();
				});

			Dispatcher.CurrentDispatcher.InvokeShutdown();
		}

		[Test]
		[RequiresSTA]
		public void SmokeTest2()
		{
			var mockRepository = new MockRepository();
			var app = mockRepository.StrictMock<IApplicationAndMxApplication>();

			With.Mocks(mockRepository)
				.Expecting(delegate { SetupResult.For(app.hWnd).Return(0); })
				.Verify(delegate
				{
					var v = new ContentFinderButton();
					v.OnCreate(app);

					Assert.That(v, !Is.Null);

					v.OnClick();
				});

			while (true)
			{
				
			}

			Dispatcher.CurrentDispatcher.InvokeShutdown();
		}
	}

	// ReSharper restore InconsistentNaming
}
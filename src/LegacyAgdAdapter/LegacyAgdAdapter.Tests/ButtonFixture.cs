using Moq;
using NUnit.Framework;
using Rhino.Mocks;
using MockRepository = Rhino.Mocks.MockRepository;

namespace EsriDE.Samples.ContentFinder.LegacyAgdAdapter.Tests
{
	// ReSharper disable InconsistentNaming

	[TestFixture]
	public class ButtonFixture
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
		public void RhinoMocksTest()
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
		}
	}

	// ReSharper restore InconsistentNaming
}
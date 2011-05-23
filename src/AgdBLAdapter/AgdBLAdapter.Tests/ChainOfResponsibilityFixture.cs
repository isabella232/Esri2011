using Castle.MicroKernel.Registration;
using Castle.Windsor;
using EsriDE.Commons.Patterns;
using EsriDE.Samples.ContentFinder.AgdBLAdapter;
using EsriDE.Samples.ContentFinder.ApplicationAdapter.Contract;
using EsriDE.Samples.ContentFinder.DomainModel;
using NUnit.Framework;
using Rhino.Mocks;

namespace AgdBLAdapter.Tests
{
	// ReSharper disable InconsistentNaming

	[TestFixture]
	public class ChainOfResponsibilityFixture
	{
		[Test]
		public void BuildChainOfResponsibility_Works()
		{
			var container = new WindsorContainer();

			container.Register(Component.For<IChainOfResponsibilityHandler<Content>>().ImplementedBy<ApplicationMxdCorHandler>().Named("mxd").Parameters(Parameter.ForKey("successor").Eq("${ags}")));
			container.Register(Component.For<IChainOfResponsibilityHandler<Content>>().ImplementedBy<ApplicationAgsCorHandler>().Named("ags").Parameters(Parameter.ForKey("successor").Eq("${Ende}")));
			container.Register(Component.For<IChainOfResponsibilityHandler<Content>>().ImplementedBy<EndApplicationCorHandler>().Named("Ende"));

			container.Register(Component.For<IContentProcessorAdapter>().ImplementedBy<ContentProcessorAdapter>());

			var applicationAdapter = Isolate.Fake.Instance<IApplicationAdapter>();
			container.Register(Component.For<IApplicationAdapter>().Instance(applicationAdapter));

			var v = container.Resolve<IContentProcessorAdapter>();
			Assert.That(v, Is.Not.Null);
		}

		[Test]
		public void FakingWholeObjectTreeWithTypemock()
		{
			var applicationAdapter = Isolate.Fake.Instance<IApplicationAdapter>();
			var application = applicationAdapter.Application;

			Assert.That(application, Is.Not.Null);
		}

		[Test]
		public void FakingSingleObjectsWithRhinoMocks()
		{
			var mockRepository = new MockRepository();
			var applicationAdapter = mockRepository.StrictMock<IApplicationAdapter>();

			With.Mocks(mockRepository)
				.Expecting(() => SetupResult.For(applicationAdapter.Application).Return(null))
				.Verify(delegate
				        	{
				        		var application = applicationAdapter.Application;
				        		Assert.That(application, Is.Null);
				        	});
		}
	}

	// ReSharper restore InconsistentNaming
}

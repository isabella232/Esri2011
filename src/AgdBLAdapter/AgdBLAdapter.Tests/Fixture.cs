using Castle.MicroKernel.Registration;
using Castle.Windsor;
using EsriDE.Samples.ContentFinder.DomainModel;
using NUnit.Framework;

//using NUnit.Framework.SyntaxHelpers;

namespace AgdBLAdapter.Tests
{
	// ReSharper disable InconsistentNaming

	[TestFixture]
	public class Fixture
	{
		[Test]
		public void Do()
		{
			var container = new WindsorContainer();
			//container.Register(Component.For<ICorHandler<Content>>().ImplementedBy())
		}
	}

	// ReSharper restore InconsistentNaming
}

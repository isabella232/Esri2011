using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NUnit.Framework;

namespace EsriDE.Trials.CastleWindsor.Primitives
{
	[TestFixture]
	public class Fixture
	{
		private IWindsorContainer _container;

		[TestFixtureSetUp]
		public void TestFixtureSetUp()
		{
			_container = new WindsorContainer();
			//_container.AddFacility<FactorySupportFacility>();
			_container.Register(Component.For<IAbc>().ImplementedBy<Abc>());
			_container.Register(Component.For<IXyz>().ImplementedBy<Xyz>());
			_container.Register(Component.For<IA>().ImplementedBy<A>());
			_container.Register(Component.For<IB>().ImplementedBy<B>());
			_container.Register(Component.For<IC>().ImplementedBy<C>());
			//_container.Register(AllTypes.FromThisAssembly()
		}

		[Test]
		public void Resolving()
		{
			var o = _container.Resolve<IAbc>();
			Assert.That(o, !Is.Null, "Could not resolve IAbc");
		}
	}
}
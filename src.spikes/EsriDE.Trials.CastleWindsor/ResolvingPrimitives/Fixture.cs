using Castle.Facilities.FactorySupport;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using EsriDE.Trials.CastleWindsor.ResolvingPrimitives.Contracts;
using EsriDE.Trials.CastleWindsor.ResolvingPrimitives.Implementations;
using NUnit.Framework;

namespace EsriDE.Trials.CastleWindsor.ResolvingPrimitives
{
	// ReSharper disable InconsistentNaming
	[TestFixture]
	public class Fixture
	{
		private IWindsorContainer _container;

		[Test]
		public void ResolveWithFluentSyntax()
		{
			InitializeContainerWithFluentSyntax();

			var o = _container.Resolve<IAbc>();
			Assert.That(o, !Is.Null, "Could not resolve IAbc");
		}

		private void InitializeContainerWithFluentSyntax()
		{
			_container = new WindsorContainer();
			_container.Register(Component.For<IAbc>().ImplementedBy<Abc>());
			_container.Register(Component.For<IXyz>().ImplementedBy<Xyz>());
			_container.Register(Component.For<IA>().ImplementedBy<A>());
			_container.Register(Component.For<IB>().ImplementedBy<B>());
			_container.Register(Component.For<IC>().ImplementedBy<C>());
		}

		[Test]
		public void ResolveWithRegisterAllTypesFromThisAssembly()
		{
			InitializeContainerWithRegisterAllTypesFromThisAssembly();

			var o = _container.Resolve<IAbc>();
			Assert.That(o, !Is.Null, "Could not resolve IAbc");
		}

		private void InitializeContainerWithRegisterAllTypesFromThisAssembly()
		{
			_container = new WindsorContainer();
			//_container.Register(AllTypes.FromThisAssembly().Pick());
			_container.Register(
				AllTypes
					.FromThisAssembly()
					.IncludeNonPublicTypes()//because we have internal classes!
					.Where(type => type.Namespace.StartsWith(
						"EsriDE.Trials.CastleWindsor.ResolvingPrimitives"))
					.WithService
					.FirstInterface());
		}

		[Test]
		public void ResolveWithFactorySupportFacility()
		{
			return;
			_container.AddFacility<FactorySupportFacility>();
		}

		[Test]
		public void ResolvingSingletonOver2DistinctInterfaces()
		{
			_container = new WindsorContainer();
			_container.Register(Component.For<IA, IB>().ImplementedBy<AundB>());

			var a = _container.Resolve<IA>();
			var b = _container.Resolve<IB>();

			Assert.That(a, Is.EqualTo(b), "Different singletons.");
			Assert.That(a, Is.SameAs(b), "Different singletons.");
			Assert.AreSame(a, b);
		}

		[Test]
		public void ResolvingSingletonOver2DistinctInterfacesByRegisteringInstancesDoesNotWork()
		{
			var o = new AundB();

			_container = new WindsorContainer();

			_container.Register(Component.For(typeof(IA)).Instance(o));
			Assert.Throws<ComponentRegistrationException>(() => _container.Register(Component.For(typeof(IB)).Instance(o)));

			var a = _container.Resolve<IA>();
			Assert.That(a, !Is.Null);

			Assert.Throws<ComponentNotFoundException>(() => _container.Resolve<IB>());
		}
	}
	// ReSharper restore InconsistentNaming
}
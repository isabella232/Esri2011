using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using EsriDE.Trials.CastleWindsor.LifestyleBehaviour.Contracts;
using EsriDE.Trials.CastleWindsor.LifestyleBehaviour.Implementations;
using NUnit.Framework;

namespace EsriDE.Trials.CastleWindsor.LifestyleBehaviour
{
	[TestFixture]
	public partial class Fixture
	{
		// ReSharper disable InconsistentNaming
		[Test]
		[Explicit]
		public void ResolvingWithTrulyTransientChildContainerInUsingScope()
		{
			_container.Register(Component.For<IA>().ImplementedBy<A>());

			using (var childContainer = GetTransientChildContainer(trulyTransient: true))
			{
				_container.AddChildContainer(childContainer);

				var a = _container.Resolve<IA>();
				Assert.That(a, !Is.Null, "Could not resolve IA");

				var b = childContainer.Resolve<IB>();
				Assert.That(b, !Is.Null, "Could not resolve IB");

				//childContainer.Release(b);

				//childContainer.Dispose();
				_container.RemoveChildContainer(childContainer);
			}
			GC.Collect();
			while (true) ;
			//Assert.Throws<AssertionException>(() => _container.Resolve<IB>(), "");
		}

		private IWindsorContainer GetTransientChildContainer(bool trulyTransient)
		{
			if (trulyTransient)
			{
				return GetTrulyTransientChildContainer();
			}
			else
			{
				return GetTransientChildContainer();
			}
		}

		private IWindsorContainer GetTransientChildContainer()
		{
			IWindsorContainer container = new WindsorContainer();
			container.Register(Component.For<IB>().ImplementedBy<B>().LifeStyle.Transient);
			return container;
		}

		private IWindsorContainer GetTrulyTransientChildContainer()
		{
			IWindsorContainer container = new WindsorContainer();
			container.Kernel.ReleasePolicy = new TrulyTransientReleasePolicy();
			container.Register(Component.For<IB>().ImplementedBy<B>().LifeStyle.Custom(typeof(TrulyTransientLifestyleManager)));
			return container;
		}

		//_container.RemoveChildContainer(childContainer);
		//Assert.Throws<ComponentNotFoundException>(() => _container.Resolve<IB>(), "");

		[Test]
		[Explicit]
		public void ResolvingInlineDoesNotReleaseComponent()
		{
			_container.Register(Component.For<IA>().ImplementedBy<A>());
			_container.Register(Component.For<IB>().ImplementedBy<B>().LifeStyle.Transient);

			var a = _container.Resolve<IA>();
			Assert.That(a, !Is.Null, "Could not resolve IA");

			var b = _container.Resolve<IB>();
			Assert.That(b, !Is.Null, "Could not resolve IB");

			GC.Collect();
			while (true) ;
		}

		[Test]
		[Explicit]
		public void ResolvingPartionalInline()
		{
			_container.Register(Component.For<IA>().ImplementedBy<A>());

			//_container.Register(Component.For<IB>().ImplementedBy<B>().LifeStyle.Custom(typeof(TrulyTransientLifestyleManager)));
			_container.Register(Component.For<IB>().ImplementedBy<B>().LifeStyle.Transient);

			var a = _container.Resolve<IA>();
			Assert.That(a, !Is.Null, "Could not resolve IA");

			ResolvingPartionalInline_NotInlinePart();

			GC.Collect();
			while (true) ;
			//Assert.Throws<AssertionException>(() => _container.Resolve<IB>(), "");
		}

		private void ResolvingPartionalInline_NotInlinePart()
		{
			var b = _container.Resolve<IB>();
			Assert.That(b, !Is.Null, "Could not resolve IB");
			bool x;
			b.SetAction(_ => x = _);
		}

		[Test]
		public void ResolvingNotInline()
		{
			_container.Register(Component.For<IA>().ImplementedBy<A>());
			_container.Register(Component.For<IB>().ImplementedBy<B>().LifeStyle.Transient);
			ResolvingNotInline_NotInlinePart();

			GC.Collect();
			while (!_fertig1) ;
		}

		private void ResolvingNotInline_NotInlinePart()
		{
			var a = _container.Resolve<IA>();
			Assert.That(a, !Is.Null, "Could not resolve IA");

			var b = _container.Resolve<IB>();
			Assert.That(b, !Is.Null, "Could not resolve IB");
			b.SetAction(_ => _fertig1 = _);
		}
	}
	// ReSharper restore InconsistentNaming
}

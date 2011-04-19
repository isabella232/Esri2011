using System;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NUnit.Framework;

namespace EsriDE.Trials.EmbeddingManager
{
	public partial class FixtureCastle
	{
		[Test]
		public void Resolving_OneFunc_Works()
		{
			var container = new WindsorContainer();
			container.Register(Component.For<Func<int>>().Instance(() => 1));

			var func = container.Resolve<Func<int>>();
			var i = func();
			Assert.AreEqual(1, i);
		}

		[Test]
		public void Registering_SameTwoFuncsWithoutNames_ThrowsException()
		{
			var container = new WindsorContainer();

			container.Register(Component.For<Func<int>>().Instance(() => 1));
			Assert.Throws<ComponentRegistrationException>(() => container.Register(Component.For<Func<int>>().Instance(() => 2)));
		}

		[Test]
		public void Registering_SameTwoFuncsWithDifferentNames_Works()
		{
			RegisteringSameTwoFuncsWithDifferentNames();
		}

		[Test]
		public void Resolving_SameTwoFuncsWithoutNames_FirstFuncIsTaken()
		{
			var container = RegisteringSameTwoFuncsWithDifferentNames();

			// the first registered func is the "default"-func
			var func = container.Resolve<Func<int>>();
			var i = func();
			Assert.AreEqual(1, i);
		}

		[Test]
		public void Resolving_SameTwoFuncsWithNames_Works()
		{
			var container = RegisteringSameTwoFuncsWithDifferentNames();

			var func = container.Resolve<Func<int>>("First");
			var i = func();
			Assert.AreEqual(1, i);

			func = container.Resolve<Func<int>>("Second");
			i = func();
			Assert.AreEqual(2, i);
		}

		[Test]
		public void Registering_WithServiceOverride_Works()
		{
			var container = RegisteringSameTwoFuncsWithDifferentNames();

			container.Register(Component.For<Something>().DependsOn(ServiceOverride.ForKey("myFunc").Eq("Second")));

			var s = container.Resolve<Something>();

			Assert.AreEqual(2, s.I);
		}

		private WindsorContainer RegisteringSameTwoFuncsWithDifferentNames()
		{
			var container = new WindsorContainer();

			container.Register(Component.For<Func<int>>().Instance(() => 1).Named("First"));
			container.Register(Component.For<Func<int>>().Instance(() => 2).Named("Second"));

			return container;
		}
	}
}
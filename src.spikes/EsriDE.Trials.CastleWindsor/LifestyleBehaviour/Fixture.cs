using System;
using System.Diagnostics;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NUnit.Framework;

namespace EsriDE.Trials.CastleWindsor.LifestyleBehaviour
{
	[TestFixture]
	public class Fixture
	{
		#region Setup/Teardown
		[SetUp]
		public void Setup()
		{
			_fertig = false;
			_fertig2 = false;
		}
		#endregion

		[TestFixtureSetUp]
		public void TestFixtureSetUp()
		{
			_container = new WindsorContainer();
			_container.Register(Component.For<IA>().ImplementedBy<A>());
		}

		private IWindsorContainer _container;
		private bool _fertig;
		private bool _fertig2;

		private void Do(int count)
		{
			var veryFirst = GC.GetTotalMemory(true);

			int i = 0;
			do
			{
				i++;
				var before = GC.GetTotalMemory(true);
				WorkingWithForms_DoSomething();
				var after = GC.GetTotalMemory(true);

				GC.Collect();
				var afterGC = GC.GetTotalMemory(true);

				//Console.WriteLine("*** before =" + before.ToString());
				//Console.WriteLine("*** after  =" + after.ToString());
				//Console.WriteLine("*** afterGC=" + afterGC.ToString());
			} while (i < count);

			GC.Collect();
			var veryLast = GC.GetTotalMemory(true);

			Trace.WriteLine("*** veryFirst=" + veryFirst);
			Trace.WriteLine("*** veryLast =" + veryLast);
			var diff = veryLast - veryFirst;
			Trace.WriteLine("*** diff     =" + diff);
		}

		private void WorkingWithForms_DoSomething()
		{
			Console.WriteLine("********* Using Variante *********");
			using (var f2 = (IDisposable) _container.Resolve<IView>())
			{
				Assert.That(f2, !Is.Null, "Could not resolve IView");
				var f3 = (IView) f2;

				f3.ShowView(_ => _fertig2 = _);
				f3.CloseView();
			}

			Console.WriteLine("********* Nicht-Using Variante *********");
			var f = _container.Resolve<IView>();
			Assert.That(f, !Is.Null, "Could not resolve IView");

			f.ShowView(_ => _fertig = _);
			f.CloseView();
		}

		private IWindsorContainer GetTransientChildContainer()
		{
			IWindsorContainer container = new WindsorContainer();
			container.Register(Component.For<IB>().ImplementedBy<B>().LifeStyle.Transient);
			return container;
		}

		private void Resolving0_DoSomething()
		{
			var a = _container.Resolve<IA>();
			Assert.That(a, !Is.Null, "Could not resolve IA");

			var b = _container.Resolve<IB>();
			Assert.That(b, !Is.Null, "Could not resolve IB");
			b.SetAction(_ => _fertig = _);
		}

		private IWindsorContainer GetTrulyTransientChildContainer()
		{
			IWindsorContainer container = new WindsorContainer();
			container.Kernel.ReleasePolicy = new TrulyTransientReleasePolicy();
			container.Register(Component.For<IB>().ImplementedBy<B>().LifeStyle.Custom(typeof (TrulyTransientLifestyleManager)));
			return container;
		}

		//_container.RemoveChildContainer(childContainer);
		//Assert.Throws<ComponentNotFoundException>(() => _container.Resolve<IB>(), "");

		private void Resolving2_DoSomething()
		{
			var b = _container.Resolve<IB>();
			Assert.That(b, !Is.Null, "Could not resolve IB");
			bool x;
			b.SetAction(_ => x = _);
		}

		private void DestructorIsCalled_DoSomething()
		{
			var b = new B();
			Console.WriteLine("b is created. b=" + b);
			bool x;
			b.SetAction(_ => x = _);
		}

		[Test]
		public void DestructorIsCalled()
		{
			DestructorIsCalled_DoSomething();
			GC.Collect();

			while (true) ;
		}

		[Test]
		public void InlineResolvingDoesNotWork()
		{
			//_container.Register(Component.For<IB>().ImplementedBy<B>().LifeStyle.Custom(typeof(TrulyTransientLifestyleManager)));
			_container.Register(Component.For<IB>().ImplementedBy<B>().LifeStyle.Transient);

			var a = _container.Resolve<IA>();
			Assert.That(a, !Is.Null, "Could not resolve IA");

			var b = _container.Resolve<IB>();
			Assert.That(b, !Is.Null, "Could not resolve IB");

			GC.Collect();
			while (true) ;
		}

		[Test]
		public void Resolving()
		{
			using (var childContainer = GetTrulyTransientChildContainer())
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

		[Test]
		public void Resolving0()
		{
			_container.Register(Component.For<IB>().ImplementedBy<B>().LifeStyle.Transient);
			Resolving0_DoSomething();

			GC.Collect();
			while (!_fertig) ;
		}

		[Test]
		public void Resolving2()
		{
			//_container.Register(Component.For<IB>().ImplementedBy<B>().LifeStyle.Custom(typeof(TrulyTransientLifestyleManager)));
			_container.Register(Component.For<IB>().ImplementedBy<B>().LifeStyle.Transient);

			var a = _container.Resolve<IA>();
			Assert.That(a, !Is.Null, "Could not resolve IA");

			Resolving2_DoSomething();

			GC.Collect();
			while (true) ;
			//Assert.Throws<AssertionException>(() => _container.Resolve<IB>(), "");
		}

		[Test]
		public void TestForm()
		{
			using (var f = new SomeForm())
			{
				f.Show();
			}
		}

		[Test]
		public void WorkingWithForms()
		{
			_container.Kernel.ReleasePolicy = new TrulyTransientReleasePolicy();
			_container.Register(
				Component.For<IView>().ImplementedBy<SomeForm>().LifeStyle.Custom(typeof (TrulyTransientLifestyleManager)));
			//_container.Register(Component.For<IView>().ImplementedBy<SomeForm>().LifeStyle.Transient);

			for (int i = 0; i < 10; i++)
			{
				Trace.WriteLine("10");
				Do(10);
				Trace.WriteLine("100");
				Do(100);
				//Trace.WriteLine("1000");
				//Do(1000);
			}
			//while (!_fertig && !_fertig2);
		}
	}
}
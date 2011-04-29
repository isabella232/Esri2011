using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NUnit.Framework;

//using NUnit.Framework.SyntaxHelpers;

namespace EsriDE.Trials.CastleWindsor.ResolvingWithSetterInjection
{
	// ReSharper disable InconsistentNaming

	[TestFixture]
	public class Fixture
	{
		[Test]
		public void Do_Foo()
		{
			var container = new WindsorContainer();

			container.Register(Component.For<IFoo>().ImplementedBy<Foo>());
			container.Register(Component.For<ISomething>().ImplementedBy<Something>());

			var v = container.Resolve<IFoo>();
			
			//var s = v.

		}

		[Test]
		public void Do_Foo2()
		{
			var container = new WindsorContainer();

			container.Register(Component.For<IFoo2>().ImplementedBy<Foo2>());
			container.Register(Component.For<ISomething>().ImplementedBy<Something>());

			var v = container.Resolve<IFoo2>();

			//var s = v.

		}
	}

	// ReSharper restore InconsistentNaming
}

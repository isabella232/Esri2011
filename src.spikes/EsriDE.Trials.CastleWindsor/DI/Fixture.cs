using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NUnit.Framework;

//using NUnit.Framework.SyntaxHelpers;

namespace EsriDE.Trials.CastleWindsor.DI
{
	// ReSharper disable InconsistentNaming

	[TestFixture]
	public class Fixture
	{
		[Test]
		public void ResolvingDependencys_Works()
		{
			var container = new WindsorContainer();

			container.Register(Component.For<ContractA>().ImplementedBy<ImplementationA>());
			container.Register(Component.For<ContractB>().ImplementedBy<ImplementationB>());
			container.Register(Component.For<ContractC>().ImplementedBy<ImplementationC>());
			container.Register(Component.For<ContractD>().ImplementedBy<ImplementationD>());
			container.Register(Component.For<ContractE>().ImplementedBy<ImplementationE>());

			var componentA = container.Resolve<ContractA>();

			Assert.That(componentA, Is.Not.Null);
		}
	}

	// ReSharper restore InconsistentNaming
}

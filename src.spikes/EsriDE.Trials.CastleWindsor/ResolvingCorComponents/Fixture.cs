using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using EsriDE.Trials.CastleWindsor.ResolvingCorComponents.Cor;
using NUnit.Framework;

namespace EsriDE.Trials.CastleWindsor.ResolvingCorComponents
{
	// ReSharper disable InconsistentNaming

	[TestFixture]
	public class Fixture
	{
		[Test]
		public void Configure_WithXml()
		{
			var container = new WindsorContainer(@"ResolvingCorComponents\CastleWindsor.config");

			var processor = container.Resolve<ContentProcessor>();
			Assert.That(processor, !Is.Null);

			Console.WriteLine("Run with Do_Xml");
			processor.Process("Do_Xml");

			Console.WriteLine("Run with ArcMapMxdContentProcessor");
			processor.Process("ArcMapMxdContentProcessor");

			Console.WriteLine("Run with ArcMapAgsContentProcessor");
			processor.Process("ArcMapAgsContentProcessor");
		}

		[Test]
		public void Configure_WithFluid()
		{
			var container = new WindsorContainer();
			container.Register(Component.For<IApplication>().ImplementedBy<Application>());

			//container.Register(Component.For<ContentProcessor>().ImplementedBy<ArcMapMxdContentProcessor>().DependsOn(Property.ForKey("Key1").Eq(1)));
			//container.Register(Component.For<ContentProcessor>().ImplementedBy<ArcMapAgsContentProcessor>().DependsOn(Property.ForKey("Key2").Eq(2)));
			//var v = container.Resolve<ContentProcessor>("Key1");

			//geht komischerweise auch einfach ohne die Keys in der Reihenfolge der Registrierungen
			container.Register(Component.For<IContentProcessor>().ImplementedBy<TestMasterContentProcessor>());
			container.Register(Component.For<ContentProcessor>().ImplementedBy<ArcMapMxdContentProcessor>());
			container.Register(Component.For<ContentProcessor>().ImplementedBy<ArcMapAgsContentProcessor>());
			container.Register(Component.For<ContentProcessor>().ImplementedBy<EndProcessor>());

			var processor = container.Resolve<ContentProcessor>();
			Assert.That(processor, !Is.Null);

			Console.WriteLine("Run with Do_Xml");
			processor.Process("Do_Xml");

			Console.WriteLine("Run with ArcMapMxdContentProcessor");
			processor.Process("ArcMapMxdContentProcessor");

			Console.WriteLine("Run with ArcMapAgsContentProcessor");
			processor.Process("ArcMapAgsContentProcessor");		}
	}

	// ReSharper restore InconsistentNaming
}

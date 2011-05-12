using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NUnit.Framework;

namespace EsriDE.Trials.ChainOfResponsibility
{
	// ReSharper disable InconsistentNaming

	[TestFixture]
	public class IocProcessingOrderSpike
	{
		[Test]
		public void Processing_AscendingOrderWithoutResolvingKey_ReturnsTwoProcessings()
		{
			var container = new WindsorContainer();

			container.Register(
				Component.For<ICorHandler<IData>>().ImplementedBy<CorHandlerOne>().Named("one").Parameters(
					Parameter.ForKey("nextLink").Eq("${two}")));
			container.Register(Component.For<ICorHandler<IData>>().ImplementedBy<CorHandlerTwo>().Named("two"));

			var corHandler = container.Resolve<ICorHandler<IData>>();

			IData data = new DemoDataOne();
			corHandler.Process(data);

			Console.WriteLine("***");

			data = new DemoDataTwo();
			corHandler.Process(data);

			/*
			This is for 'CorHandlerOne' 
			*** 
			This is for 'CorHandlerTwo'
			*/
		}

		[Test]
		public void Processing_DescendingOrderWithoutResolvingKey_ReturnsOneNoProcessingAndOneProcessing()
		{
			var container = new WindsorContainer();

			container.Register(Component.For<ICorHandler<IData>>().ImplementedBy<CorHandlerTwo>().Named("two"));
			container.Register(
				Component.For<ICorHandler<IData>>().ImplementedBy<CorHandlerOne>().Named("one").Parameters(
					Parameter.ForKey("nextLink").Eq("${two}")));

			var corHandler = container.Resolve<ICorHandler<IData>>();

			IData data = new DemoDataOne();
			corHandler.Process(data);

			Console.WriteLine("***");

			data = new DemoDataTwo();
			corHandler.Process(data);

			/*
			End from chain of responsibility reached.  
			Type info='EsriDE.Trials.ChainOfResponsibility.CorHandler`1[T]' 
			*** 
			This is for 'CorHandlerTwo'
			*/
		}

		[Test]
		public void Processing_DescendingOrderWithResolvingKey_ReturnsTwoProcessings()
		{
			var container = new WindsorContainer();

			container.Register(Component.For<ICorHandler<IData>>().ImplementedBy<CorHandlerTwo>().Named("two"));
			container.Register(
				Component.For<ICorHandler<IData>>().ImplementedBy<CorHandlerOne>().Named("one").Parameters(
					Parameter.ForKey("nextLink").Eq("${two}")));

			var corHandler = container.Resolve<ICorHandler<IData>>("one");

			IData data = new DemoDataOne();
			corHandler.Process(data);

			Console.WriteLine("***");

			data = new DemoDataTwo();
			corHandler.Process(data);

			/*
			This is for 'CorHandlerOne' 
			*** 
			This is for 'CorHandlerTwo'
			*/
		}
	}

	// ReSharper restore InconsistentNaming
}
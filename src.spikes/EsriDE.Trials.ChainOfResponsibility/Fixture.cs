using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NUnit.Framework;

//using NUnit.Framework.SyntaxHelpers;

namespace EsriDE.Trials.ChainOfResponsibility
{
	// ReSharper disable InconsistentNaming

	[TestFixture]
	public class Fixture
	{
		[Test]
		public void Do()
		{
			var container = new WindsorContainer();

			container.Register(Component.For<ICorHandler<IData>>().ImplementedBy<CorHandlerTwo>().Named("two"));
			container.Register(Component.For<ICorHandler<IData>>().ImplementedBy<CorHandlerOne>().Named("one").Parameters(Parameter.ForKey("nextLink").Eq("${two}")));

			var corHandler = container.Resolve<ICorHandler<IData>>("one");

			IData data = new DemoDataOne();
			corHandler.Process(data);

			Console.WriteLine("***");

			data = new DemoDataTwo();
			corHandler.Process(data);

			//Func<IData, bool> func = delegate(IData data)
			//                            {
			//                                if (data.GetType() == type)
			//                                {
			//                                    return true;
			//                                }
			//                                else return false;
			//                            }
			//container.Register(Component.For<ICorHandler<IData>>().ImplementedBy())
		}
	}

	// ReSharper restore InconsistentNaming
}

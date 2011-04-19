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
		public void Resolving_OneFuncWithInjection_Works()
		{
			var container = new WindsorContainer();

			container.Register(Component.For<Func<int>>().Instance(() => 1));
			container.Register(Component.For<Something>());
			
			var s = container.Resolve<Something>();

			Assert.AreEqual(1, s.I);
		}

		

		
	}
}
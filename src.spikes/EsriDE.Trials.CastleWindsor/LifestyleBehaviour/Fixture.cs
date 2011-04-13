using System;
using System.Diagnostics;
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
		private IWindsorContainer _container;

		private bool _fertig1;
		private bool _fertig2;

		#region Setup/Teardown
		[SetUp]
		public void Setup()
		{
			_fertig1 = false;
			_fertig2 = false;
		}

		[TestFixtureSetUp]
		public void TestFixtureSetUp()
		{
			_container = new WindsorContainer();
		}
		#endregion
	}
}
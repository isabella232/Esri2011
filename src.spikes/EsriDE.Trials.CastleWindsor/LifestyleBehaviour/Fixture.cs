using Castle.Windsor;
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
			_container = new WindsorContainer();
		}

		[TestFixtureSetUp]
		public void TestFixtureSetUp()
		{
		}
		#endregion
	}
}
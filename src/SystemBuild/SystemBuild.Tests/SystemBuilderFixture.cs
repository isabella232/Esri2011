using EsriDE.Samples.ContentFinder.BL.Contract;
using NUnit.Framework;

namespace EsriDE.Samples.ContentFinder.SystemBuild.Tests
{
	[TestFixture]
	public class SystemBuilderFixture
	{
		[Test]
		public void BuildingSystem_Works()
		{
			var sut = new SystemBuilder();
			var o = sut.InitializeSystem(typeof (IContentLocatorFilter));
			Assert.That(o, !Is.Null);
		}
	}
}
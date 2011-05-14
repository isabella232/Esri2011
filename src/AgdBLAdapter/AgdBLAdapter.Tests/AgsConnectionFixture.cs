using System;
using EsriDE.Commons.Ags;
using EsriDE.Commons.Testing;
using NUnit.Framework;

namespace EsriDE.Samples.ContentFinder.AgdBLAdapter.Tests
{
	// ReSharper disable InconsistentNaming

	[TestFixture]
	public class AgsConnectionFixture : FixtureBase
	{
		private readonly Uri _uri = new Uri(@"http://server.arcgisonline.com/ArcGIS/rest/services/CSP_Imagery_World_2D/MapServer");

		[Test]
		public void Do()
		{
			var serviceName = AgsUtil.GetServiceName(_uri.AbsoluteUri);

			var layer = ApplicationAgsCorHandler.GetLayerFromMapService(_uri, serviceName);

			Assert.That(layer, Is.Not.Null);
		}
	}

	// ReSharper restore InconsistentNaming
}

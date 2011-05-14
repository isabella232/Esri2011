using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.GISClient;
using EsriDE.Commons.Testing;
using NUnit.Framework;

namespace EsriDE.Samples.ContentFinder.AgdAdapter.Tests
{
	// ReSharper disable InconsistentNaming

	[TestFixture]
	public class ApplicationAgsCorHandlerFixture : FixtureBase
	{
		private const string address = @"http://server.arcgisonline.com/ArcGIS/services";

		[Test]
		public void AgsConnect_Works()
		{
			var connectionFactory = (IAGSServerConnectionFactory2) new AGSServerConnectionFactory();
			IPropertySet connectionProps = new PropertySet();
			connectionProps.SetProperty("URL", address);

			IAGSServerConnection gisServer = connectionFactory.Open(connectionProps, 0);
			Assert.That(gisServer, Is.Not.Null);
		}
	}

	// ReSharper restore InconsistentNaming
}
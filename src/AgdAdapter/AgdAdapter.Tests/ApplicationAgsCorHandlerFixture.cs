using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.GISClient;
using EsriDE.Samples.ContentFinder.AgdBLAdapter;
using NUnit.Framework;

//using NUnit.Framework.SyntaxHelpers;

namespace EsriDE.Samples.ContentFinder.AgdAdapter.Tests
{
	// ReSharper disable InconsistentNaming

	[TestFixture]
	public class ApplicationAgsCorHandlerFixture
	{
		private const string uri = @"http://server.arcgisonline.com/ArcGIS/rest/services/CSP_Imagery_World_2D";

		[Test]
		public void Do()
		{
			var connectionFactory =
				(IAGSServerConnectionFactory2) new AGSServerConnectionFactory();
			IPropertySet connectionProps = new PropertySet();
			connectionProps.SetProperty("URL", uri);

			IAGSServerConnection gisServer = connectionFactory.Open(connectionProps, 0);
			Assert.That(gisServer, Is.Not.Null);
		}
	}

	// ReSharper restore InconsistentNaming
}
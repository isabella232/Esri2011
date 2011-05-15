using System;
using EsriDE.Commons.Ags.Contracts;
using NUnit.Framework;

//using NUnit.Framework.SyntaxHelpers;

namespace EsriDE.Commons.Ags.Tests
{
	// ReSharper disable InconsistentNaming

	[TestFixture]
	public class MapServiceUtilFixture
	{
		private const string address = @"http://server.arcgisonline.com/ArcGIS/rest/services/CSP_Imagery_World_2D/MapServer";

		[Test]
		public void ConnectTo_SampleMapService_Works()
		{
			var uri =
				new Uri(
					@"http://server.arcgisonline.com/ArcGIS/rest/services/Demographics/USA_1990-2000_Population_Change/MapServer");
			var sut = MapServiceUtil.GetMapServiceInfo(uri);

			Assert.That(sut, !Is.Null, "MapService USA 1990-2000 Population konnte nicht angesprochen werden.");
		}

		[Test]
		// Aktion -> Bedingung -> Ergebnis
		public void GettingBitmap_WithValidUri_ReturnsBitmap()
		{
			var uri = new Uri(address);
			var spatialReference = new SpatialReference {wkid = 4326};
			var extent = new Extent {XMin = -179, XMax = 179, YMin = -139, YMax = -139, SpatialReference = spatialReference};

			var sut = MapServiceUtil.GetSysDrawBitmap(uri, extent, @"400,266");

			Assert.That(sut, Is.Not.Null);
		}
	}

	// ReSharper restore InconsistentNaming
}

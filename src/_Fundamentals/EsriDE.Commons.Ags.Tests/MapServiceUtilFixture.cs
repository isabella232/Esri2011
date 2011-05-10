using System;
using NUnit.Framework;

//using NUnit.Framework.SyntaxHelpers;

namespace EsriDE.Commons.Ags.Tests
{
	// ReSharper disable InconsistentNaming

	[TestFixture]
	public class MapServiceUtilFixture
	{
		[Test]
		public void ConnectTo_SampleMapService_Works()
		{
			var uri =
				new Uri(
					@"http://server.arcgisonline.com/ArcGIS/rest/services/Demographics/USA_1990-2000_Population_Change/MapServer");
			var sut = MapServiceUtil.GetMapServiceInfo(uri);

			Assert.That(sut, !Is.Null, "MapService USA 1990-2000 Population konnte nicht angesprochen werden.");
		}
	}

	// ReSharper restore InconsistentNaming
}

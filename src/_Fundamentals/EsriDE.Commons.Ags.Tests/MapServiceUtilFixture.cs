using System;
using System.Text;
using EsriDE.Commons.Ags.Contracts;
using NUnit.Framework;

//using NUnit.Framework.SyntaxHelpers;

namespace EsriDE.Commons.Ags.Tests
{
	// ReSharper disable InconsistentNaming

	[TestFixture]
	public class MapServiceUtilFixture
	{
		[Test]
		public void Spike()
		{
			var uri =
				new Uri(
					@"http://server.arcgisonline.com/ArcGIS/rest/services/Demographics/USA_1990-2000_Population_Change/MapServer");
			var sut = MapServiceUtil.GetMapServiceInfo(uri);
		}
	}

	// ReSharper restore InconsistentNaming
}

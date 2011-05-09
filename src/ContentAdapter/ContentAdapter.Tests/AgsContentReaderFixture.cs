using System;
using EsriDE.Commons.Ags;
using EsriDE.Samples.ContentFinder.ContentAdapter.Ags;
using NUnit.Framework;

namespace EsriDE.Samples.ContentFinder.ContentAdapter.Tests
{
	// ReSharper disable InconsistentNaming

	[TestFixture]
	public class AgsContentReaderFixture
	{
		[Test]
		public void Spike()
		{
			var uri = new Uri(@"http://server.arcgisonline.com/ArcGIS/rest/services/Demographics/USA_1990-2000_Population_Change/MapServer");

			var serviceType = AgsUtil.GetServiceType(uri);
			var reader = AgsContentReader.CreateAgsContentReader(serviceType);

			var content = reader.ReadContent(uri);
			Assert.That(content, !Is.Null);
		}
	}

	// ReSharper restore InconsistentNaming
}

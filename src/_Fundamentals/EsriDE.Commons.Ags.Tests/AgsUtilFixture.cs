using System;
using NUnit.Framework;

namespace EsriDE.Commons.Ags.Tests
{
	// ReSharper disable InconsistentNaming

	[TestFixture]
	public class AgsUtilFixture
	{
		[Test]
		public void GetParentDirectoryName_Works()
		{
			var path = @"http://server.arcgisonline.com/ArcGIS/rest/services/CSP_Imagery_World_2D/MapServer";
			var parentDirectoryName = AgsUtil.GetParentDirectoryName(path);

			Assert.That(parentDirectoryName, Is.EqualTo(@"http://server.arcgisonline.com/ArcGIS/rest/services"));
		}

		[Test]
		[Explicit]
		public void GetParentDirectoryName_Works2()
		{
			var path = @"http://vsdp1001.srvc.esri-de.com/ArcGIS/rest/services/Basemaps/DynamicWorldStreetMap/MapServer";
			var parentDirectoryName = AgsUtil.GetParentDirectoryName(path);

			Assert.That(parentDirectoryName, Is.EqualTo(@"http://vsdp1001.srvc.esri-de.com/ArcGIS/rest/services/Basemaps"));
		}

		[Test]
		public void GetServiceName_Works()
		{
			var path = @"http://server.arcgisonline.com/ArcGIS/rest/services/CSP_Imagery_World_2D/MapServer";
			var serviceName = AgsUtil.GetServiceName(path);

			Assert.That(serviceName, Is.EqualTo(@"CSP_Imagery_World_2D"));
		}

		[Test]
		public void GetServiceDirectory_Works()
		{
			var path = @"http://server.arcgisonline.com/ArcGIS/rest/services/";
			var serviceDirectory = AgsUtil.GetServiceDirectory(path);

			Assert.That(serviceDirectory.Folders.Count, Is.EqualTo(4));
			Assert.That(serviceDirectory.Services.Count, Is.EqualTo(20));
		}

		[Test]
		public void TrimLeadingDirectory_Works()
		{
			var path = @"dir/services";
			var service = AgsUtil.TrimLeadingDirectory(path);

			Assert.That(service, Is.EqualTo(@"services"));
		}

		[Test]
		public void TrimRestFragment_Works()
		{
			var longUri = new Uri(@"http://server.arcgisonline.com/ArcGIS/rest/services/CSP_Imagery_World_2D/MapServer");
			var shortUri = AgsUtil.TrimRestFragment(longUri);

			Assert.That(shortUri.AbsoluteUri, Is.EqualTo(@"http://server.arcgisonline.com/ArcGIS/services/CSP_Imagery_World_2D/MapServer"));
		}

		[Test]
		public void GetSoapRepresantation_Works()
		{
			var longUri = new Uri(@"http://server.arcgisonline.com/ArcGIS/rest/services/CSP_Imagery_World_2D/MapServer");
			var soapRepresentation = AgsUtil.GetSoapRepresentation(longUri);

			Assert.That(soapRepresentation.AbsoluteUri, Is.EqualTo(@"http://server.arcgisonline.com/ArcGIS/services"));
		}
	}

	// ReSharper restore InconsistentNaming
}

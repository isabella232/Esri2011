using System;
using NUnit.Framework;

namespace EsriDE.Commons.Ags.Tests
{
	// ReSharper disable InconsistentNaming

	[TestFixture]
	public class AgsUtilSpike
	{
		[Test]
		public void FindenVonFoldern()
		{
			var rootUri = new Uri("http://server.arcgisonline.com/ArcGIS/rest/services");
			var folderUris = AgsUtil.GetFolderUris(rootUri);
			var count = 0;
			foreach (var folderUri in folderUris)
			{
				count++;
				Console.WriteLine(folderUri);
			}

			Assert.That(count, Is.EqualTo(4), "Anzahl der gefundenen Folder (ago/root) ist unpassend.");
		}

		[Test]
		public void FindenVonServices()
		{
			var count = 0;

			var rootUri = new Uri("http://server.arcgisonline.com/ArcGIS/rest/services");
			var serviceUris = AgsUtil.GetServiceUris(rootUri);
			foreach (var serviceUri in serviceUris)
			{
				count++;
				Console.WriteLine(serviceUri);
			}

			Assert.That(count, Is.EqualTo(20), "Anzahl der gefundenen Services (ago/root) ist unpassend.");

			rootUri = new Uri("http://server.arcgisonline.com/ArcGIS/rest/services/Demographics");
			serviceUris = AgsUtil.GetServiceUris(rootUri);
			foreach (var serviceUri in serviceUris)
			{
				count++;
				Console.WriteLine(serviceUri);
			}

			Assert.That(count, Is.EqualTo(41), "Anzahl der gefundenen Services (ago/demographics) ist unpassend.");

			rootUri = new Uri("http://vsdp1001.srvc.esri-de.com/ArcGIS/rest/services/Basemaps");
			serviceUris = AgsUtil.GetServiceUris(rootUri);
			foreach (var serviceUri in serviceUris)
			{
				count++;
				Console.WriteLine(serviceUri);
			}

			Assert.That(count, Is.EqualTo(51), "Anzahl der gefundenen Services (demo/basemaps) ist unpassend.");
		}
	}

	// ReSharper restore InconsistentNaming
}

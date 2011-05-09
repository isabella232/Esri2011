using System;
using NUnit.Framework;

//using NUnit.Framework.SyntaxHelpers;

namespace EsriDE.Commons.Ags.Tests
{
	// ReSharper disable InconsistentNaming

	[TestFixture]
	public class AgsUtilFixture
	{
		[Test]
		public void FolderErmitteln()
		{
			var rootUri = new Uri("http://server.arcgisonline.com/ArcGIS/rest/services");
			var folderUris = AgsUtil.GetFolderUris(rootUri);
			foreach (var folderUri in folderUris)
			{
				Console.WriteLine(folderUri);
			}

			//var rootUri = new Uri("http://server.arcgisonline.com/ArcGIS/rest/services/Xyz");
			//var folderUris = AgsUtil.GetFolderUris(rootUri);
			//foreach (var folderUri in folderUris)
			//{
			//    Console.WriteLine();
			//}
		}

		[Test]
		public void ServicesErmitteln()
		{
			var rootUri = new Uri("http://server.arcgisonline.com/ArcGIS/rest/services");
			var serviceUris = AgsUtil.GetServiceUris(rootUri);
			foreach (var serviceUri in serviceUris)
			{
				Console.WriteLine(serviceUri);
			}

			rootUri = new Uri("http://server.arcgisonline.com/ArcGIS/rest/services/Demographics");
			serviceUris = AgsUtil.GetServiceUris(rootUri);
			foreach (var serviceUri in serviceUris)
			{
				Console.WriteLine(serviceUri);
			}

			rootUri = new Uri("http://vsdp1001.srvc.esri-de.com/ArcGIS/rest/services/Basemaps");
			serviceUris = AgsUtil.GetServiceUris(rootUri);
			foreach (var serviceUri in serviceUris)
			{
				Console.WriteLine(serviceUri);
			}
		}
	}

	// ReSharper restore InconsistentNaming
}

using System;
using System.Collections.Generic;
using EsriDE.Samples.ContentFinder.ContentAdapter.Ags;
using EsriDE.Samples.ContentFinder.DomainModel;
using NUnit.Framework;

//using NUnit.Framework.SyntaxHelpers;

namespace EsriDE.Samples.ContentFinder.ContentAdapter.Tests
{
	// ReSharper disable InconsistentNaming

	[TestFixture]
	public class AgsConnectionSpike
	{
		private IEnumerable<Source> GetImageSources()
		{
			yield return new Source(new Uri("http://vsdp1001.srvc.esri-de.com/ArcGIS/rest/services/Mosaike"), RecursivityPolicy.Recursiv);
		}

		private IEnumerable<Source> GetMapSources()
		{
			//yield return new Source(new Uri("http://server.arcgisonline.com/ArcGIS/rest/services"), RecursivityPolicy.NotRecursiv);
			yield return new Source(new Uri("http://server.arcgisonline.com/ArcGIS/rest/services/Demographics"), RecursivityPolicy.NotRecursiv);
			//yield return new Source(new Uri("http://vsdp1001.srvc.esri-de.com/ArcGIS/rest/services/Basemaps"), RecursivityPolicy.NotRecursiv);
			yield return new Source(new Uri("http://vsdp1001.srvc.esri-de.com/ArcGIS/rest/services"), RecursivityPolicy.Recursiv);
		}

		[Test]
		public void DoImage()
		{
			var a = new ImageAgsUriAnalyzer();
			var uris = a.GetRecursivUris(GetImageSources());

			foreach (var uri in uris)
			{
				Console.WriteLine(uri);
			}
		}

		[Test]
		public void DoMap()
		{
			var a = new MapAgsUriAnalyzer();
			var uris = a.GetRecursivUris(GetMapSources());

			foreach (var uri in uris)
			{
				Console.WriteLine(uri);
			}
		}


		[Test]
		public void Connect()
		{
			////Uri currentUri = new Uri(@"http://server.arcgisonline.com/ArcGIS/rest/services");
			////Uri currentUri = new Uri(@"http://server.arcgisonline.com/ArcGIS/rest/services/World_Imagery/MapServer");
			//Uri currentUri = new Uri(@"http://server.arcgisonline.com/ArcGIS/rest/services/Demographics/");

			//var agsServicesDirectory = new AgsServicesDirectory(currentUri);

			//Console.WriteLine("ImageServices:");
			//foreach (Service service in agsServicesDirectory.GetImageServices(true))
			//{
			//    ImageServiceInfo imageServiceInfo = AgsImageServiceInfo.GetImageServiceInfo(agsServicesDirectory, service.RESTServiceUrl);
			//    Console.WriteLine(imageServiceInfo);
			//}

			//Console.WriteLine("MapServices:");
			//foreach (Service service in agsServicesDirectory.GetMapServices(true))
			//{
			//    Uri serviceUri = service.RESTServiceUrl;
			//    Console.WriteLine(serviceUri);
			//    //ProcessMapService(serviceUri, directory);
			//    //yield return serviceUri;
			//}

			//Console.WriteLine("ServicesDirectories");
			//foreach (ServiceDirectory directory in agsServicesDirectory.AgsServiceDirectory())
			//{
			//    var uri = directory.Url;
			//    //Uri serviceUri = service.RESTServiceUrl;
			//    Console.WriteLine(uri);
			//    //ProcessMapService(serviceUri, directory);
			//    //yield return serviceUri;
			//}
		}
	}

	// ReSharper restore InconsistentNaming
}

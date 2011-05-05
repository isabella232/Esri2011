using System;
using System.Collections.Generic;
using EsriDE.Samples.ContentFinder.DomainModel;

namespace EsriDE.Samples.ContentFinder.ContentAdapter.Ags
{
	public abstract class AgsUriAnalyzer : UriAnalyzer
	{
		protected override IEnumerable<Uri> GetCompositeUris(Uri currentUri)
		{
			var serviceType = GetServiceType(currentUri);

			switch (serviceType)
			{
				case ServiceType.MapService:
				case ServiceType.ImageService:
					yield break;
				case ServiceType.ServiceDirectory:
					var uris = GetComponentUris(currentUri, RecursivityPolicy.Recursiv);
					foreach (var uri in uris)
					{
						yield return uri;
					}
					yield break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		protected override IEnumerable<Uri> GetLeafUris(Uri currentUri)
		{
			var serviceType = GetServiceType(currentUri);

			switch (serviceType)
			{
				case ServiceType.MapService:
				case ServiceType.ImageService:
					var onlyOneLeafUri = currentUri;
					yield return onlyOneLeafUri;
					yield break;
				case ServiceType.ServiceDirectory:
					yield break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}


		protected abstract IEnumerable<Uri> GetCompositeUrisCore(Uri currentUri);

		protected abstract IEnumerable<Uri> GetGetLeafUrisCore(Uri currentUri);


		private ServiceType GetServiceType(Uri currentUri)
		{
			var serviceType = GetServiceTypeString(currentUri);

			if (serviceType == "mapserver")
			{
				return ServiceType.MapService;
			}
			else if (serviceType == "imageserver")
			{
				return ServiceType.ImageService;
			}
			else
			{
				return ServiceType.ServiceDirectory;
			}
		}

		private static string GetServiceTypeString(Uri currentUri)
		{
			var segments = currentUri.Segments;
			var serverType = segments[segments.Length - 1].ToLower();
			var result = serverType.Replace("/", string.Empty);

			return result;
		}

		private void GetImageFromServiceInfo(object o)
		{
			var serviceHandler = ServiceInfoHandler.CreateServiceInfoHandler(o);

			var result = serviceHandler.GetBitmap(serviceHandler);
		}
	}
}

//public static AgsUriAnalyzer CreateAgsUriAnalyzer(Uri currentUri)
//{
//    var serviceType = GetServiceType(currentUri);

//    switch (serviceType)
//    {

//    }
//}


//public abstract class ItemAgsUriAnalyzer : AgsUriAnalyzer
//{

//}

//public class ImageAgsUriAnalyzer : ItemAgsUriAnalyzer
//{

//}

//public class MapAgsUriAnalyzer : ItemAgsUriAnalyzer
//{

//}

//public abstract class DirectoryAgsUriAnalyzer : AgsUriAnalyzer
//{

//}

//public class ImageDirectoryAgsUriAnalyzer : DirectoryAgsUriAnalyzer
//{

//}

//public class MapDirectoryAgsUriAnalyzer : DirectoryAgsUriAnalyzer
//{

//}
using System;
using System.Collections.Generic;
using EsriDE.Samples.AgsServicesJsonDeSerializer;

namespace EsriDE.Samples.ContentFinder.ContentAdapter.Ags
{
	public class MapAgsUriAnalyzer : UriAnalyzer
	{
		protected override IEnumerable<Uri> GetCompositeUris(Uri currentUri)
		{
			var directory = new AgsServicesDirectory(currentUri);
			//private void ProcessMapServiceDirectory(AgsServicesDirectory directory, bool checkSubDirectory)
			//{
			foreach (Service service in directory.GetMapServices(true))
			{
				Uri serviceUri = service.RESTServiceUrl;
				//ProcessMapService(serviceUri, directory);
				yield return serviceUri;
			}
			//}
		}

		//protected override IEnumerable<Uri> GetLeafUris(Uri currentUri)
		//{
		//    yield break;
		//}

		//private void ProcessMapService(Uri restServiceUrl, AgsServicesDirectory directory)
		//{
		//    MapServiceInfo mapServiceInfo = AgsMapServiceInfo.GetMapServiceInfo(directory, restServiceUrl);
		//    if (null != mapServiceInfo)
		//    {
		//        ProcessMapServiceInfo(mapServiceInfo);
		//    }
		//}



		//private void ProcessMapServiceInfo(MapServiceInfo mapServiceInfo)
		//{
		//    if (null == mapServiceInfo.Service)
		//    {
		//        return;
		//    }

		//    Guid guid = Guid.NewGuid();
		//    IMetaData metaData = CreateMetaData(mapServiceInfo, guid);
		//    OnDataReaded(metaData);

		//    try
		//    {
		//        ReadImageAsync(mapServiceInfo, guid);
		//    }
		//    catch (Exception e)
		//    {
		//        Console.WriteLine(e);
		//    }
		//}

		
	}
}
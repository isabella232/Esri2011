using System;
using System.Text;
using EsriDE.Samples.AgsServicesJsonDeSerializer;
using EsriDE.Samples.ContentLoader.Adapter.Contract;

namespace EsriDE.Samples.ContentLoader.Adapter.Basemap
{
	public partial class BasemapReaderAdapter
	{
		private static IMetaData CreateMetaData(ImageServiceInfo imageServiceInfo, Guid guid)
		{
			var topText = GetTopTextFromImageServiceInfo(imageServiceInfo);
			var bottomText = GetBottomTextFromImageServiceInfo(imageServiceInfo);
			var tooltipText = GetTooltipTextFromImageServiceInfo(imageServiceInfo);
			var serviceName = GetServiceNameFromImageServiceInfo(imageServiceInfo);

			return CreateMetaData(guid, imageServiceInfo.Service.SOAPServiceUrl, serviceName, topText, bottomText,
			                  tooltipText);
		}

		private static IMetaData CreateMetaData(MapServiceInfo mapServiceInfo, Guid guid)
		{
			var topText = GetTopTextFromMapServiceInfo(mapServiceInfo);
			var bottomText = GetBottomTextFromMapServiceInfo(mapServiceInfo);
			var tooltipText = GetTooltipTextFromMapServiceInfo(mapServiceInfo);
			var serviveName = GetServiceNameFromMapServiceInfo(mapServiceInfo);

			return CreateMetaData(guid, mapServiceInfo.Service.SOAPServiceUrl, serviveName, topText, bottomText, tooltipText);
		}

		private static IMetaData CreateMetaData(Guid id, Uri uri, string serviceName, string topText, string bottomText,
		                                        string tooltipText)
		{
			IMetaData metaData = new BasemapMetaData(id, uri, serviceName, topText, bottomText, tooltipText);
			return metaData;
		}

        private static string GetTopTextFromMapServiceInfo(MapServiceInfo mapServiceInfo)
        {
            if (null == mapServiceInfo.Service || string.IsNullOrEmpty(mapServiceInfo.Service.Name))
            {
                return "Karten-Service";
            }

            return mapServiceInfo.Service.Name;
        }

		private static string GetTopTextFromImageServiceInfo(ImageServiceInfo imageServiceInfo)
		{
			if (null == imageServiceInfo.Service || string.IsNullOrEmpty(imageServiceInfo.Service.Name))
			{
                return "Rasterdaten-Service";
			}

			return imageServiceInfo.Service.Name;
		}

		private static string GetBottomTextFromImageServiceInfo(ImageServiceInfo imageServiceInfo)
		{
			if (string.IsNullOrEmpty(imageServiceInfo.ServiceDescription))
			{
                return imageServiceInfo.Service.RESTServiceUrl.ToString();
			}

			return imageServiceInfo.ServiceDescription;
		}

		private static string GetTooltipTextFromImageServiceInfo(ImageServiceInfo imageServiceInfo)
		{
			var stringBuilder = new StringBuilder();
			stringBuilder.Append(string.Format("Service Name: {0}{1}", imageServiceInfo.Service.Name, Environment.NewLine));
			stringBuilder.Append(string.Format("Server: {0}{1}", imageServiceInfo.Service.RESTServerUrl.Host, Environment.NewLine));
			stringBuilder.Append(string.Format("Service Type: {0}{1}", imageServiceInfo.Service.Type, Environment.NewLine));
			stringBuilder.Append(string.Format("Author: {0}{1}", imageServiceInfo.BandCount, Environment.NewLine));
			stringBuilder.Append(string.Format("Category: {0}", imageServiceInfo.ServiceDataType));
			return stringBuilder.ToString();
		}

		private static string GetServiceNameFromImageServiceInfo(ImageServiceInfo imageServiceInfo)
		{
			return imageServiceInfo.Service.Name;
		}



		private static string GetBottomTextFromMapServiceInfo(MapServiceInfo mapServiceInfo)
		{
			if (string.IsNullOrEmpty(mapServiceInfo.ServiceDescription))
			{
                return mapServiceInfo.Service.RESTServiceUrl.ToString();
			}

			return mapServiceInfo.ServiceDescription;
		}

		private static string GetTooltipTextFromMapServiceInfo(MapServiceInfo mapServiceInfo)
		{
			try
			{
				var stringBuilder = new StringBuilder();
				stringBuilder.Append(string.Format("Service Name: {0}{1}", mapServiceInfo.Service.Name, Environment.NewLine));
				stringBuilder.Append(string.Format("Server: {0}{1}", mapServiceInfo.Service.RESTServerUrl.Host, Environment.NewLine));
				stringBuilder.Append(string.Format("Service Type: {0}{1}", mapServiceInfo.Service.Type, Environment.NewLine));
				if (null != mapServiceInfo.DocumentInfo)
				{
					stringBuilder.Append(string.Format("Author: {0}{1}", mapServiceInfo.DocumentInfo.Author, Environment.NewLine));
					stringBuilder.Append(string.Format("Category: {0}", mapServiceInfo.DocumentInfo.Category));
				}
				return stringBuilder.ToString();
			}
			catch
			{
				return "Keine gültigen Serviceinformationen gefunden.";
			}
		}

		private static string GetServiceNameFromMapServiceInfo(MapServiceInfo mapServiceInfo)
		{
			return mapServiceInfo.Service.Name;
		}
	}
}
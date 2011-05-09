using System;
using System.Drawing;
using System.Text;
using EsriDE.Commons.Ags;
using EsriDE.Commons.Ags.Contracts;

namespace EsriDE.Samples.ContentFinder.ContentAdapter.Ags
{
	public class MapAgsContentReader : AgsContentReader
	{
		public override AgsContent ReadContent(Uri uri)
		{
			MapServiceInfo mapServiceInfo = MapServiceUtil.GetMapServiceInfo(uri);
			var content = CreateContent(mapServiceInfo);
			return content;
		}

		private static AgsContent CreateContent(MapServiceInfo mapServiceInfo)
		{
			var topText = GetTopTextFromMapServiceInfo(mapServiceInfo);
			var bottomText = GetBottomTextFromMapServiceInfo(mapServiceInfo);
			var tooltipText = GetTooltipTextFromMapServiceInfo(mapServiceInfo);
			var serviveName = GetServiceNameFromMapServiceInfo(mapServiceInfo);

			var bitmap = GetImageFromServiceInfo(mapServiceInfo);
			var result = new AgsContent(topText, bitmap, mapServiceInfo.Uri);

			return result;
		}

		private static Bitmap GetImageFromServiceInfo(MapServiceInfo info)
		{
				var	bitmap = MapServiceUtil.
						GetSysDrawBitmap(info.Uri, info.InitialExtent, @"400,266");
			return bitmap;
		}

		private static string GetTopTextFromMapServiceInfo(MapServiceInfo mapServiceInfo)
		{
			if (string.IsNullOrEmpty(GetServiceNameFromMapServiceInfo(mapServiceInfo)))
			{
				return "Karten-Service";
			}

			return mapServiceInfo.Name;
		}

		private static string GetBottomTextFromMapServiceInfo(MapServiceInfo mapServiceInfo)
		{
			if (string.IsNullOrEmpty(mapServiceInfo.ServiceDescription))
			{
				return mapServiceInfo.Uri.ToString();
			}

			return mapServiceInfo.ServiceDescription;
		}

		private static string GetTooltipTextFromMapServiceInfo(MapServiceInfo mapServiceInfo)
		{
			try
			{
				var stringBuilder = new StringBuilder();
				stringBuilder.Append(string.Format("Service Name: {0}{1}", GetServiceNameFromMapServiceInfo(mapServiceInfo), Environment.NewLine));
				stringBuilder.Append(string.Format("Server: {0}{1}", mapServiceInfo.Uri.Host, Environment.NewLine));
				stringBuilder.Append(string.Format("Service Type: {0}{1}", "mapService", Environment.NewLine));
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
			var serviceName = AgsUtil.GetServiceName(mapServiceInfo.Uri.AbsoluteUri);
			return serviceName;
		}
	}
}
using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using EsriDE.Commons.Ags.Contracts;

namespace EsriDE.Commons.Ags
{
	public static class MapServiceUtil
	{
		public static MapServiceInfo GetMapServiceInfo(Uri uri)
		{
			var json = JsonUtil.GetHttpJsonRequestResult(uri.AbsoluteUri);
			var result = JsonUtil.Deserialize<MapServiceInfo>(json);

			result.Uri = uri;

			return result;
		}

		public static ImageInfo GetImageInfo(Uri url, Extent extent, string size)
		{
			try
			{
				string imageInfoUrl = url.AbsoluteUri + @"/export?bbox=" +
									  extent.XMin.ToString(CultureInfo.InvariantCulture) + "," +
									  extent.YMin.ToString(CultureInfo.InvariantCulture) + "," +
									  extent.XMax.ToString(CultureInfo.InvariantCulture) + "," +
									  extent.YMax.ToString(CultureInfo.InvariantCulture) + "&Size=" + size + "&f=json";

				var info = JsonUtil.Deserialize<ImageInfo>(JsonUtil.GetHttpJsonRequestResult(imageInfoUrl));

				return info;
			}
			catch
			{
				return null;
			}
		}

		public static Stream GetSysDrawImageStream(Uri url, Extent extent, string size)
		{
			try
			{
				string imageInfoUrl = url.AbsoluteUri + @"/export?bbox=" +
									  extent.XMin.ToString(CultureInfo.InvariantCulture) + "," +
									  extent.YMin.ToString(CultureInfo.InvariantCulture) + "," +
									  extent.XMax.ToString(CultureInfo.InvariantCulture) + "," +
									  extent.YMax.ToString(CultureInfo.InvariantCulture) + "&Size=" + size + "&f=image";

				var httpWebRequest = (HttpWebRequest)WebRequest.Create(imageInfoUrl);
				var httpWebReponse = (HttpWebResponse)httpWebRequest.GetResponse();

				return httpWebReponse.GetResponseStream();
			}
			catch
			{
				return null;
			}
		}

		public static Bitmap GetSysDrawBitmap(Uri url, Extent extent, string size)
		{
			try
			{
				Stream stream = GetSysDrawImageStream(url, extent, size);

				var bitmap = new Bitmap(stream);
				return bitmap;
			}
			catch
			{
				return null;
			}
		}
	}
}
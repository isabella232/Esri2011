using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using EsriDE.Commons.Ags.Contracts;

namespace EsriDE.Commons.Ags
{
	public static class ImageServiceUtil
	{
		private static string _JASONSegment = @"f=json";

		#region methods

		public static ImageServiceInfo GetImageServiceInfo(AgsServicesDirectory directory, Uri url)
		{
			try
			{
				Service service = null;

				foreach (Service serv in directory.GetImageServices(true))
				{
					if (serv.RESTServiceUrl.AbsoluteUri.ToLower().Contains(url.AbsoluteUri.ToLower().ToLower()))
					{
						service = serv;
						break;
					}
				}


				var info =
					JsonUtil.Deserialize<ImageServiceInfo>(JsonUtil.GetHttpJsonRequestResult(url.AbsoluteUri + "?" + _JASONSegment));
				info.Service = service;
				string imageInfoUrl = url.AbsoluteUri + @"exportImage?bbox=" +
									  info.Extent.XMin.ToString(CultureInfo.InvariantCulture) + "," +
									  info.Extent.YMin.ToString(CultureInfo.InvariantCulture) + "," +
									  info.Extent.XMax.ToString(CultureInfo.InvariantCulture) + "," +
									  info.Extent.YMax.ToString(CultureInfo.InvariantCulture) + "&Size=400,266&f=json";

				return info;
			}
			catch
			{
				return null;
			}
		}

		public static ImageInfo GetImageInfo(Uri url, Extent extent, string size)
		{
			try
			{
				string imageInfoUrl = url.AbsoluteUri + @"exportImage?bbox=" +
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
				string imageInfoUrl = url.AbsoluteUri + @"exportImage?bbox=" +
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

		#endregion
	}
}
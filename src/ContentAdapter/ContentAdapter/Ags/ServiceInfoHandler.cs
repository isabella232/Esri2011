using System;
using System.Drawing;
using EsriDE.Samples.AgsServicesJsonDeSerializer;

namespace EsriDE.Samples.ContentFinder.ContentAdapter.Ags
{
	public abstract class ServiceInfoHandler
	{
		protected abstract Type ServiceInfoType { get; }

		public Bitmap GetBitmap(object serviceInfo)
		{
			VerifyCorrectServiceInfoType(serviceInfo);

			var result = GetBitmapCore(serviceInfo);
			return result;
		}

		protected abstract Bitmap GetBitmapCore(object serviceInfo);

		private void VerifyCorrectServiceInfoType(object serviceInfo)
		{
			if (serviceInfo.GetType() != ServiceInfoType)
			{
				throw new ArgumentException("serviceInfo hat nicht den erwarteten Typ: " + ServiceInfoType, "serviceInfo");
			}
		}

		public static ServiceInfoHandler CreateServiceInfoHandler(object serviceInfo)
		{
			if (typeof(MapServiceInfo) == serviceInfo.GetType())
			{
				return new MapServiceInfoHandler();
			}
			else if (typeof(ImageServiceInfo) == serviceInfo.GetType())
			{
				return new ImageServiceInfoHandler();
			}
			else
			{
				throw new ArgumentException("Unbekannter serviceInfoTyp: " + serviceInfo.GetType(), "serviceInfo");
			}
		}
	}

	public class MapServiceInfoHandler : ServiceInfoHandler
	{
		protected override Type ServiceInfoType
		{
			get { return typeof (MapServiceInfo); }
		}

		protected override Bitmap GetBitmapCore(object serviceInfo)
		{
			var mapServiceInfo = (MapServiceInfo) serviceInfo;
			Bitmap result = AgsMapServiceInfo.
				GetSysDrawBitmap(mapServiceInfo.Service.RESTServiceUrl, mapServiceInfo.InitialExtent, @"400,266");
			return result;
		}
	}

	public class ImageServiceInfoHandler : ServiceInfoHandler
	{
		protected override Type ServiceInfoType
		{
			get { return typeof (ImageServiceInfo); }
		}

		protected override Bitmap GetBitmapCore(object serviceInfo)
		{
			var imageServiceInfo = (ImageServiceInfo) serviceInfo;
			Bitmap result = AgsImageServiceInfo.
				GetSysDrawBitmap(imageServiceInfo.Service.RESTServiceUrl, imageServiceInfo.Extent, @"400,266");

			return result;
		}
	}
}
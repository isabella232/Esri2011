using System;
using System.Diagnostics;
using System.Threading;

namespace EsriDE.Samples.ContentFinder.ContentAdapter.Ags
{
	public abstract class AgsContentReader
	{
		public static AgsContentReader CreateAgsContentReader(ServiceType serviceType)
		{
			switch (serviceType)
			{
				case ServiceType.MapService:
					return new MapAgsContentReader();
				case ServiceType.ImageService:
					return new ImageAgsContentReader();
				case ServiceType.Unknown:
					return new NullAgsContentReader();
				default:
					throw new ArgumentOutOfRangeException("serviceType");
			}
		}
		public abstract AgsContent ReadContent(Uri uri);
	}
}
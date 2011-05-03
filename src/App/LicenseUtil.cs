using ESRI.ArcGIS;
using ESRI.ArcGIS.esriSystem;

namespace EsriDE.Samples.ContentFinder.App
{
	public static class LicenseUtil
	{
		internal static void InitializeEngineLicense()
		{
			RuntimeManager.Bind(ProductCode.EngineOrDesktop);
			AoInitialize aoi = new AoInitializeClass();

			//more license choices could be included here
			const esriLicenseProductCode productCode = esriLicenseProductCode.esriLicenseProductCodeEngine;
			if (aoi.IsProductCodeAvailable(productCode) == esriLicenseStatus.esriLicenseAvailable)
			{
				aoi.Initialize(productCode);
			}
		}
	}
}
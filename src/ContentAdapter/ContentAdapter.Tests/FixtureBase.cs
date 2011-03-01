using System;
using ESRI.ArcGIS;
using ESRI.ArcGIS.esriSystem;
using NUnit.Framework;

namespace EsriDE.Samples.ContentFinder.ContentAdapter.Tests
{
	public abstract class FixtureBase
	{
		[TestFixtureSetUp]
		public virtual void FixtureSetup()
		{
			RuntimeManager.Bind(ProductCode.EngineOrDesktop);
			InitializeEngineLicense();
		}

		[SetUp]
		public virtual void Setup()
		{
			try
			{
				//ContainerFacade.CreateContainer();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}


		private static void InitializeEngineLicense()
		{
			AoInitialize aoi = new AoInitializeClass();

			//more license choices could be included here
			const esriLicenseProductCode productCode = esriLicenseProductCode.esriLicenseProductCodeArcInfo;
			if (aoi.IsProductCodeAvailable(productCode) == esriLicenseStatus.esriLicenseAvailable)
			{
				aoi.Initialize(productCode);
			}
		}
	}
}
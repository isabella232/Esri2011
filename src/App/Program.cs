using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using ESRI.ArcGIS;
using ESRI.ArcGIS.esriSystem;
using EsriDE.Commons.CastleWindsor.Extension;
using EsriDE.Samples.ContentFinder.BL;
using EsriDE.Samples.ContentFinder.BL.Contract;
using EsriDE.Samples.ContentFinder.ConfigurationAdapter.Contract;
using EsriDE.Samples.ContentFinder.ContentAdapter;
using EsriDE.Samples.ContentFinder.ContentAdapter.Contract;
using EsriDE.Samples.ContentFinder.DomainModel;
using EsriDE.Samples.ContentFinder.UI.Contract;
using EsriDE.Samples.ContentFinder.WpfUI;
using EsriDE.Samples.ContentFinder.XmlConfigurationAdapter;
using Application = System.Windows.Forms.Application;

namespace App
{
	internal static class Program
	{
		private static IWindsorContainer _container;

		/// <summary>
		///   The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			InitializeEngineLicense();

			if (0 == args.Length)
			{
				StartGui();
			}
			else
			{
				StartConsole();
			}
		}

		private static void StartConsole()
		{
			Console.WriteLine("Console startet.");
		}

		private static void StartGui()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			var uc = ConfigureIoc();
			var form = new WinFormsForm(uc);
			Application.Run(form);
		}

		private static ElementHost ConfigureIoc()
		{
			_container = new WindsorContainer();

			_container.Kernel.ReleasePolicy = new TrulyTransientReleasePolicy();

			//_container.Register(
			//    Component.For<IPortal>().ImplementedBy<ContentUserControl>().LifeStyle.Custom(
			//        typeof (TrulyTransientLifestyleManager)));

			_container.Register(
				Component
				.For<IPortal>()
				.ImplementedBy<ContentUserControl>()
				.DynamicParameters((k, d) =>
				{
					d["Controller"] =
						k.Resolve<IController>();
				}));

			_container.Register(
				Component.For<IController>().ImplementedBy<Controller>().LifeStyle.Custom(typeof (TrulyTransientLifestyleManager)));
			_container.Register(
				Component.For<IConfigurationReader>().ImplementedBy<XmlConfigurationReader>().LifeStyle.Custom(
					typeof (TrulyTransientLifestyleManager)));
			
			_container.Register(
				Component.For<IToggleableView, IToggleableForm>().ImplementedBy<ContentForm>().LifeStyle.Custom(
					typeof (TrulyTransientLifestyleManager)));

			var assemlblyLocation = Assembly.GetExecutingAssembly().Location;
			string assemblyPath = (Directory.GetParent(assemlblyLocation)).FullName;

			_container.Register(AllTypes.FromAssemblyInDirectory(new AssemblyFilter(assemblyPath)).BasedOn
			                    	<IContentLocatorCreatorFilter>()
			                    	.WithService.FromInterface()
			                    	.Configure(c => c.LifeStyle.Custom(typeof (TrulyTransientLifestyleManager))));

			_container.Register(Component
									.For<IContentLocatorResolver>()
									.ImplementedBy<ContentLocatorResolver>()
									.DynamicParameters((k, d) =>
									{
										d["contentLocatorCreatorFilters"] =
											k.ResolveAll<IContentLocatorCreatorFilter>();
									}));
			
			//var v1 = _container.Resolve<IContentLocatorResolver>();
			//var controller = _container.Resolve<IController>();

			//if (null == _container)
			//{
			//    throw new ApplicationException("Ioc Configuration failed.");
			//}

			//controller.ContentFound += ImportContent;
			//controller.Start();

			var uc = (UIElement) _container.Resolve<IPortal>();
			var host = new ElementHost { Child = uc};

			return host;
		}

		private static void ImportContent(Content content)
		{
			Console.WriteLine(content.Title);
			Debug.WriteLine(content.Title);
		}

		private static void InitializeEngineLicense()
		{
			RuntimeManager.Bind(ProductCode.EngineOrDesktop);
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

/*
//_container.Register(
			//    Component.For<IContentLocatorResolver>().ImplementedBy<ContentLocatorResolver>().LifeStyle.Custom(
			//        typeof (TrulyTransientLifestyleManager)));

			//_container.Register(
			//    Component.For<IEnumerable<IContentLocatorCreatorFilter>>().ImplementedBy<List<IContentLocatorCreatorFilter>>().
			//        LifeStyle.Custom(typeof (TrulyTransientLifestyleManager)));
			//Scanning (MxdContentLocatorCreatorFilter(mxd, ..), AgsContentLocatorCreatorFilter(ags, ..))
			//Scanning (MxdContentLocatorCreator, AgsContentLocatorCreator)

//var filters = _container.ResolveAll<IContentLocatorCreatorFilter>();
			//_container.Register(
			//    Component.For<IEnumerable<IContentLocatorCreatorFilter>>().Instance(filters));

			//_container.Register(
			//    Component.For<ContentLocatorResolver>().Parameters(Parameter.ForKey("contentLocatorCreatorFilters").Eq(filters));

			//_container.Register(Component
			//                        .For<ContentLocatorResolver>()
			//                        .DynamicParameters((k, d) =>
			//                                            {
			//                                                d["contentLocatorCreatorFilters"] =
			//                                                    k.ResolveAll<IContentLocatorCreatorFilter>();
			//                                            }));
			//_container.Register(
			//    Component.For<IContentLocatorResolver>().ImplementedBy<ContentLocatorResolver>().LifeStyle.Custom(
			//        typeof(TrulyTransientLifestyleManager)));

//     .DynamicParameters((k, d) => // dynamic parameters
		//{
		//    var randomNumber = 2;
		//    if (randomNumber == 2)
		//    {
		//        d["customer"] = k.Resolve<ICustomer>("otherCustomer");
		//    }
		//}));

  //          Component.For<MyComponentObject>()
  //.Parameters(Parameter.ForKey("Dao").Eq("myDao")));


*/
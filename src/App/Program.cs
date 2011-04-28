using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using EsriDE.Samples.ContentFinder.BL;
using EsriDE.Samples.ContentFinder.BL.Contract;
using EsriDE.Samples.ContentFinder.ConfigurationAdapter.Contract;
using EsriDE.Samples.ContentFinder.ContentAdapter;
using EsriDE.Samples.ContentFinder.ContentAdapter.Contract;
using EsriDE.Samples.ContentFinder.SystemBuild;
using EsriDE.Samples.ContentFinder.UI.Contract;
using EsriDE.Samples.ContentFinder.WpfUI;
using EsriDE.Samples.ContentFinder.XmlConfigurationAdapter;

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
			ConfigureIoc();

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}

		private static void ConfigureIoc()
		{
			_container = new WindsorContainer();

			_container.Kernel.ReleasePolicy = new TrulyTransientReleasePolicy();

			_container.Register(
				Component.For<IPortal>().ImplementedBy<ContentUserControl>().LifeStyle.Custom(
					typeof (TrulyTransientLifestyleManager)));

			_container.Register(
				Component.For<IController>().ImplementedBy<Controller>().LifeStyle.Custom(typeof (TrulyTransientLifestyleManager)));
			_container.Register(
				Component.For<IConfigurationReader>().ImplementedBy<XmlConfigurationReader>().LifeStyle.Custom(
					typeof (TrulyTransientLifestyleManager)));
			_container.Register(
				Component.For<IContentLocatorResolver>().ImplementedBy<ContentLocatorResolver>().LifeStyle.Custom(
					typeof (TrulyTransientLifestyleManager)));

			_container.Register(
				Component.For<IEnumerable<IContentLocatorCreatorFilter>>().ImplementedBy<List<IContentLocatorCreatorFilter>>().
					LifeStyle.Custom(typeof (TrulyTransientLifestyleManager)));
			//Scanning (MxdContentLocatorCreatorFilter(mxd, ..), AgsContentLocatorCreatorFilter(ags, ..))
			//Scanning (MxdContentLocatorCreator, AgsContentLocatorCreator)

			_container.Register(
				Component.For<IToggleableView, IToggleableForm>().ImplementedBy<ContentForm>().LifeStyle.Custom(
					typeof (TrulyTransientLifestyleManager)));

			var assemlblyLocation = Assembly.GetExecutingAssembly().Location;
			string assemblyPath = (Directory.GetParent(assemlblyLocation)).FullName;

			_container.Register(AllTypes.FromAssemblyInDirectory(new AssemblyFilter(assemblyPath)).BasedOn
			                    	<IContentLocatorCreatorFilter>()
			                    	.WithService.FromInterface()
			                    	.Configure(c => c.LifeStyle.Custom(typeof (TrulyTransientLifestyleManager))));

			if (null == _container)
			{
				throw new ApplicationException("Ioc Configuration failed.");
			}
		}
	}
}
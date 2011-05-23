using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using EsriDE.Commons.CastleWindsor.Extension;
using EsriDE.Samples.ContentFinder.BL;
using EsriDE.Samples.ContentFinder.BL.Contract;
using EsriDE.Samples.ContentFinder.ConfigurationAdapter.Contract;
using EsriDE.Samples.ContentFinder.ContentAdapter;
using EsriDE.Samples.ContentFinder.ContentAdapter.Contract;
using EsriDE.Samples.ContentFinder.UI.Contract;
using EsriDE.Samples.ContentFinder.WpfUI;
using EsriDE.Samples.ContentFinder.XmlConfigurationAdapter;

namespace EsriDE.Samples.ContentFinder.App
{
	public class Builder
	{
		private readonly WindsorContainer _container;

		public Builder()
		{
			_container = GetConfiguredIocContainer();
		}

		public IPortal GetPortal()
		{
			var portal = _container.Resolve<IPortal>();
			return portal;
		}

		public Control GetPortalControl()
		{
			var uc = (UIElement)_container.Resolve<IPortal>();
			var host = new ElementHost { Child = uc };

			return host;
		}

		private static WindsorContainer GetConfiguredIocContainer()
		{
			var container = new WindsorContainer();

			container.Kernel.ReleasePolicy = new TrulyTransientReleasePolicy();

			container.Register(
				Component
				.For<IPortal>()
				.ImplementedBy<ContentUserControl>()
				.DynamicParameters((k, d) =>
				{
					d["Controller"] =
						k.Resolve<IController>();
					d["ContentProcessorAdapter"] =
						k.Resolve<IContentProcessorAdapter>();
				})
				.LifeStyle.Custom(typeof(TrulyTransientLifestyleManager)));

			container.Register(
				Component.For<IController>().ImplementedBy<Controller>()
				.LifeStyle.Custom(typeof(TrulyTransientLifestyleManager)));
			container.Register(
				Component.For<IConfigurationReader>().ImplementedBy<XmlConfigurationReader>()
				.LifeStyle.Custom(typeof(TrulyTransientLifestyleManager)));

			container.Register(
				Component.For<IToggleableView, IToggleableForm>().ImplementedBy<ContentForm>()
				.LifeStyle.Custom(typeof(TrulyTransientLifestyleManager)));

			var assemlblyLocation = Assembly.GetExecutingAssembly().Location;
			string assemblyPath = (Directory.GetParent(assemlblyLocation)).FullName;

			container.Register(AllTypes.FromAssemblyInDirectory(new AssemblyFilter(assemblyPath)).BasedOn
									<IContentLocatorCreatorFilter>()
									.WithService.FromInterface()
									.Configure(c => c.LifeStyle.Custom(typeof(TrulyTransientLifestyleManager))));

			container.Register(Component
									.For<IContentLocatorResolver>()
									.ImplementedBy<ContentLocatorResolver>()
									.DynamicParameters((k, d) =>
									{
										d["contentLocatorCreatorFilters"] =
											k.ResolveAll<IContentLocatorCreatorFilter>();
									})
									.LifeStyle.Custom(typeof(TrulyTransientLifestyleManager)));

			container.Register(Component.For<IContentProcessorAdapter>().ImplementedBy<SimpleWriterContentProcessorAdapter>());

			return container;
		}
	}
}
#region Oldcode
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
#endregion
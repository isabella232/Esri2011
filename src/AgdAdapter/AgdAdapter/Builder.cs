using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using EsriDE.Commons.CastleWindsor.Extension;
using EsriDE.Commons.Patterns;
using EsriDE.Samples.ContentFinder.AgdAdapterCommons;
using EsriDE.Samples.ContentFinder.AgdBLAdapter;
using EsriDE.Samples.ContentFinder.ApplicationAdapter.Contract;
using EsriDE.Samples.ContentFinder.BL;
using EsriDE.Samples.ContentFinder.BL.Contract;
using EsriDE.Samples.ContentFinder.ConfigurationAdapter.Contract;
using EsriDE.Samples.ContentFinder.ContentAdapter;
using EsriDE.Samples.ContentFinder.ContentAdapter.Contract;
using EsriDE.Samples.ContentFinder.DomainModel;
using EsriDE.Samples.ContentFinder.UI.Contract;
using EsriDE.Samples.ContentFinder.WpfUI;
using EsriDE.Samples.ContentFinder.XmlConfigurationAdapter;

namespace EsriDE.Samples.ContentFinder.AgdAdapter
{
	public class Builder
	{
		private IWindsorContainer _container;

		private IContentProcessorAdapter _contentProcessorAdapter;
		private IPortal _portal;
		private IToggleablePresenter _toggleablePresenter;

		public Builder(IShifterView view)
		{
			ConfigureIocContainer();

			ConnectShifterPresenterTo(view);
			ConnectMeAsModelObserver();
		}

		~Builder()
		{
			Trace.WriteLine("Builder.~");
		}

		private void ConnectMeAsModelObserver()
		{
			var model = _container.Resolve<IShifterModel>();
			model.ShifterStateChanged += ManageFormSubsystem;
		}

		private void ManageFormSubsystem(ShifterState state)
		{
			switch (state)
			{
				case ShifterState.On:
					ConstructSystem();
					break;
				case ShifterState.Off:
					DestroySystem();
					break;
				default:
					throw new ArgumentOutOfRangeException("state");
			}
		}

		private void ConstructSystem()
		{
			Trace.WriteLine("Construct system");
			try
			{
				_toggleablePresenter = _container.Resolve<IToggleablePresenter>();

				_contentProcessorAdapter = _container.Resolve<IContentProcessorAdapter>();
				_portal = _container.Resolve<IPortal>();
				_portal.ContentSelected += _contentProcessorAdapter.Process;
			}
			catch (Exception e)
			{
				Trace.WriteLine(e);
			}
			//var formVisibilityModel = _container.Resolve<IToggleModel>();
			//_toggleablePresenter.SetModel(formVisibilityModel);
		}

		private void DestroySystem()
		{
			Trace.WriteLine("Destroy system");
			////_toggleablePresenter.UnsetModel();
			////_container.Release(_toggleablePresenter);

			_portal.ContentSelected -= _contentProcessorAdapter.Process;
			_portal = null;
			_contentProcessorAdapter = null;

			_toggleablePresenter = null;
		}

		private void ConfigureIocContainer()
		{
			_container = new WindsorContainer();
			_container.Register(Component.For<IShifterPresenter>().ImplementedBy<ShifterPresenter>());
			_container.Register(Component.For<IShifterModel>().ImplementedBy<ToggleViewShifterModel>());
			_container.Register(Component.For<IWindowInformation>().ImplementedBy<HostWindowInformation>());

			_container.Kernel.ReleasePolicy = new TrulyTransientReleasePolicy();
			_container.Register(
				Component.For<IToggleablePresenter>().ImplementedBy<ContentFormPresenter>().LifeStyle.Custom(
					typeof (TrulyTransientLifestyleManager)));

			_container.Register(
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
			//Todo: .LifeStyle.Custom(typeof(TrulyTransientLifestyleManager)));
			//IPortal wird 2mal angefordert -> 2mal instantiert;

			_container.Register(
				Component.For<IController>().ImplementedBy<Controller>().LifeStyle.Custom(typeof (TrulyTransientLifestyleManager)));
			_container.Register(
				Component.For<IConfigurationReader>().ImplementedBy<XmlConfigurationReader>()
					.LifeStyle.Custom(typeof (TrulyTransientLifestyleManager)));

			_container.Register(
				Component.For<IToggleableView, IToggleableForm>().ImplementedBy<ContentForm>()
					.LifeStyle.Custom(typeof (TrulyTransientLifestyleManager)));

			var assemlblyLocation = Assembly.GetExecutingAssembly().Location;
			string assemblyPath = Directory.GetParent(assemlblyLocation).FullName;

			_container.Register(AllTypes.FromAssemblyInDirectory(new AssemblyFilter(assemblyPath))
			                    	.BasedOn<IContentLocatorCreatorFilter>()
			                    	.WithService.FromInterface()
			                    	.Configure(c => c.LifeStyle.Custom(typeof (TrulyTransientLifestyleManager))));

			_container.Register(Component
			                    	.For<IContentLocatorResolver>()
			                    	.ImplementedBy<ContentLocatorResolver>()
			                    	.DynamicParameters((k, d) =>
			                    	                   	{
			                    	                   		d["contentLocatorCreatorFilters"] =
			                    	                   			k.ResolveAll<IContentLocatorCreatorFilter>();
			                    	                   	})
			                    	.LifeStyle.Custom(typeof (TrulyTransientLifestyleManager)));

			_container.Register(Component.For<IApplicationAdapter>().ImplementedBy<ArcMapAddinAdapter>()
			                    	.LifeStyle.Custom(typeof (TrulyTransientLifestyleManager)));

			_container.Register(Component.For<IChainOfResponsibilityHandler<Content>>().ImplementedBy<ApplicationMxdCorHandler>()
			                    	.Named("mxd").Parameters(Parameter.ForKey("successor").Eq("${ags}"))
			                    	.LifeStyle.Custom(typeof (TrulyTransientLifestyleManager)));
			_container.Register(Component.For<IChainOfResponsibilityHandler<Content>>().ImplementedBy<ApplicationAgsCorHandler>()
			                    	.Named("ags").Parameters(Parameter.ForKey("successor").Eq("${Ende}"))
			                    	.LifeStyle.Custom(typeof (TrulyTransientLifestyleManager)));
			_container.Register(Component.For<IChainOfResponsibilityHandler<Content>>().ImplementedBy<EndApplicationCorHandler>()
			                    	.LifeStyle.Custom(typeof (TrulyTransientLifestyleManager))
			                    	.Named("Ende"));

			_container.Register(Component.For<IContentProcessorAdapter>().ImplementedBy<ContentProcessorAdapter>());
		}

		private void ConnectShifterPresenterTo(IShifterView view)
		{
			var buttonPresenter = _container.Resolve<IShifterPresenter>();
			buttonPresenter.ConnectView(view);
		}
	}
}

#region Oldcode
/*
			_container.Register(Component.For<IToggleableView, IToggleableForm>().ImplementedBy<ContentForm>().LifeStyle.Custom(typeof(TrulyTransientLifestyleManager)));

			////_container.Register(
			////    Component.For<IContentModel>().ImplementedBy<ContentModel>().LifeStyle.Custom(
			////        typeof (TrulyTransientLifestyleManager)));
			////_container.Register(
			////    Component.For<IAgdAdapter>().ImplementedBy<AgdAdapter>().LifeStyle.Custom(typeof (TrulyTransientLifestyleManager)));
			*/
#endregion
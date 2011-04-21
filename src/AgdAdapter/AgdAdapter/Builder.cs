using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using EsriDE.Samples.ContentFinder.SystemBuild;
using EsriDE.Samples.ContentFinder.UI.Contract;
using EsriDE.Samples.ContentFinder.WpfUI;

namespace EsriDE.Samples.ContentFinder.AgdAdapter
{
	public class Builder
	{
		private IWindsorContainer _container;
		private IToggleablePresenter _toggleablePresenter;

		public Builder(IShifterView view)
		{
			ConfigureIocContainer();

			ConnectShifterPresenterTo(view);
			ConnectMeAsModelObserver();
		}

		~Builder()
		{
			Console.WriteLine("Builder.~");
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
			Console.WriteLine("Construct system");
			_toggleablePresenter = _container.Resolve<IToggleablePresenter>();
			//var formVisibilityModel = _container.Resolve<IToggleModel>();
			//_toggleablePresenter.SetModel(formVisibilityModel);
		}

		private void DestroySystem()
		{
			Console.WriteLine("Destroy system");
			//_toggleablePresenter.UnsetModel();
			//_container.Release(_toggleablePresenter);
			_toggleablePresenter = null;
		}

		private void ConfigureIocContainer()
		{
			_container = new WindsorContainer();
			_container.Register(Component.For<IShifterPresenter>().ImplementedBy<ShifterPresenter>());
			_container.Register(Component.For<IShifterModel>().ImplementedBy<ToggleViewShifterModel>());

			_container.Kernel.ReleasePolicy = new TrulyTransientReleasePolicy();
			_container.Register(
				Component.For<IToggleablePresenter>().ImplementedBy<ContentFormPresenter>().LifeStyle.Custom(
					typeof (TrulyTransientLifestyleManager)));
			_container.Register(
				Component.For<IToggleableView, IToggleableForm>().ImplementedBy<ContentForm>().LifeStyle.Custom(typeof(TrulyTransientLifestyleManager)));
			//_container.Register(
			//    Component.For<IContentModel>().ImplementedBy<ContentModel>().LifeStyle.Custom(
			//        typeof (TrulyTransientLifestyleManager)));
			//_container.Register(
			//    Component.For<IAgdAdapter>().ImplementedBy<AgdAdapter>().LifeStyle.Custom(typeof (TrulyTransientLifestyleManager)));
		}

		private void ConnectShifterPresenterTo(IShifterView view)
		{
			var buttonPresenter = _container.Resolve<IShifterPresenter>();
			buttonPresenter.ConnectView(view);
		}
	}

	//public class Activator : IComponentActivator
	//{
	//    public object Create(CreationContext context)
	//    {
	//        context.
	//        throw new NotImplementedException();
	//    }

	//    public void Destroy(object instance)
	//    {
	//        throw new NotImplementedException();
	//    }
	//}
}
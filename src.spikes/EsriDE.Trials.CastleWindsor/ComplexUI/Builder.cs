using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using EsriDE.Trials.CastleWindsor.ComplexUI.AA;
using EsriDE.Trials.CastleWindsor.ComplexUI.Buttons;
using EsriDE.Trials.CastleWindsor.ComplexUI.Fixtures.WidgetFakes;
using EsriDE.Trials.CastleWindsor.ComplexUI.Forms;
using EsriDE.Trials.CastleWindsor.LifestyleBehaviour;

namespace EsriDE.Trials.CastleWindsor.ComplexUI
{
	public class Builder : IBuilder
	{
		private IWindsorContainer _container;
		private IFormPresenter _formPresenter;

		public Builder(IButtonView view)
		{
			ConfigureIocContainer();

			ConnectButtonPresenterTo(view);
			ConnectMeAsModelObserver();
		}

		~Builder()
		{
			Console.WriteLine("Builder.~");
		}

		private void ConnectMeAsModelObserver()
		{
			var model = _container.Resolve<IToggleFormVisibilityModel>();
			model.VisibilityChanged += ModelToggeled;
		}

		private void ModelToggeled(VisibilityState visibilityState)
		{
			switch (visibilityState)
			{
				case VisibilityState.Visible:
					ConstructSystem();
					break;
				case VisibilityState.Invisible:
					DestroySystem();
					break;
				default:
					throw new ArgumentOutOfRangeException("visibilityState");
			}
		}

		private void ConstructSystem()
		{
			Console.WriteLine("Construct system");
			_formPresenter = _container.Resolve<IFormPresenter>();
			var formVisibilityModel = _container.Resolve<IToggleFormVisibilityModel>();
			_formPresenter.SetModel(formVisibilityModel);
		}

		private void DestroySystem()
		{
			Console.WriteLine("Destroy system");
			_formPresenter.UnsetModel();
			//_container.Release(_formPresenter);
			_formPresenter = null;
		}

		private void ConfigureIocContainer()
		{
			_container = new WindsorContainer();
			_container.Register(Component.For<IButtonPresenter>().ImplementedBy<ButtonPresenter>());
			_container.Register(Component.For<IToggleFormVisibilityModel>().ImplementedBy<ToggleFormVisibilityModel>());

			//_container.Register(Component.For<IFormPresenter>().ImplementedBy<SampleFormPresenter>().LifeStyle.Transient);
			//_container.Register(Component.For<IFormView>().ImplementedBy<FormView>().LifeStyle.Transient);
			//_container.Register(Component.For<IContentModel>().ImplementedBy<ContentModel>().LifeStyle.Transient);

			_container.Kernel.ReleasePolicy = new TrulyTransientReleasePolicy();
			_container.Register(
				Component.For<IFormPresenter>().ImplementedBy<SampleFormPresenter>().LifeStyle.Custom(
					typeof (TrulyTransientLifestyleManager)));
			_container.Register(
				Component.For<IFormView>().ImplementedBy<WinFormsView>().LifeStyle.Custom(typeof (TrulyTransientLifestyleManager)));
			_container.Register(
				Component.For<IContentModel>().ImplementedBy<ContentModel>().LifeStyle.Custom(
					typeof (TrulyTransientLifestyleManager)));
			_container.Register(
				Component.For<IAgdAdapter>().ImplementedBy<AgdAdapter>().LifeStyle.Custom(typeof (TrulyTransientLifestyleManager)));
		}

		private void ConnectButtonPresenterTo(IButtonView view)
		{
			var buttonPresenter = _container.Resolve<IButtonPresenter>();
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
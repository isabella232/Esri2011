using System;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace EsriDE.Trials.CastleWindsor
{
	public interface IBuilder
	{}

	public class Builder : IBuilder
	{
		private IWindsorContainer _container;
		private IFormPresenter _formPresenter;

		~Builder()
		{
			Console.WriteLine("Builder.~");
		}

		public Builder(IButtonView view)
		{
			ConfigureIocContainer();

			ConnectButtonPresenterTo(view);
			ConnectMeAsModelObserver();
		}

		private void ConnectMeAsModelObserver()
		{
			var model = _container.Resolve<IToggleFormVisibilityModel>();
			model.VisibilityChanged += ModelToggeled;
		}

		private void ModelToggeled(Visibility visibility)
		{
			switch (visibility)
			{
				case Visibility.Visible:
					ConstructSystem();
					break;
				case Visibility.Invisible:
					DestroySystem();
					break;
				default:
					throw new ArgumentOutOfRangeException("visibility");
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
			_container.Release(_formPresenter);
			_formPresenter = null;
		}

		private void ConfigureIocContainer()
		{
			_container = new WindsorContainer();
			_container.Register(Component.For<IButtonPresenter>().ImplementedBy<ButtonPresenter>());
			_container.Register(Component.For<IToggleFormVisibilityModel>().ImplementedBy<ToggleFormVisibilityModel>());
			_container.Register(Component.For<IAgdAdapter>().ImplementedBy<AgdAdapter>().LifeStyle.Transient);
			_container.Register(Component.For<IFormPresenter>().ImplementedBy<FormPresenter>().LifeStyle.Transient);
			_container.Register(Component.For<IFormView>().ImplementedBy<FormView>().LifeStyle.Transient);
			_container.Register(Component.For<IContentModel>().ImplementedBy<ContentModel>().LifeStyle.Transient);
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
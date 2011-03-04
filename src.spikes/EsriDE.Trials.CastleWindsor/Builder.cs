using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace EsriDE.Trials.CastleWindsor
{
	public class Builder
	{
		private readonly IButtonView _view;
		private IWindsorContainer _container;
		private IButtonPresenter _buttonPresenter;

		public Builder(IButtonView view)
		{
			_container = new WindsorContainer();
			_view = view;
			_view.Clicked += ConstructSystem;
			Configure();
		}

		private void ConstructSystem()
		{
			var v = _container.Resolve<IFormPresenter>();
			var m = _container.Resolve<IToggleFormVisibilityModel>();
			v.SetModel(m);
			_view.Clicked -= ConstructSystem;
		}

		public void Configure()
		{
			_container.Register(Component.For<IButtonPresenter>().ImplementedBy<ButtonPresenter>());
			_container.Register(Component.For<IToggleFormVisibilityModel>().ImplementedBy<ToggleFormVisibilityModel>());
			_container.Register(Component.For<IAgdAdapter>().ImplementedBy<AgdAdapter>());
			_container.Register(Component.For<IFormPresenter>().ImplementedBy<FormPresenter>());
			_container.Register(Component.For<IFormView>().ImplementedBy<FormView>());
			_container.Register(Component.For<IContentModel>().ImplementedBy<ContentModel>());
			//_container.Register(Component.For<IXyz>().ImplementedBy<Xyz>());
			//_container.Register(Component.For<IA>().ImplementedBy<A>());
			//_container.Register(Component.For<IB>().ImplementedBy<B>());
			//_container.Register(Component.For<IC>().ImplementedBy<C>());
		}

		public IButtonPresenter GetButtonPresenter()
		{
			if (null == _buttonPresenter)
			{
				_buttonPresenter = _container.Resolve<IButtonPresenter>();
			}

			return _buttonPresenter;
		}
	}
}
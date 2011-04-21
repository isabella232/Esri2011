using System;

namespace EsriDE.Samples.ContentFinder.AgdAdapter
{
	public abstract class ButtonPresenter : IButtonPresenter
	{
		private readonly IButtonView _view;
		private readonly IButtonModel _model;

		protected ButtonPresenter(IButtonView view, IButtonModel model)
		{
			_view = view;
			_model = model;

			_view.Clicked += ClickedHandler;
		}

		public virtual void ClickedHandler()
		{
			_model.Do();
		}

	}

	class ContentFinderButtonPresenter : ButtonPresenter
	{
		public ContentFinderButtonPresenter(IButtonView view)
			: base(view, new ContentFinderButtonModel())
		{
		}
	}
}
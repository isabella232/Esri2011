using System;

namespace EsriDE.Trials.CastleWindsor
{
	public interface IFormPresenter
	{
		void SetModel(IToggleFormVisibilityModel model);
	}

	class FormPresenter : IFormPresenter
	{
		private readonly IFormView _view;
		private readonly IAgdAdapter _agdAdapter;
		private IToggleFormVisibilityModel _model;

		public FormPresenter(IFormView view, IAgdAdapter agdAdapter)
		{
			_view = view;
			_agdAdapter = agdAdapter;
		}

		public void SetModel(IToggleFormVisibilityModel model)
		{
			_model = model;
		}
	}
}
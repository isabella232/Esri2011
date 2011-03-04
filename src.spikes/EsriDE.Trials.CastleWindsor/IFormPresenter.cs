using System;
using System.Windows.Forms;

namespace EsriDE.Trials.CastleWindsor
{
	public interface IFormPresenter
	{
		void SetModel(IToggleFormVisibilityModel model);
		void UnsetModel();
	}

	class FormPresenter : IFormPresenter
	{
		private Guid _guid = Guid.NewGuid();

		private readonly IFormView _view;
		private readonly IAgdAdapter _agdAdapter;
		private IToggleFormVisibilityModel _model;

		public FormPresenter(IFormView view, IAgdAdapter agdAdapter)
		{
			Console.WriteLine("FormPresenter.ctor()");
			_view = view;
			_agdAdapter = agdAdapter;

			var timer = new Timer();
			timer.Interval = 2500;
			timer.Tick += Ticked;
			timer.Enabled = true;
		}

		private void Ticked(object sender, EventArgs e)
		{
			Console.WriteLine(_guid);
		}

		~FormPresenter()
		{
			Console.WriteLine("FormPresenter.~()");
		}

		public void SetModel(IToggleFormVisibilityModel model)
		{
			_model = model;
			_model.VisibilityChanged += VisibilityChangedHandler;
		}

		private void VisibilityChangedHandler(Visibility obj)
		{
			
		}

		public void UnsetModel()
		{
			_model.VisibilityChanged -= VisibilityChangedHandler;
			_model = null;
		}
	}
}
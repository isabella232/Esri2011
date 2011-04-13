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
		Timer timer = new Timer();

		private readonly IFormView _view;
		private readonly IAgdAdapter _agdAdapter;
		private IToggleFormVisibilityModel _model;

		public FormPresenter(IFormView view, IAgdAdapter agdAdapter)
		{
			Console.WriteLine("FormPresenter.ctor()");
			_view = view;
			_view.Closing += HandleViewClosed;
			_agdAdapter = agdAdapter;
			_view.Show();

			timer.Interval = 2500;
			timer.Tick += Ticked;
			timer.Enabled = true;
		}

		private void HandleViewClosed()
		{
			_model.ToggleVisibility();
			//_view.Closing -= HandleViewClosed;

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
			Console.WriteLine("FormPresenter.SetModel()");
			_model = model;
			_model.VisibilityChanged += VisibilityChangedHandler;
		}

		private void VisibilityChangedHandler(Visibility obj)
		{
			switch (obj)
			{
				case Visibility.Visible:
					Console.WriteLine("FormPresenter.VisibilityChangedHandler(visible)");
					break;
				case Visibility.Invisible:
					Console.WriteLine("FormPresenter.VisibilityChangedHandler(invisible)");
					_view.Closing -= HandleViewClosed;
					_view.Hide();
					break;
				default:
					throw new ArgumentOutOfRangeException("obj");
			}
		}

		public void UnsetModel()
		{
			Console.WriteLine("FormPresenter.UnsetModel()");
			_model.VisibilityChanged -= VisibilityChangedHandler;
			_model = null;
			timer.Enabled = false;
			//_view.Hide();
		}
	}
}
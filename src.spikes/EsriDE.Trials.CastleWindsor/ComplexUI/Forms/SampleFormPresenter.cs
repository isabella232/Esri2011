using System;
using System.Windows.Forms;
using EsriDE.Trials.CastleWindsor.ComplexUI.AA;

namespace EsriDE.Trials.CastleWindsor.ComplexUI.Forms
{
	internal class SampleFormPresenter : IFormPresenter
	{
		private readonly IAgdAdapter _agdAdapter;
		private readonly IFormView _view;
		private Guid _guid = Guid.NewGuid();
		private IToggleFormVisibilityModel _model;
		private Timer timer = new Timer();

		public SampleFormPresenter(IFormView view, IAgdAdapter agdAdapter)
		{
			Console.WriteLine("SampleFormPresenter.ctor()");
			_view = view;
			_view.Closing += HandleViewClosed;
			_agdAdapter = agdAdapter;
			_view.Show();

			timer.Interval = 2500;
			timer.Tick += Ticked;
			timer.Enabled = true;
		}

		#region IFormPresenter Members
		public void SetModel(IToggleFormVisibilityModel model)
		{
			Console.WriteLine("SampleFormPresenter.SetModel()");
			_model = model;
			_model.VisibilityChanged += VisibilityChangedHandler;
		}

		public void UnsetModel()
		{
			Console.WriteLine("SampleFormPresenter.UnsetModel()");
			_model.VisibilityChanged -= VisibilityChangedHandler;
			_model = null;
			timer.Enabled = false;
			//_view.Hide();
		}
		#endregion

		private void HandleViewClosed()
		{
			_model.ToggleVisibility();
			//_view.Closing -= HandleViewClosed;
		}

		private void Ticked(object sender, EventArgs e)
		{
			Console.WriteLine(_guid);
		}

		~SampleFormPresenter()
		{
			Console.WriteLine("SampleFormPresenter.~()");
		}

		private void VisibilityChangedHandler(VisibilityState obj)
		{
			switch (obj)
			{
				case VisibilityState.Visible:
					Console.WriteLine("SampleFormPresenter.VisibilityChangedHandler(visible)");
					break;
				case VisibilityState.Invisible:
					Console.WriteLine("SampleFormPresenter.VisibilityChangedHandler(invisible)");
					_view.Closing -= HandleViewClosed;
					_view.Hide();
					break;
				default:
					throw new ArgumentOutOfRangeException("obj");
			}
		}
	}
}
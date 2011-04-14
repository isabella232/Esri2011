using System;
using EsriDE.Trials.CastleWindsor.ComplexUI.AA;
using EsriDE.Trials.CastleWindsor.ComplexUI.Contracts.DomainModel;
using EsriDE.Trials.CastleWindsor.ComplexUI.Contracts.Forms;

namespace EsriDE.Trials.CastleWindsor.ComplexUI.Implementations.Forms
{
	internal class SampleFormPresenter : IFormPresenter
	{
		private readonly IAgdAdapter _agdAdapter;
		private readonly IFormView _view;
		private IToggleModel _model;

		public SampleFormPresenter(IToggleModel model, IFormView view, IAgdAdapter agdAdapter)
		{
			Console.WriteLine("SampleFormPresenter.ctor()");

			_model = model;
			_model.VisibilityStateChanged += VisibilityChangedHandler;

			_view = view;
			_view.Closing += HandleViewClosed;
			_view.Show();

			_agdAdapter = agdAdapter;
		}

		#region IFormPresenter Members
		//public void SetModel(IFormModel model)
		//{
		//    Console.WriteLine("SampleFormPresenter.SetModel()");

		//    _model = model;
		//    _model.VisibilityStateChanged += VisibilityChangedHandler;
		//}

		//public void UnsetModel()
		//{
		//    Console.WriteLine("SampleFormPresenter.UnsetModel()");

		//    CleanUp();
		//}
		#endregion

		private void HandleViewClosed()
		{
			_model.ToggleVisibility();
		}

		~SampleFormPresenter()
		{
			Console.WriteLine("SampleFormPresenter.~()");
		}

		private void VisibilityChangedHandler(VisibilityState state)
		{
			switch (state)
			{
				case VisibilityState.Visible:
					Console.WriteLine("SampleFormPresenter.VisibilityChangedHandler(visible)");
					break;
				case VisibilityState.Invisible:
					Console.WriteLine("SampleFormPresenter.VisibilityChangedHandler(invisible)");
					
					CleanUp();
					break;
				default:
					throw new ArgumentOutOfRangeException("state");
			}
		}

		private void CleanUp()
		{
			Console.WriteLine("SampleFormPresenter.CleanUp()");

			_view.Closing -= HandleViewClosed;
			_view.Hide();

			_model.VisibilityStateChanged -= VisibilityChangedHandler;
		}
	}
}
using EsriDE.Samples.ContentFinder.UI.Contract;

namespace EsriDE.Samples.ContentFinder.AgdAdapterCommons
{
	public class ShifterPresenter : IShifterPresenter
	{
		private readonly IShifterModel _model;
		private IShifterView _view;

		public ShifterPresenter(IShifterModel model)
		{
			_model = model;
			_model.ShifterStateChanged += SetButtonShifterState;
		}

		#region IButtonPresenter Members
		public void ConnectView(IShifterView view)
		{
			_view = view;
			_view.Clicked += Clicked;
		}
		#endregion

		protected void SetButtonShifterState(ShifterState state)
		{
			_view.SetShifterState(state);
		}

		protected void Clicked()
		{
			_model.ToggleShifter();
		}
	}
}
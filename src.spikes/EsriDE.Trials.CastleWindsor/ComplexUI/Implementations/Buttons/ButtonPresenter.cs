using System;
using EsriDE.Trials.CastleWindsor.ComplexUI.AA;
using EsriDE.Trials.CastleWindsor.ComplexUI.Contracts.Buttons;
using EsriDE.Trials.CastleWindsor.ComplexUI.Contracts.DomainModel;

namespace EsriDE.Trials.CastleWindsor.ComplexUI.Implementations.Buttons
{
	public class ButtonPresenter : IButtonPresenter
	{
		private readonly IToggleModel _model;
		private IButtonView _buttonView;

		public ButtonPresenter(IToggleModel model)
		{
			_model = model;
			_model.VisibilityStateChanged += SetButtonCheckedState;
		}

		#region IButtonPresenter Members
		public void ConnectView(IButtonView buttonView)
		{
			_buttonView = buttonView;
			_buttonView.Clicked += ButtonClicked;
		}
		#endregion

		protected void SetButtonCheckedState(VisibilityState visibilityState)
		{
			var checkedState = GetCheckedState(visibilityState);
			_buttonView.SetCheckedState(checkedState);
		}

		protected static CheckedState GetCheckedState(VisibilityState visibilityState)
		{
			switch (visibilityState)
			{
				case VisibilityState.Visible:
					return CheckedState.Checked;
				case VisibilityState.Invisible:
					return CheckedState.Unchecked;
				default:
					throw new ArgumentOutOfRangeException("visibilityState");
			}
		}

		protected void ButtonClicked()
		{
			_model.ToggleVisibility();
		}
	}
}
using System;

namespace EsriDE.Trials.CastleWindsor
{
	public class ButtonPresenter : IButtonPresenter
	{
		private readonly IToggleFormVisibilityModel _toggleFormVisibilityModel;
		private IButtonView _buttonView;

		public ButtonPresenter(IToggleFormVisibilityModel toggleFormVisibility)
		{
			_toggleFormVisibilityModel = toggleFormVisibility;
			_toggleFormVisibilityModel.VisibilityChanged += SetButtonCheckedState;
		}

		public void ConnectView(IButtonView buttonView)
		{
			_buttonView = buttonView;
			_buttonView.Clicked += ButtonClicked;
		}

		protected void SetButtonCheckedState(Visibility visibility)
		{
			var checkedState = GetCheckedState(visibility);
			_buttonView.SetCheckedState(checkedState);
		}

		protected static CheckedState GetCheckedState(Visibility visibility)
		{
			switch (visibility)
			{
				case Visibility.Visible:
					return CheckedState.Checked;
				case Visibility.Invisible:
					return CheckedState.Unchecked;
				default:
					throw new ArgumentOutOfRangeException("visibility");
			}
		}

		protected void ButtonClicked()
		{
			_toggleFormVisibilityModel.ToggleVisibility();
		}
	}
}
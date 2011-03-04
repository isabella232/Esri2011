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
		public void SetView(IButtonView buttonView)
		{
			_buttonView = buttonView;
			_buttonView.Clicked += ButtonClicked;
		}

		private void SetButtonCheckedState(Visibility visibility)
		{
			CheckedState checkedState = GetCheckedState(visibility);
			_buttonView.SetCheckedState(checkedState);
		}

		private static CheckedState GetCheckedState(Visibility visibility)
		{
			switch (visibility)
			{
				case Visibility.Visible:
					return CheckedState.Checked;
				case Visibility.Unvisible:
					return CheckedState.Unchecked;
				default:
					throw new ArgumentOutOfRangeException("visibility");
			}
		}

		private void ButtonClicked()
		{
			_toggleFormVisibilityModel.ToggleVisibility();
		}
	}
}
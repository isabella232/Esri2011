using System;

namespace EsriDE.Trials.CastleWindsor
{
	public class Button : AddInButton, IButtonView
	{
		public Button()
		{
			new Builder(this);
		}

		public override void OnClick()
		{
			Clicked();
		}

		public void SetCheckedState(CheckedState checkedState)
		{
			Checked = CheckedState.Checked == checkedState;
		}

		public event Action Clicked = delegate{};
	}
}
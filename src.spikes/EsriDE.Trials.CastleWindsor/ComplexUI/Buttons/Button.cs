using System;

namespace EsriDE.Trials.CastleWindsor.ComplexUI.Buttons
{
	public class Button : FakedAddInButton, IButtonView
	{
		public Button()
		{
			new Builder(this);
		}

		#region IButtonView Members
		public event Action Clicked = delegate { };

		public void SetEnablesState(EnabledState enabledState)
		{
			Enabled = EnabledState.Enabled == enabledState;
		}

		public void SetCheckedState(CheckedState checkedState)
		{
			Checked = CheckedState.Checked == checkedState;
		}
		#endregion

		public override void OnClick()
		{
			Clicked();
		}
	}
}
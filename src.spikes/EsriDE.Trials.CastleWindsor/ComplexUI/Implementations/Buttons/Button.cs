using System;
using EsriDE.Trials.CastleWindsor.ComplexUI.AA;
using EsriDE.Trials.CastleWindsor.ComplexUI.Contracts.Buttons;
using EsriDE.Trials.CastleWindsor.ComplexUI.Contracts.DomainModel;

namespace EsriDE.Trials.CastleWindsor.ComplexUI.Implementations.Buttons
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
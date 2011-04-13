using System;

namespace EsriDE.Trials.CastleWindsor.ComplexUI.Buttons
{
	public interface IButtonView
	{
		void SetCheckedState(CheckedState checkedState);
		void SetEnablesState(EnabledState enabledState);
		event Action Clicked;
	}
}
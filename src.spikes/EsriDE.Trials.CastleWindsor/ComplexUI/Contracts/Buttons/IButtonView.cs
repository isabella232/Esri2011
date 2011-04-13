using System;
using EsriDE.Trials.CastleWindsor.ComplexUI.Contracts.DomainModel;

namespace EsriDE.Trials.CastleWindsor.ComplexUI.Contracts.Buttons
{
	public interface IButtonView
	{
		void SetCheckedState(CheckedState checkedState);
		void SetEnablesState(EnabledState enabledState);
		event Action Clicked;
	}
}
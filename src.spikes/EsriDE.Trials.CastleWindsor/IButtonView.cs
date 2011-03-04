using System;

namespace EsriDE.Trials.CastleWindsor
{
	public interface IButtonView
	{
		void SetCheckedState(CheckedState checkedState);
		event Action Clicked;
	}
}
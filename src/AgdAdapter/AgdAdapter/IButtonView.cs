using System;

namespace EsriDE.Samples.ContentFinder.AgdAdapter
{
	public interface IButtonView
	{
		event Action Clicked;
		void SetCheckedState(CheckedState checkedState);
		void SetEnabledState(EnabledState enabledState);
	}

	public enum EnabledState
	{
		Enabled,
		Disabled
	}

	public enum CheckedState
	{
		Checked,
		Unchecked
	}
}
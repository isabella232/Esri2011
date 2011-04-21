using System;
using AddInButton = ESRI.ArcGIS.Desktop.AddIns.Button;

namespace EsriDE.Samples.ContentFinder.AgdAdapter
{
	public abstract class Button : AddInButton, IButtonView
	{
		public event Action Clicked;

		public void SetCheckedState(CheckedState checkedState)
		{
			Checked = CheckedState.Checked == checkedState;
		}

		public void SetEnabledState(EnabledState enabledState)
		{
			Enabled = EnabledState.Enabled == enabledState;
		}

		protected abstract IButtonPresenter CreatePresenter();
	}
}
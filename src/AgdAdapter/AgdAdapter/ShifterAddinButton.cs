using System;
using ESRI.ArcGIS.Desktop.AddIns;
using EsriDE.Samples.ContentFinder.UI.Contract;

namespace EsriDE.Samples.ContentFinder.AgdAdapter
{
	public abstract class ShifterAddinButton : Button, IShifterView
	{
		public virtual void SetShifterState(ShifterState state)
		{
			Checked = ShifterState.On == state;
		}

		public virtual void SetEnabledState(EnabledState state)
		{
			Enabled = EnabledState.Enabled == state;
		}

		private event Action _clicked;

		public event Action Clicked
		{
			add { _clicked += value;  }
			remove { _clicked -= value; }
		}

		protected override void OnClick()
		{
			OnClicked();
		}

		protected virtual void OnClicked()
		{
			var clicked = _clicked;
			if (null != clicked)
			{
				clicked();
			}
		}
	}
}
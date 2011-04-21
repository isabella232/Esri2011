using System;

namespace EsriDE.Samples.ContentFinder.UI.Contract
{
	public interface IShifterView
	{
		void SetShifterState(ShifterState shifterState);
		void SetEnabledState(EnabledState enabledState);
		event Action Clicked;
	}
}
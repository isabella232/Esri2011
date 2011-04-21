using System;

namespace EsriDE.Samples.ContentFinder.UI.Contract
{
	public interface IShifterModel
	{
		ShifterState ShifterState { get; }

		void ToggleShifter();
		event Action<ShifterState> ShifterStateChanged;
	}
}
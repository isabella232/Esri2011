using System;
using EsriDE.Trials.CastleWindsor.ComplexUI.Contracts.DomainModel;

namespace EsriDE.Trials.CastleWindsor.ComplexUI.AA
{
	public interface IToggleModel
	{
		VisibilityState VisibilityState { get; }

		void ToggleVisibility();
		event Action<VisibilityState> VisibilityStateChanged;
	}
}
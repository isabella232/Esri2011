using System;
using EsriDE.Trials.CastleWindsor.ComplexUI.AA;
using EsriDE.Trials.CastleWindsor.ComplexUI.Contracts.DomainModel;

namespace EsriDE.Trials.CastleWindsor.ComplexUI.Implementations
{
	public class ToggleModel : IToggleModel
	{
		private VisibilityState _visibilityState = VisibilityState.Invisible;
		public VisibilityState VisibilityState
		{
			get { return _visibilityState; }
			set
			{
				if (value != _visibilityState)
				{
					_visibilityState = value;
					VisibilityStateChanged(_visibilityState);
				}
			}
		}

		public void ToggleVisibility()
		{
			switch (VisibilityState)
			{
				case VisibilityState.Visible:
					VisibilityState = VisibilityState.Invisible;
					break;
				case VisibilityState.Invisible:
					VisibilityState = VisibilityState.Visible;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public event Action<VisibilityState> VisibilityStateChanged = delegate { };
	}
}
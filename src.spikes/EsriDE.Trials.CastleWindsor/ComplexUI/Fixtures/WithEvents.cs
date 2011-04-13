using System;
using EsriDE.Trials.CastleWindsor.ComplexUI.Contracts.DomainModel;

namespace EsriDE.Trials.CastleWindsor.ComplexUI.Fixtures
{
	public class WithEvents : IWithEvents
	{
		#region IWithEvents Members
		public event Action<VisibilityState> Blah;

		public void RaiseEvent(VisibilityState visibilityState)
		{
			if (Blah != null)
				Blah(visibilityState);
		}
		#endregion
	}
}
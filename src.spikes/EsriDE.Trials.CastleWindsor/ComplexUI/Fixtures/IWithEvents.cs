using System;
using EsriDE.Trials.CastleWindsor.ComplexUI.Forms;

namespace EsriDE.Trials.CastleWindsor.ComplexUI.Fixtures
{
	public interface IWithEvents
	{
		event Action<VisibilityState> Blah;
		void RaiseEvent(VisibilityState visibilityState);
	}
}
using System;

namespace EsriDE.Trials.CastleWindsor
{
	public class WithEvents : IWithEvents
	{
		public event Action<Visibility> Blah;

		public void RaiseEvent(Visibility visibility)
		{
			if (Blah != null)
				Blah(visibility);
		}
	}
}
using System;

namespace EsriDE.Trials.CastleWindsor
{
	public interface IWithEvents
	{
		event Action<Visibility> Blah;
		void RaiseEvent(Visibility visibility);
	}
}
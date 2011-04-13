using System;

namespace EsriDE.Trials.CastleWindsor.ComplexUI
{
	public interface IContentModel
	{
		event Action<object> ContentAdded;
		event Action<object> ContentSelected;
	}
}
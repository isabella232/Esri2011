using System;

namespace EsriDE.Trials.CastleWindsor
{
	public interface IContentModel
	{
		event Action<object> ContentAdded;
		event Action<object> ContentSelected;
	}

	public class ContentModel : IContentModel
	{
		public event Action<object> ContentAdded;
		public event Action<object> ContentSelected;
	}
}
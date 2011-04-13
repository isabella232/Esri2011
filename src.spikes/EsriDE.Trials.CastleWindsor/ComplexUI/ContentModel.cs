using System;

namespace EsriDE.Trials.CastleWindsor.ComplexUI
{
	public class ContentModel : IContentModel
	{
		#region IContentModel Members
		public event Action<object> ContentAdded;
		public event Action<object> ContentSelected;
		#endregion
	}
}
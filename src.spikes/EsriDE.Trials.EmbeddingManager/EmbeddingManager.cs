using System;
using WpfPanel = System.Windows.Controls.Canvas;
using WpfWindow = System.Windows.Window;

namespace EsriDE.Trials.EmbeddingManager
{
	public interface IEmbeddingManager<T>
	{
		void EmbedControl(Action<T> embedControl);
	}

	public class EmbeddingManager<THost, TEmbedded> : IEmbeddingManager<THost>
	{
		private readonly TEmbedded _embeddedControl;
		private readonly Func<TEmbedded, THost> _wrapControl;

		public EmbeddingManager(TEmbedded embeddedControl, Func<TEmbedded, THost> wrapControl)
		{
			_embeddedControl = embeddedControl;
			_wrapControl = wrapControl;
		}

		#region IEmbeddingManager<THost> Members

		public virtual void EmbedControl(Action<THost> embedControl)
		{
			var c = _wrapControl(_embeddedControl);
			embedControl(c);
		}

		#endregion
	}
}
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
			//bettet das "fremde" UI-Control (z.B. WinForms-UserControl) in ein hostendes UI-Control der aktuelle UI-Platform ein (z.B. Wpf-WindowsFormsHost)
			var inHostWrappedControl = _wrapControl(_embeddedControl);

			//fügt das hostende UserControl in einen UI-Container ein (z.B. auf das Wpf-Formular)
			embedControl(inHostWrappedControl);
		}

		#endregion
	}
}
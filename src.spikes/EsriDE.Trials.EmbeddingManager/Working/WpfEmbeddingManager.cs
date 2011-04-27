using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace EsriDE.Trials.EmbeddingManager.Working
{
	public class WpfEmbeddingManager<TEmbedded> : EmbeddingManager<UIElement, TEmbedded>
	{
		//private readonly TEmbedded _embeddedControl;
		//private readonly Func<TEmbedded, UIElement> _wrapControl;

		public WpfEmbeddingManager(TEmbedded embeddedControl, Func<TEmbedded, UIElement> wrapControl)
			: base(embeddedControl, wrapControl)
		{
			//_embeddedControl = embeddedControl;
			//_wrapControl = wrapControl;
		}

		//#region IEmbeddingManager<THost> Members

		//public override void EmbedControl(Action<UIElement> embedControl)
		//{
		//    //bettet das "fremde" UI-Control (z.B. WinForms-UserControl) in ein hostendes UI-Control der aktuelle UI-Platform ein (z.B. Wpf-WindowsFormsHost)
		//    var inHostWrappedControl = _wrapControl(_embeddedControl);

		//    //fügt das hostende UserControl in einen UI-Container ein (z.B. auf das Wpf-Formular)
		//    embedControl(inHostWrappedControl);
		//}

		//#endregion

		public override void EmbedControl()
		{
			
		}
	}

	public class WinInWpfEmbeddingManager : WpfEmbeddingManager<Control>
	{
		//private readonly TEmbedded _embeddedControl;
		//private readonly Func<TEmbedded, UIElement> _wrapControl;

		public WinInWpfEmbeddingManager(Control embeddedControl)
			: base(embeddedControl, c => new WindowsFormsHost { Child = c })
		{
		}
	}
}
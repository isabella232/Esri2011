using System;
using System.Windows.Forms;

namespace EsriDE.Trials.EmbeddingManager
{
	//EmbeddingManager<THost, TEmbedded> : IEmbeddingManager<THost>
	public class WinInWinEmbeddingManager : EmbeddingManager<Control, Control>
	{
		public WinInWinEmbeddingManager(Control embeddedControl, Func<Control, Control> wrapControl)
			: base(embeddedControl, wrapControl)
		{
			Console.WriteLine("WinInWinEmbeddingManager.ctor(.., ..)");
		}
	}
}
using System;
using System.Windows.Forms;

namespace EsriDE.Trials.EmbeddingManager
{
	public partial class WinFormsFormView : Form, IFormView
	{
		private readonly IEmbeddingManager<Control> _embeddingManager;

		public WinFormsFormView()
		{
			InitializeComponent();
		}

		//public WinFormsFormView(IEmbeddingManager<Control> embeddingManager)
		//{
		//    InitializeComponent();
		//    _embeddingManager = embeddingManager;
		//}

		public WinFormsFormView(Func<IEmbeddingManager<Control>> embeddingManagerFunc)
			: this()
		{
			_embeddingManager = embeddingManagerFunc();
		}

		public void ShowView(string text)
		{
			Console.WriteLine("WinFormsFormView.ShowView()");
			_embeddingManager.EmbedControl(EmbedControlHere);
			ShowDialog();
			ViewShowed(text);
		}

		public event Action<string> ViewShowed = delegate { };

		private void EmbedControlHere(Control obj)
		{
			Console.WriteLine("WinFormsFormView.EmbedControlHere('" + obj + "')");

			panel1.Controls.Add(obj);
		}
	}
}

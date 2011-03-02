using System.Windows.Forms;

namespace EsriDE.Trials.EmbeddingManager
{
	public partial class WinFormsWindow : Form
	{
		private readonly IEmbeddingManager<Control> _manager;

		public WinFormsWindow()
		{
			InitializeComponent();
		}

		public WinFormsWindow(IEmbeddingManager<Control> manager)
			: this()
		{
			_manager = manager;
		}

		public void ShowWindow()
		{
			_manager.EmbedControl(EmbedControl);
			ShowDialog(); //modal showing
		}

		public void EmbedControl(Control element)
		{
			panel1.Controls.Add(element);
		}
	}
}
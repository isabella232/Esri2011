using System.Windows;
using System.Windows.Threading;
using WinFormsControl = System.Windows.Forms.Control;

namespace EsriDE.Trials.EmbeddingManager
{
	/// <summary>
	///   Interaction logic for WpfWindow.xaml
	/// </summary>
	public partial class WpfWindow
	{
		private readonly IEmbeddingManager<UIElement> _manager;

		protected WpfWindow()
		{
			InitializeComponent();
		}

		public WpfWindow(IEmbeddingManager<UIElement> manager) : this()
		{
			_manager = manager;
		}

		public void ShowWindow()
		{
			_manager.EmbedControl(EmbedControl);
			ShowDialog();
		}

		private void EmbedControl(UIElement element)
		{
			if (!Dispatcher.CheckAccess())
			{
				Dispatcher.Invoke(DispatcherPriority.Normal, new ThreadHelper(EmbedControl), element);
			}
			else
			{
				_userControlContainer.Children.Add(element);
			}
		}
	}

	internal delegate void ThreadHelper(UIElement element);
}
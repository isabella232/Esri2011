using System;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Interop;
using EsriDE.Samples.ContentFinder.UI.Contract;

namespace EsriDE.Samples.ContentFinder.WpfUI
{
	/// <summary>
	/// Interaction logic for ContentForm.xaml
	/// </summary>
	public partial class ContentForm : IToggleableForm
	{
		private readonly IWindowInformation _windowInformation;
		private bool _isClosing;

		//public ContentForm()
		//{
		//    InitializeComponent();
		//}

		public ContentForm(IWindowInformation windowInformation, IPortal portal)
		{
			_windowInformation = windowInformation;
			InitializeComponent();

			SetParent();
			this.HostingContainer.Children.Add((UserControl)portal);
		}

		public void SetParent()
		{
			new WindowInteropHelper(this)
			{
				Owner = (IntPtr)_windowInformation.WindowHandle
			};
		}

		void IToggleableView.Show()
		{
			Show();
		}

		void IToggleableView.Hide()
		{
			// innerhalb des Closing-Event-Handlings darf Close, Show, u.ä. nicht aufgerufen werden
			// deshalb verwenden wir den Boolean _isClosing als Marker
			if (!_isClosing)
			{
				Close();
			}
			//Dispose(true);
		}

		private event Action _closing = delegate { };
		event Action IToggleableForm.Closing
		{
			add { _closing += value; }
			remove { _closing -= value; }
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			_isClosing = true;
			_closing();
		}

		private void Window_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
		{
			Debug.WriteLine("New: " + e.NewSize);
			Debug.WriteLine("Old: " + e.PreviousSize);
		}
	}
}

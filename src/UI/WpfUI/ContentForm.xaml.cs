using System;
using EsriDE.Samples.ContentFinder.UI.Contract;

namespace EsriDE.Samples.ContentFinder.WpfUI
{
	/// <summary>
	/// Interaction logic for ContentForm.xaml
	/// </summary>
	public partial class ContentForm : IToggleableForm
	{
		public ContentForm()
		{
			InitializeComponent();
		}

		public void SetParent()
		{
			return;
			throw new NotImplementedException();
		}

		void IToggleableView.Show()
		{
			Show();
		}

		void IToggleableView.Hide()
		{
			Close();
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
			_closing();
		}


	}
}

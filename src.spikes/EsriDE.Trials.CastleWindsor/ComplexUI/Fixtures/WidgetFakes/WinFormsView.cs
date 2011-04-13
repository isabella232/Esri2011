using System;
using System.Windows.Forms;
using EsriDE.Trials.CastleWindsor.ComplexUI.Contracts.Forms;

namespace EsriDE.Trials.CastleWindsor.ComplexUI.Fixtures.WidgetFakes
{
	public partial class WinFormsView : Form, IFormView
	{
		public WinFormsView()
		{
			InitializeComponent();
		}

		#region IFormView Members
		void IFormView.SetParent()
		{
			return;
			throw new NotImplementedException();
		}

		void IFormView.Show()
		{
			Show();
		}

		void IFormView.Hide()
		{
			Close();
			Dispose(true);
		}

		event Action IFormView.Closing
		{
			add { _closing += value; }
			remove { _closing -= value; }
		}
		#endregion

		public void SetParent()
		{
		}

		private event Action _closing = delegate { };

		private void WinFormsView_FormClosed(object sender, FormClosedEventArgs e)
		{
			_closing();
		}
	}
}
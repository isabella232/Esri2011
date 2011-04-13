using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EsriDE.Trials.CastleWindsor
{
	public partial class WinFormsView : Form, IFormView
	{
		public WinFormsView()
		{
			InitializeComponent();
		}

		public void SetParent()
		{
			
		}

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

		private event Action _closing = delegate { }; 
		event Action IFormView.Closing
		{
			add { _closing += value; }
			remove { _closing -= value; }
		}

		private void WinFormsView_FormClosed(object sender, FormClosedEventArgs e)
		{
			_closing();
		}
	}
}

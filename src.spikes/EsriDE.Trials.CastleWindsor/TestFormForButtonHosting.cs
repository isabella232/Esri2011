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
	public partial class TestFormForButtonHosting : Form
	{
		private Button _button = new Button();
		public TestFormForButtonHosting()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			_button.OnClick();
		}
	}
}

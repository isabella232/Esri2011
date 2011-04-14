using System;
using System.Windows.Forms;
using Button = EsriDE.Trials.CastleWindsor.ComplexUI.Fixtures.WidgetFakes.Button;

namespace EsriDE.Trials.CastleWindsor.ComplexUI.Fixtures.WidgetFakes
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
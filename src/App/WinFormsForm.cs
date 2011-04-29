using System.Windows.Forms;

namespace App
{
	public partial class WinFormsForm : Form
	{
		public WinFormsForm()
		{
			InitializeComponent();
		}

		public WinFormsForm(Control uc) : this()
		{
			panel1.Controls.Add(uc);
			uc.Dock = DockStyle.Fill;
		}
	}
}

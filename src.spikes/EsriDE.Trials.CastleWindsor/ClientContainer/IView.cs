using System;
using System.Windows.Forms;

namespace EsriDE.Trials.CastleWindsor.ClientContainer
{
	public interface IView
	{
		void ShowView(Action<bool> action);
		void CloseView();
	}

	//public class TestForm : Form, IView
	//{
	//    public void ShowView()
	//    {
			
	//    }
	//}
}
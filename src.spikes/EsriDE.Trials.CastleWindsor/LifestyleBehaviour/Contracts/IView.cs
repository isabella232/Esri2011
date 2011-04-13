using System;

namespace EsriDE.Trials.CastleWindsor.LifestyleBehaviour.Contracts
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
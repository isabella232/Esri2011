using System;
using System.Runtime.InteropServices;

namespace EsriDE.Trials.CastleWindsor.ClientContainer
{
	public class FakeView : IView
	{
		private Action<bool> _action;

		#region IView Members
		public void ShowView(Action<bool> action)
		{
			Console.WriteLine("FakeView.ShowView");
			_action = action;
		}

		public void CloseView()
		{
			Console.WriteLine("FakeView.CloseView");
		}
		#endregion

		~FakeView()
		{
			Console.WriteLine("~FakeView()");
			_action(true);
		}
	}
}
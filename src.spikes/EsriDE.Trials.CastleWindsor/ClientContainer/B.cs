using System;
using System.Timers;

namespace EsriDE.Trials.CastleWindsor.ClientContainer
{
	public interface IB
	{
		void SetAction(Action<bool> action);
	}

	public class B : IB
	{
		private Action<bool> _action = delegate { };

		public B()
		{
			Console.WriteLine("B.ctor()");

			//var timer = new Timer();
			//timer.Interval = 2500;
			//timer.Elapsed += DoSomething;
			//timer.Enabled = true;
		}

		private void DoSomething(object sender, ElapsedEventArgs e)
		{
			Console.WriteLine("B.DoSomething");
		}

		~B()
		{
			Console.WriteLine("~B()");
			_action(true);
		}

		public void SetAction(Action<bool> action)
		{
			Console.WriteLine("B.SetAction()");
			_action = action;
		}
	}
}
using System;
using System.Timers;

namespace EsriDE.Trials.CastleWindsor.LifestyleBehaviour
{
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

		#region IB Members
		public void SetAction(Action<bool> action)
		{
			Console.WriteLine("B.SetAction()");
			_action = action;
		}
		#endregion

		private void DoSomething(object sender, ElapsedEventArgs e)
		{
			Console.WriteLine("B.DoSomething");
		}

		~B()
		{
			Console.WriteLine("~B()");
			_action(true);
		}
	}
}
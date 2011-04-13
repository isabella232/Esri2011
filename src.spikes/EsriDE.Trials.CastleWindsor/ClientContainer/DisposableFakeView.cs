using System;

namespace EsriDE.Trials.CastleWindsor.ClientContainer
{
	public class DisposableFakeView : IView, IDisposable
	{
		private Action<bool> _action;

		#region IDisposable Members
		public void Dispose()
		{
			Console.WriteLine("DisposableFakeView.Dispose()");
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		#endregion

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				Console.WriteLine("DisposableFakeView.Dispose(true)");
			}
			else
			{
				Console.WriteLine("DisposableFakeView.Dispose(false)");
			}

			_action(true);
		}

		#region IView Members
		public void ShowView(Action<bool> action)
		{
			Console.WriteLine("DisposableFakeView.ShowView");
			_action = action;
		}

		public void CloseView()
		{
			Console.WriteLine("DisposableFakeView.CloseView");
		}
		#endregion

		~DisposableFakeView()
		{
			Console.WriteLine("~DisposableFakeView()");
			Dispose(false);
		}
	}
}
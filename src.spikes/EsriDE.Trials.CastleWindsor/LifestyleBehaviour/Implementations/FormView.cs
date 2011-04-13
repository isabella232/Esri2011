using System;
using System.Diagnostics;
using System.Windows.Forms;
using EsriDE.Trials.CastleWindsor.LifestyleBehaviour.Contracts;

namespace EsriDE.Trials.CastleWindsor.LifestyleBehaviour.Implementations
{
	public partial class FormView : Form, IView
	{
		private Action<bool> _action;

		public FormView()
		{
			InitializeComponent();
		}

		#region IView Members
		public void ShowView(Action<bool> action)
		{
			Console.WriteLine("FormView.ShowView()");
			_action = action;
			Show();
		}

		public void CloseView()
		{
			Console.WriteLine("FormView.CloseView()");

			//calls only SomeForm_FormClosing and SomeForm_FormClosed
			//http://www.alwaysgetbetter.com/blog/2008/04/04/c-formclose-vs-formdispose/
			Close();

			//Console.WriteLine("FormView.CloseView() - and now Dispose()");
			//Dispose();
		}
		#endregion

		~FormView()
		{
			Console.WriteLine("~FormView()");
			Trace.WriteLine("~FormView()");
			_action(true);
		}

		private void SomeForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			Console.WriteLine("SomeForm_FormClosing");
		}

		private void SomeForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			Console.WriteLine("SomeForm_FormClosed");
			Console.WriteLine("FormView.SomeForm_FormClosed - and now Dispose()");
			Dispose();
		}
	}
}
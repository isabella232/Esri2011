using System;
using System.Diagnostics;
using System.Windows.Forms;
using EsriDE.Trials.CastleWindsor.LifestyleBehaviour.Contracts;

namespace EsriDE.Trials.CastleWindsor.LifestyleBehaviour.Implementations
{
	public partial class SomeForm : Form, IView
	{
		private Action<bool> _action;

		public SomeForm()
		{
			InitializeComponent();
		}

		#region IView Members
		public void ShowView(Action<bool> action)
		{
			Console.WriteLine("SomeForm.ShowView()");
			_action = action;
			Show();
		}

		public void CloseView()
		{
			Console.WriteLine("SomeForm.CloseView()");

			//calls only SomeForm_FormClosing and SomeForm_FormClosed
			//http://www.alwaysgetbetter.com/blog/2008/04/04/c-formclose-vs-formdispose/
			Close();

			//Console.WriteLine("SomeForm.CloseView() - and now Dispose()");
			//Dispose();
		}
		#endregion

		~SomeForm()
		{
			Console.WriteLine("~SomeForm()");
			Trace.WriteLine("~SomeForm()");
			_action(true);
		}

		private void SomeForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			Console.WriteLine("SomeForm_FormClosing");
		}

		private void SomeForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			Console.WriteLine("SomeForm_FormClosed");
			Console.WriteLine("SomeForm.SomeForm_FormClosed - and now Dispose()");
			Dispose();
		}
	}
}
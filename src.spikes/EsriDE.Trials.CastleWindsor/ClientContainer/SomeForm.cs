using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EsriDE.Trials.CastleWindsor.ClientContainer
{
	public partial class SomeForm : Form, IView
	{
		private Action<bool> _action;

		public SomeForm()
		{
			InitializeComponent();
		}

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

		~SomeForm()
		{
			Console.WriteLine("~SomeForm()");
			System.Diagnostics.Trace.WriteLine("~SomeForm()");
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

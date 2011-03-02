using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Threading;
using NUnit.Framework;

namespace EsriDE.Trials.EmbeddingManager
{
	[TestFixture]
	public class Fixture
	{
		[Test]
		[RequiresSTA]
		public void EmbedWin_InWin_Works()
		{
			var winFormsUserControl = new WinFormsUserControl();
			var manager = new EmbeddingManager<Control, Control>(winFormsUserControl, control => control);

			var winFormsWindow = new WinFormsWindow(manager);
			winFormsWindow.ShowWindow();
		}

		[Test]
		[RequiresSTA]
		public void EmbedWin_InWpf_Works()
		{
			var winFormsUserControl = new WinFormsUserControl();
			var manager = new EmbeddingManager<UIElement, Control>(
				winFormsUserControl, c => new WindowsFormsHost {Child = c});

			var wpfWindow = new WpfWindow(manager);
			wpfWindow.ShowWindow();

			//http://sunshaking.blogspot.com/2008/10/tricks-for-writing-unit-tests-for-wpf.html
			Dispatcher.CurrentDispatcher.InvokeShutdown();
		}

		[Test]
		[RequiresSTA]
		public void EmbedWpf_InWin_Works()
		{
			var wpfUserControl = new WpfUserControl();
			var manager = new EmbeddingManager<Control, UIElement>(
				wpfUserControl, uiElement => new ElementHost {Child = uiElement});

			var winFormsWindow = new WinFormsWindow(manager);
			winFormsWindow.ShowWindow();

			//http://sunshaking.blogspot.com/2008/10/tricks-for-writing-unit-tests-for-wpf.html
			Dispatcher.CurrentDispatcher.InvokeShutdown();
		}

		[Test]
		[RequiresSTA]
		public void EmbedWpf_InWpf_Works()
		{
			var wpfUserControl = new WpfUserControl();
			var manager = new EmbeddingManager<UIElement, UIElement>(wpfUserControl, uiElement => uiElement);

			var wpfWindow = new WpfWindow(manager);
			wpfWindow.ShowWindow();

			//http://sunshaking.blogspot.com/2008/10/tricks-for-writing-unit-tests-for-wpf.html
			Dispatcher.CurrentDispatcher.InvokeShutdown();
		}
	}
}
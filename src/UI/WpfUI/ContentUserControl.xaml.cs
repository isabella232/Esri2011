using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using EsriDE.Commons.Aop;
using EsriDE.Samples.ContentFinder.BL.Contract;
using EsriDE.Samples.ContentFinder.DomainModel;
using EsriDE.Samples.ContentFinder.UI.Contract;

namespace EsriDE.Samples.ContentFinder.WpfUI
{
	/// <summary>
	/// Interaction logic for ContentUserControl.xaml
	/// </summary>
	public partial class ContentUserControl : IPortal
	{
		private IController _controller;
		private volatile ContentObservableCollection _contentObservableCollection;

		public ContentUserControl()
		{
			InitializeComponent();

			_contentObservableCollection = (ContentObservableCollection)Resources["contentItems"];
		}

		public IController Controller
		{
			get { return _controller; }
			set
			{
				if (null != _controller)
				{
					_controller.ContentFound -= ImportContentIndirect;
				}

				_controller = value;
				_controller.ContentFound += ImportContentIndirect;
				_controller.Start();
			}
		}

		private IContentProcessorAdapter _contentProcessorAdapter;
		public IContentProcessorAdapter ContentProcessorAdapter
		{
			get { return _contentProcessorAdapter; }
			set
			{
				if (null != _contentProcessorAdapter)
				{
					this.ContentSelected -= _contentProcessorAdapter.Process;
				}

				_contentProcessorAdapter = value;
				this.ContentSelected += _contentProcessorAdapter.Process;
			}
		}

		////Todo: Wrap in AOP
		//public void ImportContentClassical(Content c)
		//{
		//    try
		//    {
		//        if (!Dispatcher.CheckAccess())
		//        {
		//            Dispatcher.Invoke(DispatcherPriority.Background, new Action<Content>(ImportContentClassical), c);
		//        }
		//        else
		//        {
		//            try
		//            {
		//                AddContentToModel(c);
		//                RefreshListbox();
		//            }
		//            catch (Exception e)
		//            {
		//                Console.WriteLine(e);
		//            }
		//        }
		//    }
		//    catch (Exception e)
		//    {
		//        Console.WriteLine(e);
		//    }
		//}

		private void ImportContentIndirect(Content c)
		{
			ImportContent(c);
		}

		[GuiDispatchingAspect(Priority = DispatcherPriority.Background)]
		private void ImportContent(Content c)
		{
			try
			{
				AddContentToModel(c);
				RefreshListbox();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		private void RefreshListbox()
		{
			ContentListBox.Items.Refresh();
		}

		private void AddContentToModel(Content c)
		{
			_contentObservableCollection.Add(c);
		}

		private void HandleBorderMouseEnter(object sender, MouseEventArgs e)
		{
			var border = (Border)sender;
			border.BorderThickness = new Thickness(1);


			border.BorderBrush = new SolidColorBrush(Color.FromRgb(30, 51, 120));
			var grid = (Grid)border.Child;
			var panel = (StackPanel)grid.Children[1];
			panel.Background = new SolidColorBrush(Color.FromRgb(30, 51, 120));
			var tblocktop = (TextBlock)panel.Children[0];
			tblocktop.Foreground = new SolidColorBrush(Colors.White);
			var tblockbottom = (TextBlock)panel.Children[2];
			tblockbottom.Foreground = new SolidColorBrush(Colors.White);
		}

		private void HandleBorderMouseLeave(object sender, MouseEventArgs e)
		{
			var border = (Border)sender;
			border.BorderThickness = new Thickness(0);
			border.BorderBrush = new SolidColorBrush(Colors.Transparent);
			var grid = (Grid)border.Child;
			var panel = (StackPanel)grid.Children[1];
			panel.Background = new SolidColorBrush(Colors.Transparent);
			var tblocktop = (TextBlock)panel.Children[0];
			tblocktop.Foreground = new SolidColorBrush(Colors.Black);
			var tblockbottom = (TextBlock)panel.Children[2];
			tblockbottom.Foreground = new SolidColorBrush(Colors.Black);
		}

		private void HandleBorderMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			Cursor = Cursors.Wait;

			try
			{
				var border = (Border)sender;
				var grid = (Grid)border.Child;
				var panel = (StackPanel)grid.Children[1];
				var uri = (Uri)panel.Tag;
				ProcessSelectedContent(uri);
			}
			finally
			{
				Cursor = Cursors.Arrow;
			}
		}

		private void ProcessSelectedContent(Uri uri)
		{
			Content content;
			if (_contentObservableCollection.TryGetContentItem(uri, out content))
			{
				OnContentSelected(content);
			}
		}

		protected virtual void OnContentSelected(Content content)
		{
			var contentSelected = _contentSelected;
			if (null != contentSelected)
			{
				contentSelected(content);
			}
		}

		private event Action<Content> _contentSelected;
		public event Action<Content> ContentSelected
		{
			add { _contentSelected += value; }
			remove { _contentSelected -= value; }
		}
	}
}

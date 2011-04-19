using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using EsriDE.Samples.ContentFinder.DomainModel;
using EsriDE.Samples.ContentLoader.UI.Wpf;

namespace EsriDE.Samples.ContentFinder.WpfUI
{
	internal delegate void ItemDateImportThreadHelper(Content c);

	public class ContentObservableCollection : ObservableCollectionEx<Content>
	{ }

	/// <summary>
	/// Interaction logic for ContentUserControl.xaml
	/// </summary>
	public partial class ContentUserControl
	{
		private IContentProvider _provider;
		private volatile ContentObservableCollection _contentObservableCollection;

		public ContentUserControl()
		{
			InitializeComponent();

			_contentObservableCollection = (ContentObservableCollection)Resources["contentItems"];

		}

		public void SetProvider(IContentProvider provider)
		{
			_provider = provider;
			_provider.NewContent += ImportContent;
		}

		public void ImportContent(Content c)
		{
			Console.WriteLine("Content received");

			if (!Dispatcher.CheckAccess())
			{
				Dispatcher.Invoke(DispatcherPriority.Background,
								  new ItemDateImportThreadHelper(ImportContent), c);
			}
			else
			{
				try
				{
					ImportAsMapDocumentItem(c);
					RefreshMapDocumentListbox();
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
			}
		}

		private void RefreshMapDocumentListbox()
		{
			ContentListBox.Items.Refresh();
		}

		private void ImportAsMapDocumentItem(Content c)
		{
			_contentObservableCollection.Add(c);
			//_mapDocumentItemObservableCollection.Add(mapDocumentItem);
		}

		private void HandleBorderMouseEnter(object sender, MouseEventArgs e)
		{
			var border = (Border)sender;
			border.BorderThickness = new Thickness(1);
			//border.BorderBrush = new SolidColorBrush(Colors.Red);


			border.BorderBrush = new SolidColorBrush(Color.FromRgb(30, 51, 120));
			var grid = (Grid)border.Child;
			var panel = (StackPanel)grid.Children[1];
			//panel.Background = new SolidColorBrush(Colors.Gray);
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
				var guid = (Guid)panel.Tag;
				//StartArcMapOperation(guid);
			}
			finally
			{
				Cursor = Cursors.Arrow;
			}
		}

		
	}
}

using System;
using System.Drawing;
using System.Windows.Controls;
using EsriDE.Samples.ContentFinder.DomainModel;

namespace EsriDE.Samples.ContentFinder.WpfUI.Tests
{
	/// <summary>
	/// Interaction logic for TestWindow.xaml
	/// </summary>
	public partial class TestWindow : IContentProvider
	{
		public TestWindow()
		{
			InitializeComponent();

			contentUserControl.SetProvider(this);

			Do();
		}

		public event Action<Content> NewContent;

		public void Do()
		{
			Content content;
			for (var i = 0; i < 100; i++)
			{
				content = new SampleContent("Titel " + i, GetDefaultBitmap(), new Uri(@"c:\temp"));
				NewContent(content);
			}
		}

		private static Bitmap GetDefaultBitmap()
		{
			return new Bitmap(Resource.MapDocumentDefaultImage);
		}
	}
}

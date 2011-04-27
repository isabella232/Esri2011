using System;
using System.Drawing;
using EsriDE.Samples.ContentFinder.BL.Contract;
using EsriDE.Samples.ContentFinder.DomainModel;

namespace EsriDE.Samples.ContentFinder.WpfUI.Tests
{
	/// <summary>
	/// Interaction logic for TestWindow.xaml
	/// </summary>
	public partial class TestWindow : IController
	{
		public TestWindow()
		{
			InitializeComponent();

			Do();
		}

		public void Do()
		{
			this.contentUserControl.Controller = this;

			Content content;
			for (var i = 0; i < 100; i++)
			{
				content = new SampleContent("Titel " + i, GetDefaultBitmap(), new Uri(@"c:\temp"));
				ContentFound(content);
			}
		}

		private static Bitmap GetDefaultBitmap()
		{
			return new Bitmap(Resource.MapDocumentDefaultImage);
		}

		public void Start()
		{
		}

		public void Stop()
		{
		}

		public event Action SearchComplete
		{
			add { }
			remove { }
		}

		public event Action<Content> ContentFound = delegate { };
	}
}

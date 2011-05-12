using System;
using System.Drawing;
using EsriDE.Samples.ContentFinder.AgdBLAdapter;
using EsriDE.Samples.ContentFinder.ContentAdapter.Ags;
using NUnit.Framework;

namespace AgdBLAdapter.Tests
{
	// ReSharper disable InconsistentNaming

	[TestFixture]
	public class ContentProcessorAdapterFixture
	{
		[Test]
		public void Do()
		{
			var agsHandler = new ApplicationAgsCorHandler(null, null);
			var mxdHandler = new ApplicationMxdCorHandler(agsHandler, null);
			var contentProcessorAdapter = new ContentProcessorAdapter(mxdHandler);

			var agsContent = new AgsContent("Ags Demo Content", new Bitmap(1, 1), new Uri("http://nothing.to.do"));

			Assert.That(() => contentProcessorAdapter.Process(agsContent),
				Throws.TypeOf<NullReferenceException>().With.Property("Message").EqualTo("Object reference not set to an instance of an object."));

			//contentProcessorAdapter.Process(agsContent);

			//var container = new WindsorContainer();
			//container.Register(Component.For<ICorHandler<Content>>().ImplementedBy())
		}
	}

	// ReSharper restore InconsistentNaming
}

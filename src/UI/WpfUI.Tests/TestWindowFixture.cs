using System;
using System.Drawing;
using EsriDE.Samples.ContentFinder.DomainModel;
using NUnit.Framework;

//using NUnit.Framework.SyntaxHelpers;

namespace EsriDE.Samples.ContentFinder.WpfUI.Tests
{
	// ReSharper disable InconsistentNaming

	[TestFixture]
	public class TestWindowFixture
	{
		[Test]
		[RequiresSTA]
		//[Timeout(10000)]
		public void Start()
		{
			var f = new TestWindow();
			f.ShowDialog();

			//Content c = new SampleContent("Sample" new Bitmap())
			//f.Do();

			//while (true) ;
		}
	}

	// ReSharper restore InconsistentNaming
}

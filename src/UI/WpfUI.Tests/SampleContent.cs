using System;
using System.Drawing;
using EsriDE.Samples.ContentFinder.DomainModel;

namespace EsriDE.Samples.ContentFinder.WpfUI.Tests
{
	public class SampleContent : Content
	{
		public SampleContent(string title, Bitmap bitmap, Uri uri) : base(title, bitmap, uri)
		{
		}
	}
}
using System;
using System.Drawing;
using EsriDE.Samples.ContentFinder.DomainModel;

namespace EsriDE.Samples.ContentFinder.ContentAdapter.Ags
{
	public class AgsContent : Content
	{
		public AgsContent(string title, Bitmap bitmap, Uri uri) : base(title, bitmap, uri)
		{
		}
	}
}
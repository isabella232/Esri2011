using System;
using System.Drawing;

namespace EsriDE.Samples.ContentFinder.DomainModel
{
	public abstract class Content
	{
		public string Title { get; private set; }
		public Bitmap Bitmap { get; private set; }
		public Uri Uri { get; private set; }

		protected Content(string title, Bitmap bitmap, Uri uri)
		{
			Title = title;
			Bitmap = bitmap;
			Uri = uri;
		}
	}
}
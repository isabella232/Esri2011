using System;
using System.Drawing;

namespace EsriDE.Trials.ContentLoader.DomainModel
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

	public class MxdContent : Content
	{
		public string Subject { get; private set; }
		public string Comments { get; private set; }

		public MxdContent(string title, Bitmap bitmap, Uri uri, string subject, string comments)
			:base(title, bitmap, uri)
		{
			Subject = subject;
			Comments = comments;
		}
	}

	public class AgsContent : Content
	{
		public AgsContent(string title, Bitmap bitmap, Uri uri) : base(title, bitmap, uri)
		{
		}
	}
}
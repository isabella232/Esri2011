using System;
using System.Drawing;
using EsriDE.Samples.ContentFinder.DomainModel;

namespace EsriDE.Samples.ContentFinder.ContentAdapter
{
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
}
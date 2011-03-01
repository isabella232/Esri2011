using System;
using System.Drawing;
using System.IO;
using ESRI.ArcGIS.Carto;
using EsriDE.Samples.ContentFinder.DomainModel;
using stdole;

namespace EsriDE.Samples.ContentFinder.ContentAdapter.Mxd
{
	public class MxdContentLocator : FileContentLocator
	{
		public MxdContentLocator(SourceBundle sourceBundle)
			: base(sourceBundle, GetPredicate())
		{
		}

		private static Predicate<FileInfo> GetPredicate()
		{
			return fileInfo => fileInfo.Extension == ".mxd";
		}

		protected override void Process(Uri uri)
		{
			IMapDocument mapDoc = new MapDocumentClass();
			mapDoc.Open(uri.LocalPath, null);
			try
			{
				var documentInfo = (IDocumentInfo2)mapDoc;

				var title = GetTitle(documentInfo, uri);
				var subject = GetSubject(documentInfo, uri);
				var comments = GetComments(documentInfo, uri);
				var bitmap = GetBitmap(mapDoc);

				var mxdContent = new MxdContent(title, bitmap, uri, subject, comments);

				OnFoundContent(mxdContent);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
			finally
			{
				mapDoc.Close();
			}
		}

		private static string GetTitle(IDocumentInfo2 documentInfo, Uri uri)
		{
			string title = documentInfo.DocumentTitle.Trim();
			if (string.IsNullOrEmpty(title))
			{
				string filename = Path.GetFileName(uri.LocalPath);
				title = filename;
			}
			return title;
		}

		private static string GetSubject(IDocumentInfo2 documentInfo, Uri uri)
		{
			string subject = documentInfo.Subject.Trim();
			if (string.IsNullOrEmpty(subject))
			{
				subject = uri.LocalPath;
			}
			return subject;
		}

		private static string GetComments(IDocumentInfo2 documentInfo, Uri uri)
		{
			string comments = documentInfo.Comments.Trim();
			if (string.IsNullOrEmpty(comments))
			{
				comments = uri.LocalPath;
			}
			return comments;
		}

		private static Bitmap GetBitmap(IMapDocument mapDoc)
		{
			Bitmap bitmap;
			if (!TryGetBitmap(mapDoc, out bitmap))
			{
				bitmap = GetDefaultBitmap();
			}

			return bitmap;
		}

		private static bool TryGetBitmap(IMapDocument mapDoc, out Bitmap bitmap)
		{
			try
			{
				if (null == mapDoc.Thumbnail)
				{
					bitmap = null;
					return false;
				}

				var picture = mapDoc.Thumbnail;
				bitmap = GetBitmap(picture);
				return true;
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception);
				bitmap = null;
				return false;
			}
		}

		private static Bitmap GetBitmap(IPicture picture)
		{
			return BitmapUtil.GetBitmap(picture);
		}

		private static Bitmap GetDefaultBitmap()
		{
			return new Bitmap(Resource.MapDocumentDefaultImage);
		}
	}
}
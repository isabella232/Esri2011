using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EsriDE.Samples.ContentFinder.ContentAdapter
{
	internal class FileUriAnalyzer : UriAnalyzer
	{
		private Predicate<FileInfo> _predicate;

		public FileUriAnalyzer(Predicate<FileInfo> predicate)
		{
			_predicate = predicate;
		}

		protected override IEnumerable<Uri> GetCompositeUris(Uri currentUri)
		{
			DirectoryInfo dirInfo = GetDirInfo(currentUri);
			DirectoryInfo[] folders;

			try
			{
				folders = dirInfo.GetDirectories();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				yield break;
			}

			foreach (DirectoryInfo folder in folders)
			{
				var s = folder.FullName;
				var result = new Uri(s);
				yield return result;
			}

			yield break;
		}

		protected override IEnumerable<Uri> GetLeafUris(Uri currentUri)
		{
			IEnumerable<FileInfo> fileInfos = GetFileInfos(currentUri);

			var uris = from fileInfo in fileInfos where _predicate(fileInfo) select new Uri(fileInfo.FullName);

			foreach (Uri mxdUri in uris)
			{
				yield return mxdUri;
			}
		}

		private static IEnumerable<FileInfo> GetFileInfos(Uri currentUri)
		{
			DirectoryInfo dirInfo = GetDirInfo(currentUri);
			return IoUtils.GetFileInfos(dirInfo);
		}

		private static DirectoryInfo GetDirInfo(Uri currentUri)
		{
			DirectoryInfo dirInfo;
			try
			{
				dirInfo = new DirectoryInfo(currentUri.LocalPath);
			}
			catch
			{
				dirInfo = null;
			}
			return dirInfo;
		}
	}
}
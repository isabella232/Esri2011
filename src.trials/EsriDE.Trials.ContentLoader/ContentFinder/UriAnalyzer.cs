using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EsriDE.Trials.ContentLoader.DomainModel;

namespace EsriDE.Trials.ContentLoader.ContentFinder
{
	public abstract class UriAnalyzer
	{
		public IEnumerable<Uri> GetRecursivUris(IEnumerable<Source> sources)
		{
			foreach (var source in sources)
			{
				IEnumerable<Uri> uris = GetComponentUris(source.Uri, source.RecursivityPolicy);

				foreach (Uri uri in uris)
				{
					yield return uri;
				}
			}
		}

		// Composite Pattern (http://en.wikipedia.org/wiki/Composite_pattern)
		protected IEnumerable<Uri> GetComponentUris(Uri currentUri, RecursivityPolicy policy)
		{
			var leafUris = GetLeafUris(currentUri);
			foreach (var leafUri in leafUris)
			{
				yield return leafUri;
			}

			if (RecursivityPolicy.Recursiv == policy)
			{
				var compositeUris = GetCompositeUris(currentUri);
				foreach (var compositeUri in compositeUris)
				{
					yield return compositeUri;
				}
			}
		}

		protected abstract IEnumerable<Uri> GetCompositeUris(Uri currentUri);

		protected abstract IEnumerable<Uri> GetLeafUris(Uri currentUri);
	}

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
				IEnumerable<Uri> folderUris = GetComponentUris(new Uri(folder.FullName), RecursivityPolicy.Recursiv);
				foreach (Uri folderUri in folderUris)
				{
					yield return folderUri;
				}
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
			return FileUtils.GetFileInfos(dirInfo);
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
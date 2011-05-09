using System.Collections.Generic;
using System.IO;

namespace EsriDE.Samples.ContentFinder.ContentAdapter
{
	public static class IoUtils
	{
		public static IEnumerable<FileInfo> GetFileInfos(DirectoryInfo dirInfo)
		{
			if (null == dirInfo)
			{
				yield break;
			}

			FileInfo[] fileInfos;
			try
			{
				fileInfos = dirInfo.GetFiles();
			}
			catch
			{
				yield break;
			}

			foreach (FileInfo fileInfo in fileInfos)
			{
				yield return fileInfo;
			}
		}
	}
}
using System;
using System.Collections.Generic;
using EsriDE.Commons.Ags;

namespace EsriDE.Samples.ContentFinder.ContentAdapter.Ags
{
	public sealed class AgsUriAnalyzer : UriAnalyzer
	{
		protected override IEnumerable<Uri> GetCompositeUris(Uri currentUri)
		{
			var list = AgsUtil.GetFolderUris(currentUri);
			foreach (var uri in list)
			{
				yield return uri;
			}
		}

		protected override IEnumerable<Uri> GetLeafUris(Uri currentUri)
		{
			var list = AgsUtil.GetServiceUris(currentUri);
			foreach (var uri in list)
			{
				yield return uri;
			}
		}
	}
}
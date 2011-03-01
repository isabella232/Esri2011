using System;
using System.Collections.Generic;
using EsriDE.Samples.ContentFinder.DomainModel;

namespace EsriDE.Samples.ContentFinder.ContentAdapter
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
}
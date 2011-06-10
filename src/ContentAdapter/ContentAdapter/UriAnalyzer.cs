using System;
using System.Collections.Generic;
using System.Diagnostics;
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

				foreach (var uri in uris)
				{
					yield return uri;
				}
			}
		}

		// similar to Composite Pattern (http://en.wikipedia.org/wiki/Composite_pattern)
		// Template Method (http://en.wikipedia.org/wiki/Template_method_pattern)
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
					var childs = GetComponentUris(compositeUri, policy);
					foreach (var child in childs)
					{
						yield return child;
					}
				}
			}
		}

		protected abstract IEnumerable<Uri> GetCompositeUris(Uri currentUri);

		protected abstract IEnumerable<Uri> GetLeafUris(Uri currentUri);
	}
}
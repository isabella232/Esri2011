using System;
using System.Collections.Generic;
using EsriDE.Commons.Ags;

namespace EsriDE.Samples.ContentFinder.ContentAdapter.Ags
{
	public class MapAgsUriAnalyzer : AgsUriAnalyzer
	{
		protected override IEnumerable<Uri> GetLeafUris(Uri currentUri)
		{
			var leafs = base.GetLeafUris(currentUri);

			foreach (var leaf in leafs)
			{
				ServiceType type = ServiceType.Unknown;
				try
				{
					type = AgsUtil.GetServiceType(leaf);
				}
				catch (Exception)
				{
				}

				if (ServiceType.MapService == type)
				{
					yield return leaf;
				}
			}
		}
	}
}
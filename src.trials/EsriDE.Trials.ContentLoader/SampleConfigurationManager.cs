using System;
using System.Collections.Generic;
using EsriDE.Trials.ContentLoader.DomainModel;

namespace EsriDE.Trials.ContentLoader
{
	public class SampleConfigurationManager : IConfigurationManager
	{
		public IEnumerable<SourceBundle> GetContentsToSearch()
		{
			yield return
				new SourceBundle("mxd",
				            new List<Source>
				            	{
				            		new Source(new Uri(@"c:\temp\"), RecursivityPolicy.Recursiv),
				            		new Source(new Uri(@"c:\users\"), RecursivityPolicy.NotRecursiv)
				            	});
			yield return
				new SourceBundle("mxd",
				            new List<Source>
				            	{
				            		new Source(new Uri(@"z:\"), RecursivityPolicy.NotRecursiv),
				            	});
			yield return
				new SourceBundle("ags",
							new List<Source>
				            	{
				            		new Source(new Uri(@"http://server.arcgisonline.com/ArcGIS/rest/services"), RecursivityPolicy.Recursiv),
				            		new Source(new Uri(@"http://server.arcgisonline.com/ArcGIS/soap/services"), RecursivityPolicy.NotRecursiv)
				            	});
			yield break;
		}
	}
}
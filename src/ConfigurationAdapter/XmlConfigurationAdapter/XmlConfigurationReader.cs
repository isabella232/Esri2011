using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using EsriDE.Samples.ContentFinder.ConfigurationAdapter.Contract;
using EsriDE.Samples.ContentFinder.DomainModel;

namespace EsriDE.Samples.ContentFinder.XmlConfigurationAdapter
{
	public class XmlConfigurationReader : IConfigurationReader
	{
		public IEnumerable<SourceBundle> ReadConfiguration(Uri uri)
		{
			if (null != uri)
			{
				throw new ApplicationException("Lesen mit individueller Uri ist noch nicht implementiert.");
			}

			XDocument locationsConfig = XDocument.Load(@"Locations.config");

			var r = from c in locationsConfig.Descendants("ContentToSearch")
					select
						new SourceBundle(c.Attribute("type").Value,
										 from d in c.Descendants("Source")
										 select new Source(new Uri(d.Attribute("Uri").Value),
														   (Convert.ToBoolean(d.Attribute("IsRecursive").Value)
																? RecursivityPolicy.Recursiv
																: RecursivityPolicy.NotRecursiv)));

			return r;
		}
	}
}
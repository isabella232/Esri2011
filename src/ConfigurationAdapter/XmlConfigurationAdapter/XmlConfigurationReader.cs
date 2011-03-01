using System;
using System.Collections.Generic;
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
				//throw new NotImplementedException("Lesen mit individueller Uri ist noch nicht implementiert.");
				throw new ApplicationException("Lesen mit individueller Uri ist noch nicht implementiert.");
			}

			yield break;
		}
	}
}
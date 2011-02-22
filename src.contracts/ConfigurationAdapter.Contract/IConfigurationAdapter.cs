using System.Collections.Generic;
using EsriDE.Samples.ContentFinder.DomainModel;

namespace EsriDE.Samples.ContentFinder.ConfigurationAdapter.Contract
{
	public interface IConfigurationAdapter
	{
		IEnumerable<SourceBundle> Read();
	}
}
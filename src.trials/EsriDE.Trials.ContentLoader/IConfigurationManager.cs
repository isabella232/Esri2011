using System.Collections.Generic;
using EsriDE.Trials.ContentLoader.DomainModel;

namespace EsriDE.Trials.ContentLoader
{
	public interface IConfigurationManager
	{
		IEnumerable<SourceBundle> GetContentsToSearch();
	}
}
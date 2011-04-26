using EsriDE.Samples.ContentFinder.DomainModel;

namespace EsriDE.Samples.ContentFinder.ApplicationAdapter.Contract
{
	public interface IContentProcessorAdapter
	{
		void Process(Content content);
	}
}
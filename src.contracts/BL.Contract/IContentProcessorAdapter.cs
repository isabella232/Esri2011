using EsriDE.Samples.ContentFinder.DomainModel;

namespace EsriDE.Samples.ContentFinder.BL.Contract
{
	public interface IContentProcessorAdapter
	{
		void Process(Content content);
	}
}
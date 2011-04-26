using EsriDE.Samples.ContentFinder.DomainModel;

namespace EsriDE.Samples.ContentFinder.AgdAdapter
{
	public interface IContentProcessor
	{
		void Process(Content content);
	}
}
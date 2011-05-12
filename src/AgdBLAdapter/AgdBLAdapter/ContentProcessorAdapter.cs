using EsriDE.Commons.Patterns;
using EsriDE.Samples.ContentFinder.ApplicationAdapter.Contract;
using EsriDE.Samples.ContentFinder.DomainModel;

namespace EsriDE.Samples.ContentFinder.AgdBLAdapter
{
	public class ContentProcessorAdapter : IContentProcessorAdapter
	{
		private readonly IChainOfResponsibilityHandler<Content> _corHandler;

		public ContentProcessorAdapter(IChainOfResponsibilityHandler<Content> corHandler)
		{
			_corHandler = corHandler;
		}

		public void Process(Content content)
		{
			_corHandler.Process(content);
		}
	}
}
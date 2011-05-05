using EsriDE.Samples.ContentFinder.ApplicationAdapter.Contract;
using EsriDE.Samples.ContentFinder.DomainModel;

namespace AgdBLAdapter.Tests
{
	public class ContentProcessorAdapter: IContentProcessorAdapter
	{
		private readonly ICorHandler<Content> _corHandler;

		public ContentProcessorAdapter(ICorHandler<Content> corHandler)
		{
			_corHandler = corHandler;
		}

		public void Process(Content content)
		{
			_corHandler.Process(content);
		}
	}
}
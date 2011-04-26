using EsriDE.Samples.ContentFinder.DomainModel;

namespace EsriDE.Samples.ContentFinder.AgdAdapter
{
	public abstract class CorBasedContentProcessor : IContentProcessor
	{
		private readonly CorBasedContentProcessor _nextProcessor;

		protected CorBasedContentProcessor(CorBasedContentProcessor nextProcessor)
		{
			_nextProcessor = nextProcessor;
		}

		protected CorBasedContentProcessor NextProcessor
		{
			get { return _nextProcessor; }
		}

		public void Process(Content content)
		{
			if (IsResponsibleFor(content))
			{
				ProcessCore(content);
			}
			else
			{
				_nextProcessor.Process(content);
			}
		}

		protected abstract bool IsResponsibleFor(Content content);
		protected abstract void ProcessCore(Content content);
	}
}
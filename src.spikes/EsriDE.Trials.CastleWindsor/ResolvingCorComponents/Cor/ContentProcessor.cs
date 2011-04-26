namespace EsriDE.Trials.CastleWindsor.ResolvingCorComponents.Cor
{
	public abstract class ContentProcessor : IContentProcessor
	{
		private readonly ContentProcessor _nextProcessor;

		protected ContentProcessor(ContentProcessor nextProcessor)
		{
			_nextProcessor = nextProcessor;
		}

		protected ContentProcessor NextProcessor
		{
			get { return _nextProcessor; }
		}

		public void Process(string s)
		{
			if (IsResponsibleFor(s))
			{
				ProcessCore(s);
			}
			else
			{
				_nextProcessor.Process(s);
			}
		}

		protected abstract bool IsResponsibleFor(string s);
		protected abstract void ProcessCore(string s);
	}
}
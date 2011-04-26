namespace EsriDE.Trials.CastleWindsor.ResolvingCorComponents.Cor
{
	public abstract class AgdContentProcessor : ContentProcessor
	{
		private readonly IApplication _application;
		private readonly string _contentType;

		protected AgdContentProcessor(ContentProcessor nextProcessor, string contentType, IApplication application)
			: base(nextProcessor)
		{
			_application = application;
			_contentType = contentType;
		}

		protected IApplication Application
		{
			get { return _application; }
		}

		protected override bool IsResponsibleFor(string contentType)
		{
			var result = 0 == string.Compare(contentType, _contentType);
			return result;
		}
	}
}
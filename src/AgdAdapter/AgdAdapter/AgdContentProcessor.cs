using System;
using ESRI.ArcGIS.Framework;
using EsriDE.Samples.ContentFinder.DomainModel;

namespace EsriDE.Samples.ContentFinder.AgdAdapter
{
	public abstract class AgdContentProcessor : CorBasedContentProcessor
	{
		private readonly IApplication _application;
		private readonly Type _contentType;

		protected AgdContentProcessor(AgdContentProcessor nextProcessor, Type contentType, IApplication application)
			: base(nextProcessor)
		{
			_application = application;
			_contentType = contentType;
		}

		protected IApplication Application
		{
			get { return _application; }
		}

		protected override bool IsResponsibleFor(Content content)
		{
			var type = content.GetType();
			var result = type == _contentType;
			return result;
		}
	}
}
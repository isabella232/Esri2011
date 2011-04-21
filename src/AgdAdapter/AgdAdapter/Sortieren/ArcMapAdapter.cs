using System;
using ESRI.ArcGIS.Framework;
using EsriDE.Samples.ContentFinder.DomainModel;

namespace EsriDE.Samples.ContentFinder.AgdAdapter
{
	public abstract class ArcMapAdapter : IArcMapAdapter
	{
		private readonly IApplication _application;
		private readonly Type _contentType;

		protected ArcMapAdapter(Type contentType, IApplication application)
		{
			_application = application;
			_contentType = contentType;
		}

		protected IApplication Application
		{
			get { return _application; }
		}

		public void Process(Content content)
		{
			if (IsResponsibleFor(content))
			{
				ProcessCore(content);
			}
		}

		protected virtual bool IsResponsibleFor(Content content)
		{
			var type = content.GetType();
			var result = type == _contentType;
			return result;
		}

		protected abstract void ProcessCore(Content content);
	}
}
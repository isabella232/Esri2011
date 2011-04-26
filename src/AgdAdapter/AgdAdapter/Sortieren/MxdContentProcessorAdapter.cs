using ESRI.ArcGIS.Framework;
using EsriDE.Samples.ContentFinder.DomainModel;

namespace EsriDE.Samples.ContentFinder.AgdAdapter.Sortieren
{
	public class MxdContentProcessorAdapter : AgdContentProcessor
	{
		public MxdContentProcessorAdapter(AgdContentProcessor nextProcessor, IApplication application)
			: base(nextProcessor, typeof(MxdContentProcessorAdapter), application)
		{
		}

		protected override void ProcessCore(Content content)
		{
			//Application.OpenDocument(content.Uri.LocalPath);
		}
	}
}
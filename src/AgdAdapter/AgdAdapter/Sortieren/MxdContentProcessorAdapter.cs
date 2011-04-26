using ESRI.ArcGIS.Framework;
using EsriDE.Samples.ContentFinder.DomainModel;

namespace EsriDE.Samples.ContentFinder.AgdAdapter.Sortieren
{
	public class MxdContentProcessorAdapter : ContentProcessorAdapter
	{
		public MxdContentProcessorAdapter(IApplication application) : base(typeof(MxdContentProcessorAdapter), application)
		{
		}

		protected override void ProcessCore(Content content)
		{
			Application.OpenDocument(content.Uri.LocalPath);
		}
	}
}
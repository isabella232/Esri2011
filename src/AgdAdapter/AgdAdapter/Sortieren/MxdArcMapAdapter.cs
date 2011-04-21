using ESRI.ArcGIS.Framework;
using EsriDE.Samples.ContentFinder.DomainModel;

namespace EsriDE.Samples.ContentFinder.AgdAdapter
{
	public class MxdArcMapAdapter : ArcMapAdapter
	{
		public MxdArcMapAdapter(IApplication application) : base(typeof(MxdArcMapAdapter), application)
		{
		}

		protected override void ProcessCore(Content content)
		{
			Application.OpenDocument(content.Uri.LocalPath);
		}
	}
}
using EsriDE.Samples.ContentFinder.ApplicationAdapter.Contract;
using IApplication = ESRI.ArcGIS.Framework.IApplication;

namespace EsriDE.Samples.ContentFinder.AgdAdapter
{
	public class ArcMapAddinAdapter : IApplicationAdapter
	{
		public IApplication Application
		{
			get { return ArcMap.Application; }
		}
	}
}
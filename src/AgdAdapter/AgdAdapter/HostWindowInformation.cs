using EsriDE.Samples.ContentFinder.UI.Contract;

namespace EsriDE.Samples.ContentFinder.AgdAdapter
{
	public class HostWindowInformation : IWindowInformation
	{
		public int WindowHandle
		{
			get { return ArcMap.Application.hWnd; }
		}
	}
}
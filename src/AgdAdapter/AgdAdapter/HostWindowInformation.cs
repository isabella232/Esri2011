using EsriDE.Samples.ContentFinder.UI.Contract;

namespace EsriDE.Samples.ContentFinder.AgdAdapter
{
	public class HostWindowInformation : IWindowInformation
	{
		public int WindowHandle { get; set; }

		public HostWindowInformation()
		{
			WindowHandle = ArcMap.Application.hWnd;
		}
	}
}
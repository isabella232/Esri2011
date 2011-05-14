using System;
using System.Diagnostics;
using EsriDE.Samples.ContentFinder.UI.Contract;

namespace EsriDE.Samples.ContentFinder.AgdAdapter
{
	public class HostWindowInformation : IWindowInformation
	{
		public int WindowHandle { get; set; }

		public HostWindowInformation()
		{
			try
			{
				WindowHandle = ArcMap.Application.hWnd;
			}
			catch (NullReferenceException e)
			{
				// Wenn Unit-Test läuft & kein Addin als SUT-Treiber diente
				Debug.WriteLine(e);
			}
		}
	}
}
using System;
using System.Diagnostics;

namespace EsriDE.Samples.ContentFinder.AgdAdapter
{
	public class ContentFinderButton : ShifterAddinButton
	{
		public ContentFinderButton()
		{
			try
			{
				new Builder(this);
			}
			catch (Exception e)
			{
				Trace.WriteLine(e);
			}
		}
	}
}
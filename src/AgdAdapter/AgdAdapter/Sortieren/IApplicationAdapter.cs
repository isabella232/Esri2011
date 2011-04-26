using System;
using ESRI.ArcGIS.Framework;
namespace EsriDE.Samples.ContentFinder.AgdAdapter
{
	public interface IApplicationAdapter
	{
		IApplication Application { get; }
	}

	public class ArcMapAddinAdapter : IApplicationAdapter
	{
		public IApplication Application
		{
			get { throw new NotImplementedException(); }
		}
	}
}
using System;
using System.Diagnostics;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.GISClient;
using EsriDE.Samples.ContentFinder.DomainModel;

namespace EsriDE.Samples.ContentFinder.AgdAdapter.Sortieren
{
	public class AgsContentProcessorAdapter : AgdContentProcessor
	{
		public AgsContentProcessorAdapter(AgdContentProcessor nextProcessor, IApplication application)
			: base(nextProcessor, typeof(AgsContentProcessorAdapter), application)
		{
		}

		protected override void ProcessCore(Content content)
		{
			var uri = content.Uri;
			string[] segments = uri.Segments;
			string serverType = segments[segments.Length - 1].ToLower();
			serverType = serverType.Replace("/", string.Empty);

			var document = (IMxDocument)Application.Document;
			IMap focusMap = document.FocusMap;
			ILayer layer = null;

			if (serverType == "mapserver")
			{
				string serviceName = GetServiceName(content);
				layer = GetLayerfromMapService(uri, serviceName);
			}
			if (serverType == "imageserver")
			{
				layer = GetLayerfromImageService(uri);
			}

			if (layer != null)
			{
				focusMap.AddLayer(layer);
			}
		}

		private string GetServiceName(Content content)
		{
			throw new NotImplementedException();
		}

		public ILayer GetLayerfromImageService(Uri uri)
		{
			ImageServerLayer layer;
			try
			{
				layer = new ImageServerLayerClass();
				layer.Initialize(uri.AbsoluteUri);
				return layer;
			}
			catch
			{
				return null;
			}
		}

		public ILayer GetLayerfromMapService(Uri uri, string serviceName)
		{
			try
			{
				var connectionFactory =
					(IAGSServerConnectionFactory2)new AGSServerConnectionFactory();
				//create a property set to hold connection properties
				IPropertySet connectionProps = new PropertySet();
				//specify the URL for the server
				connectionProps.SetProperty("URL", uri.AbsoluteUri);
				//define username and password for the connection
				//connectionProps.SetProperty("USER", "<USER>");
				//connectionProps.SetProperty("PASSWORD", "<PASS>");
				//open the server connection, pass in the property set, get a connection object back
				IAGSServerConnection gisServer = connectionFactory.Open(connectionProps, Application.hWnd);

				//get an enum of all server object names from the server (GIS services, i.e.)
				IAGSEnumServerObjectName agsEnumServerObjectName = gisServer.ServerObjectNames;
				//loop thru all services, find a map service called "I3_Imagery_Prime_World_2D" (high res imagery for the world)
				var agsServerObjectName3 = (IAGSServerObjectName3)agsEnumServerObjectName.Next();
				do
				{
					if ((agsServerObjectName3.Type == "MapServer") && (agsServerObjectName3.Name == serviceName))
					{
						break; //found it
					}
					//keep searching the services ...
					agsServerObjectName3 = (IAGSServerObjectName3)agsEnumServerObjectName.Next();
				}
				while (agsServerObjectName3 != null);
				
				//if the desired service was found ...
				if (agsServerObjectName3 != null)
				{
					//create a layer factory to make a new MapServerLayer from the server object name
					ILayerFactory layerFactory = new MapServerLayerFactory();

					//create an enum of layers using the name object (will contain only a single layer)
					IEnumLayer enumLyrs = layerFactory.Create(agsServerObjectName3);
					//get the layer from the enum, store it in a MapServerLayer variable
					var mapServerLayer = (IMapServerLayer)enumLyrs.Next();
					//make sure the layer is not empty (Nothing), then add it to the map
					if (mapServerLayer != null)
					{
						return (ILayer)mapServerLayer;
					}
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
			return null;
		}
	}
}
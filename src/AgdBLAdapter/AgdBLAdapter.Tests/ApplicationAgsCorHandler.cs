using System;
using System.Diagnostics;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.GISClient;
using EsriDE.Samples.ContentFinder.ApplicationAdapter.Contract;
using EsriDE.Samples.ContentFinder.ContentAdapter.Ags;
using EsriDE.Samples.ContentFinder.DomainModel;

namespace AgdBLAdapter.Tests
{
	public class ApplicationAgsCorHandler : ApplicationCorHandler
	{
		public ApplicationAgsCorHandler(ApplicationCorHandler nextLink, IApplicationAdapter applicationAdapter)
			: base(nextLink, applicationAdapter)
		{
		}

		protected override bool IsResponsible(Content data)
		{
			var result = typeof(AgsContent) == data.GetType();
			return result;
		}

		protected override void ProcessCore(Content data)
		{
			var application = ApplicationAdapter.Application;

			var serviceUri = GetServiceUri(data.Uri);
			var serviceName = GetServiceName(data.Uri);

			string[] segments = serviceUri.Segments;
			string serverType = segments[segments.Length - 1].ToLower();
			serverType = serverType.Replace("/", string.Empty);

			var mxDocument = (IMxDocument)application.Document;
			IMap focusMap = mxDocument.FocusMap;
			ILayer layer = null;

			if (serverType == "mapserver")
			{
				layer = GetLayerfromMapService(serviceUri, serviceName);
			}
			if (serverType == "imageserver")
			{
				layer = GetLayerfromImageService(serviceUri);
			}

			if (layer != null)
			{
				focusMap.AddLayer(layer);
			}
		}

		private Uri GetServiceUri(Uri uri)
		{
			var strings = GetSplittedUri(uri);
			var serviceUri = strings[0];
			var result = new Uri(serviceUri);
			return result;
		}

		private string GetServiceName(Uri uri)
		{
			var strings = GetSplittedUri(uri);
			var result = strings[1];
			return result;
		}

		private string[] GetSplittedUri(Uri uri)
		{
			string s = uri.LocalPath;
			var strings = s.Split('?');
			if (strings.Length != 2)
			{
				throw new ArgumentException("URI kann nicht in Service-URI und Service-Name zerlegt werden.");
			}
			return strings;
		}

		public ILayer GetLayerfromImageService(Uri uri)
		{
			ImageServerLayer imLayer;
			try
			{
				imLayer = new ImageServerLayerClass();
				imLayer.Initialize(uri.AbsoluteUri);
				return imLayer;
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
				IAGSServerConnection gisServer = connectionFactory.Open(connectionProps, _application.hWnd);

				//get an enum of all server object names from the server (GIS services, i.e.)
				IAGSEnumServerObjectName soNames = gisServer.ServerObjectNames;
				//loop thru all services, find a map service called "I3_Imagery_Prime_World_2D" (high res imagery for the world)
				var soName = (IAGSServerObjectName3)soNames.Next();
				do
				{
					if ((soName.Type == "MapServer") && (soName.Name == serviceName))
					{
						break; //found it
					}
					//keep searching the services ...
					soName = (IAGSServerObjectName3)soNames.Next();
				} while (soName != null);
				//if the desired service was found ...
				if (soName != null)
				{
					//create a layer factory to make a new MapServerLayer from the server object name
					ILayerFactory msLayerFactory = new MapServerLayerFactory();

					//create an enum of layers using the name object (will contain only a single layer)
					IEnumLayer enumLyrs = msLayerFactory.Create(soName);
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
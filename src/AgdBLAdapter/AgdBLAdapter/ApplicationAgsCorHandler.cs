using System;
using System.Diagnostics;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.GISClient;
using EsriDE.Commons.Ags;
using EsriDE.Samples.ContentFinder.ApplicationAdapter.Contract;
using EsriDE.Samples.ContentFinder.ContentAdapter.Ags;
using EsriDE.Samples.ContentFinder.DomainModel;

namespace EsriDE.Samples.ContentFinder.AgdBLAdapter
{
	public class ApplicationAgsCorHandler : ApplicationCorHandler
	{
		public ApplicationAgsCorHandler(ApplicationCorHandler successor, IApplicationAdapter applicationAdapter)
			: base(successor, applicationAdapter)
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

			var serviceType = AgsUtil.GetServiceType(data.Uri);
			var serviceName = AgsUtil.GetServiceName(data.Uri.AbsoluteUri);

			ILayer layer = null;
			switch (serviceType)
			{
				case ServiceType.MapService:
					layer = GetLayerFromMapService(data.Uri, serviceName);
					break;
				case ServiceType.ImageService:
					layer = GetLayerFromImageService(data.Uri);
					break;
				case ServiceType.Unknown:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			if (layer != null)
			{
				var mxDocument = (IMxDocument)application.Document;
				IMap focusMap = mxDocument.FocusMap;
				focusMap.AddLayer(layer);
			}
		}

		public ILayer GetLayerFromImageService(Uri uri)
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


		public ILayer GetLayerFromMapService(Uri uri, string serviceName)
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
				var handle = ApplicationAdapter.Application.hWnd;
				IAGSServerConnection gisServer = connectionFactory.Open(connectionProps, handle);

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

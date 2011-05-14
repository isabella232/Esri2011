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

		internal ILayer GetLayerFromImageService(Uri uri)
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

		internal static ILayer GetLayerFromMapService(Uri uri, string serviceName)
		{
			try
			{
				IAGSServerConnection gisServerConnection = GetGisServerConnection(uri);

				IAGSEnumServerObjectName serverObjectNames = gisServerConnection.ServerObjectNames;

				IAGSServerObjectName3 objectName;
				while (null != (objectName = (IAGSServerObjectName3) serverObjectNames.Next()))
				{
					var correctType = 0 == string.Compare("MapServer", objectName.Type, StringComparison.InvariantCultureIgnoreCase);
					var correctName = 0 == string.Compare(serviceName, objectName.Name, StringComparison.InvariantCultureIgnoreCase);

					if (correctType && correctName)
					{
						var layer = GetLayer(objectName);
						return layer;
					}
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}

			return null;
		}

		private static ILayer GetLayer(IAGSServerObjectName3 objectName)
		{
			ILayerFactory msLayerFactory = new MapServerLayerFactory();
			IEnumLayer enumLyrs = msLayerFactory.Create(objectName);
			var mapServerLayer = (IMapServerLayer)enumLyrs.Next();
			if (mapServerLayer != null)
			{
				return (ILayer)mapServerLayer;
			}
			return null;
		}

		private static IAGSServerConnection GetGisServerConnection(Uri uri)
		{
			var soapUri = AgsUtil.GetSoapRepresentation(uri);

			var connectionFactory = (IAGSServerConnectionFactory2) new AGSServerConnectionFactory();
			IPropertySet connectionProps = new PropertySet();
			connectionProps.SetProperty("URL", soapUri.AbsoluteUri);

			return connectionFactory.Open(connectionProps, 0);
		}
	}
}

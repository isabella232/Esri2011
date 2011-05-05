using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using EsriDE.Samples.AgsServicesJsonDeSerializer;
using EsriDE.Samples.ContentLoader.Adapter.Contract;
using EsriDE.Samples.ContentLoader.DomainModel;

namespace EsriDE.Samples.ContentLoader.Adapter.Basemap
{
    public partial class BasemapReaderAdapter : IBasemapReaderAdapter
    {
        private volatile bool _cancelReading;
        private volatile IDictionary<Guid, bool> _imageReadingProcessingStatus;
        private volatile IDictionary<Thread, bool> _imageReadingThreads;

        #region IBasemapReaderAdapter Members

        public void Read(IEnumerable<BasemapConfigItem> configItems)
        {
            _imageReadingProcessingStatus = new Dictionary<Guid, bool>();
            _imageReadingThreads = new Dictionary<Thread, bool>();
            _cancelReading = false;

            var readingThread = new Thread(ReadBasemaps);
            readingThread.Priority = ThreadPriority.Lowest;
            //readingThread.SetApartmentState(ApartmentState.STA);
            readingThread.Start(configItems);

            // synchrone Aufrufvariante
            //ReadBasemaps(configItems);
        }

        public void CancelReading()
        {
            _cancelReading = true;
        }

        public event Action<IMetaData> DataReaded = delegate { };
        public event Action<IImageData> ImageReaded = delegate { };
        public event Action ReadingCompleted = delegate { };

        #endregion

        protected virtual void OnDataReaded(IMetaData metaData)
        {
            DataReaded(metaData);
        }

        protected virtual void OnImageReaded(IImageData imageData)
        {
            ImageReaded(imageData);
        }

        [LogAspect]
        protected virtual void OnReadingCompleted()
        {
            ReadingCompleted();
        }

        [LogAspect]
        private void ReadBasemaps(object o)
        {
            var configItems = (IEnumerable<BasemapConfigItem>) o;

            //var allUris = GetRecursivUris(configItems);

            //AnalyseUris(allUris);
            AnalyseBasemapConfigItems(configItems);
            WaitForCompleteProcessing();

            OnReadingCompleted();
        }

        private static IEnumerable<Uri> GetRecursivUris(IEnumerable<BasemapConfigItem> configItems)
        {
            return configItems.Select(basemapConfigItem => basemapConfigItem.Uri);
        }

        private void AnalyseBasemapConfigItems(IEnumerable<BasemapConfigItem> basemapConfigItems)
        {
            foreach (BasemapConfigItem basemapConfigItem in basemapConfigItems)
            {
                if (_cancelReading)
                {
                    return;
                }

                AnalyseBasemapConfigItem(basemapConfigItem);
            }
        }

        private void AnalyseBasemapConfigItem(BasemapConfigItem basemapConfigItem)
        {
            try
            {
                Uri restServiceUrl = basemapConfigItem.Uri;
                var directory = new AgsServicesDirectory(restServiceUrl);

                string[] segments = restServiceUrl.Segments;
                string serverType = segments[segments.Length - 1].ToLower();
                serverType = serverType.Replace("/", string.Empty);

                if (serverType == "mapserver")
                {
                    ProcessMapService(restServiceUrl, directory);
                }
                else if (serverType == "imageserver")
                {
                    ProcessImageService(restServiceUrl, directory);
                }
                else
                {
                    ProcessMapServiceDirectory(directory, basemapConfigItem.IsRecursive);
                    ProcessImageServiceDirectory(directory, basemapConfigItem.IsRecursive);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }


        private void AnalyseUris(IEnumerable<Uri> allUris)
        {
            foreach (Uri uri in allUris)
            {
                if (_cancelReading)
                {
                    return;
                }

                AnalyseUri(uri);
            }
        }

        private void AnalyseUri(Uri uri)
        {
            try
            {
                Uri restServiceUrl = uri;
                var directory = new AgsServicesDirectory(restServiceUrl);

                if (restServiceUrl.AbsoluteUri.ToLower().Contains("mapserver"))
                {
                    ProcessMapService(restServiceUrl, directory);
                }
                else if (restServiceUrl.AbsoluteUri.ToLower().Contains("imageserver"))
                {
                    ProcessImageService(restServiceUrl, directory);
                }
                else
                {
                    ProcessMapServiceDirectory(directory, true);
                    ProcessImageServiceDirectory(directory, true);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        private void ProcessImageService(Uri restServiceUrl, AgsServicesDirectory directory)
        {
            ImageServiceInfo imageServiceInfo = AgsImageServiceInfo.GetImageServiceInfo(directory, restServiceUrl);
            if (null != imageServiceInfo)
            {
                ProcessImageServiceInfo(imageServiceInfo);
            }
        }

        private void ProcessImageServiceDirectory(AgsServicesDirectory directory, bool checkSubDirectory)
        {
            foreach (Service service in directory.GetImageServices(checkSubDirectory))
            {
                if (_cancelReading)
                {
                    return;
                }

                ImageServiceInfo imageServiceInfo = AgsImageServiceInfo.GetImageServiceInfo(directory,
                                                                                            service.RESTServiceUrl);
                ProcessImageServiceInfo(imageServiceInfo);
            }
        }

        private void ProcessImageServiceInfo(ImageServiceInfo imageServiceInfo)
        {
            if (null == imageServiceInfo.Service)
            {
                return;
            }

            Guid guid = Guid.NewGuid();

            IMetaData metaData = CreateMetaData(imageServiceInfo, guid);
            OnDataReaded(metaData);

            ReadImageAsync(imageServiceInfo, guid);
        }

        private void ProcessMapService(Uri restServiceUrl, AgsServicesDirectory directory)
        {
            MapServiceInfo mapServiceInfo = AgsMapServiceInfo.GetMapServiceInfo(directory, restServiceUrl);
            if (null != mapServiceInfo)
            {
                ProcessMapServiceInfo(mapServiceInfo);
            }
        }

        private void ProcessMapServiceDirectory(AgsServicesDirectory directory, bool checkSubDirectory)
        {
            foreach (Service service in directory.GetMapServices(checkSubDirectory))
            {
                if (_cancelReading)
                {
                    return;
                }

                Uri serviceUri = service.RESTServiceUrl;
                ProcessMapService(serviceUri, directory);
            }
        }

        private void ProcessMapServiceInfo(MapServiceInfo mapServiceInfo)
        {
            if (null == mapServiceInfo.Service)
            {
                return;
            }

            Guid guid = Guid.NewGuid();
            IMetaData metaData = CreateMetaData(mapServiceInfo, guid);
            OnDataReaded(metaData);

            try
            {
                ReadImageAsync(mapServiceInfo, guid);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void ReadImageAsync(object serviceInfo, Guid guid)
        {
            try
            {
                var imageInfoDto = new ImageInfoDto(guid, serviceInfo);
                _imageReadingProcessingStatus.Add(guid, true);

                //ThreadPool.QueueUserWorkItem(GetImageFromServiceInfo, imageInfoDto);
                var thread = new Thread(GetImageFromServiceInfo)
                                 {
                                     Priority = ThreadPriority.Lowest
                                 };
                _imageReadingThreads.Add(thread, true);
                Debug.WriteLine("======================================================== Thread#: " +
                                _imageReadingThreads.Count);
                thread.Start(imageInfoDto);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void GetImageFromServiceInfo(object o)
        {
            var imageInfoDto = (ImageInfoDto) o;
            object serviceInfo = imageInfoDto.ServiceInfo;
            try
            {
                Bitmap bitmap = null;
                if (serviceInfo.GetType() == typeof (MapServiceInfo))
                {
                    var mapServiceInfo = (MapServiceInfo) serviceInfo;
                    bitmap = AgsMapServiceInfo.
                        GetSysDrawBitmap(mapServiceInfo.Service.RESTServiceUrl, mapServiceInfo.InitialExtent, @"400,266");
                }
                if (serviceInfo.GetType() == typeof (ImageServiceInfo))
                {
                    var imageServiceInfo = (ImageServiceInfo) serviceInfo;
                    bitmap = AgsImageServiceInfo.
                        GetSysDrawBitmap(imageServiceInfo.Service.RESTServiceUrl, imageServiceInfo.Extent, @"400,266");
                }

                var imageData = new ImageData(imageInfoDto.Id, bitmap);

                OnImageReaded(imageData);

                _imageReadingThreads[Thread.CurrentThread] = false;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e + ", Image=" + imageInfoDto.ServiceInfo);
            }

            SetProcessingStatus(imageInfoDto);
        }

        private void SetProcessingStatus(ImageInfoDto imageInfoDto)
        {
            if (_imageReadingProcessingStatus.ContainsKey(imageInfoDto.Id))
            {
                _imageReadingProcessingStatus[imageInfoDto.Id] = false;
            }
        }

        private void WaitForCompleteProcessing()
        {
            //while (_imageReadingProcessingStatus.Values.Contains(true))
            //{
            //    Thread.Sleep(1000);
            //}

            while (_imageReadingThreads.Values.Contains(true))
            {
                Thread.Sleep(1000);
            }
        }

        #region Nested type: ImageInfoDto

        private struct ImageInfoDto
        {
            internal ImageInfoDto(Guid id, object serviceInfo)
                : this()
            {
                Id = id;
                ServiceInfo = serviceInfo;
            }

            internal Guid Id { get; private set; }
            internal object ServiceInfo { get; private set; }
        }

        #endregion
    }
}
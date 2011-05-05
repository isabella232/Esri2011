using System;
using System.Collections.Generic;
using EsriDE.Samples.AgsServicesJsonDeSerializer;

namespace EsriDE.Samples.ContentFinder.ContentAdapter.Ags
{
	public class ImageAgsUriAnalyzer : UriAnalyzer
	{
		protected override IEnumerable<Uri> GetCompositeUris(Uri currentUri)
		{
			var directory = new AgsServicesDirectory(currentUri);
			//    private void ProcessImageServiceDirectory(AgsServicesDirectory directory, bool checkSubDirectory)
			//{
			foreach (Service service in directory.GetImageServices(true))
			{
				ImageServiceInfo imageServiceInfo = AgsImageServiceInfo.GetImageServiceInfo(directory,
				                                                                            service.RESTServiceUrl);
				//ProcessImageServiceInfo(imageServiceInfo);

			}

			throw new NotImplementedException();
		}

		protected override IEnumerable<Uri> GetLeafUris(Uri currentUri)
		{
			throw new NotImplementedException();
		}

		private void ProcessImageServiceInfo(ImageServiceInfo imageServiceInfo)
		{
			if (null == imageServiceInfo.Service)
			{
				return;
			}

			//Guid guid = Guid.NewGuid();

			//IMetaData metaData = CreateMetaData(imageServiceInfo, guid);
			//OnDataReaded(metaData);

			//ReadImageAsync(imageServiceInfo, guid);
			//Todo
		}

		//private void ProcessImageService(Uri restServiceUrl, AgsServicesDirectory directory)
		//{
		//    ImageServiceInfo imageServiceInfo = AgsImageServiceInfo.GetImageServiceInfo(directory, restServiceUrl);
		//    if (null != imageServiceInfo)
		//    {
		//        ProcessImageServiceInfo(imageServiceInfo);
		//    }
		//}

		//private void ReadImageAsync(object serviceInfo, Guid guid)
		//{
		//    try
		//    {
		//        var imageInfoDto = new ImageInfoDto(guid, serviceInfo);
		//        _imageReadingProcessingStatus.Add(guid, true);

		//        //ThreadPool.QueueUserWorkItem(GetImageFromServiceInfo, imageInfoDto);
		//        var thread = new Thread(GetImageFromServiceInfo)
		//        {
		//            Priority = ThreadPriority.Lowest
		//        };
		//        _imageReadingThreads.Add(thread, true);
		//        Debug.WriteLine("======================================================== Thread#: " +
		//                        _imageReadingThreads.Count);
		//        thread.Start(imageInfoDto);
		//    }
		//    catch (Exception e)
		//    {
		//        Console.WriteLine(e);
		//    }
		//}
	}
}
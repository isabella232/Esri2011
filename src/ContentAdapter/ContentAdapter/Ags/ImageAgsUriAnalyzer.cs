using System;
using System.Collections.Generic;
using EsriDE.Commons.Ags;
using EsriDE.Commons.Ags.Contracts;

namespace EsriDE.Samples.ContentFinder.ContentAdapter.Ags
{
	public class ImageAgsUriAnalyzer : AgsUriAnalyzer
	{
		protected override IEnumerable<Uri> GetLeafUris(Uri currentUri)
		{
			var leafs = base.GetLeafUris(currentUri);

			foreach (var leaf in leafs)
			{
				ServiceType type = ServiceType.Unknown;
				try
				{
					type = AgsUtil.GetServiceType(leaf);
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}

				if (ServiceType.ImageService == type)
				{
					yield return leaf;
				}
			}
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
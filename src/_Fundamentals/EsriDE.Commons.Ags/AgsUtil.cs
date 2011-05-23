using System;
using System.Collections.Generic;
using System.Linq;
using EsriDE.Commons.Ags.Contracts;
using EsriDE.Samples.ContentFinder.ContentAdapter.Ags;

namespace EsriDE.Commons.Ags
{
	public static class AgsUtil
	{
		public static IEnumerable<MapServiceInfo> GetMapServices(Uri directory)
		{
			var services = GetServices(directory);

			return from service in services
			       where string.Equals(service.Type, "mapserver", StringComparison.InvariantCultureIgnoreCase)
			       select JsonUtil.GetHttpJsonRequestResult(service.RESTServiceUrl.AbsoluteUri)
			       into uri select JsonUtil.Deserialize<MapServiceInfo>(uri);
		}

		public static IEnumerable<ImageServiceInfo> GetImageServices(Uri directory)
		{
			var services = GetServices(directory);

			foreach (var service in services)
			{
				if (string.Equals(service.Type, "imageserver", StringComparison.InvariantCultureIgnoreCase))
				{
					var uri = JsonUtil.GetHttpJsonRequestResult(service.RESTServiceUrl.AbsoluteUri);
					var result = JsonUtil.Deserialize<ImageServiceInfo>(uri);
					yield return result;
				}
			}
		}

		public static IEnumerable<Service> GetServices(Uri directory)
		{
			var address = JsonUtil.GetHttpJsonRequestResult(directory.AbsoluteUri);
			var rootdirectory =
					JsonUtil.Deserialize<ServiceDirectory>(address);

			foreach (Service service in rootdirectory.Services)
			{
				var serviceName = TrimLeadingDirectory(service.Name);
				var serviceAddress = string.Format(@"{0}/{1}/{2}", directory, serviceName, service.Type);
				service.RESTServiceUrl = new Uri(serviceAddress);
				yield return service;
			}
		}

		public static IEnumerable<Uri> GetServiceUris(Uri uri)
		{
			var path = TrimEndingSlash(uri.AbsoluteUri);

			ServiceDirectory rootdirectory;
			try
			{
				rootdirectory = GetServiceDirectory(path);
			}
			catch (Exception e)
			{
				yield break;
			}

			foreach (Service service in rootdirectory.Services)
			{
				var serviceName = TrimLeadingDirectory(service.Name);
				var serviceAddress = string.Format(@"{0}/{1}/{2}", path, serviceName, service.Type);
				var result = new Uri(serviceAddress);

				yield return result;
			}
		}

		public static IEnumerable<Uri> GetFolderUris(Uri uri)
		{
			var path = TrimEndingSlash(uri.AbsoluteUri);

			ServiceDirectory rootdirectory;
			try
			{
				rootdirectory = GetServiceDirectory(path);
			}
			catch (Exception e)
			{
				yield break;
			}

			foreach (string folder in rootdirectory.Folders)
			{
				var fullFolder = string.Format(@"{0}/{1}", path, folder);
				var result = new Uri(fullFolder);

				yield return result;
			}
		}

		public static string GetServiceName(string fullpath)
		{
			var path = TrimServiceType(fullpath);

			var occurence = path.LastIndexOf('/');
			var length = path.Length - occurence - 1;
			var result = path.Substring(occurence + 1, length);

			return result;
		}

		public static string TrimEndingSlash(string s)
		{
			if (s.EndsWith("/"))
			{
				var result = s.TrimEnd('/');
				return result;
			}

			return s;
		}

		// ServerType (MapServer, ImageServer, ..) entfernen
		public static string TrimServiceType(string s)
		{
			var occurence = s.LastIndexOf('/');
			var result = s.Substring(0, occurence);
			return result;
		}

		public static ServiceType GetServiceType(Uri uri)
		{
			var path = TrimEndingSlash(uri.AbsoluteUri);

			var parentDir = GetParentDirectoryName(path);
			var serviceName = GetServiceName(path);

			var services = AgsUtil.GetServices(new Uri(parentDir));
			foreach (var service in services)
			{
				var serviceName2 = AgsUtil.TrimLeadingDirectory(service.Name);

				if (string.Equals(serviceName, serviceName2, StringComparison.InvariantCultureIgnoreCase))
				{
					var result = GetServiceType(service.Type);
					return result;
				}
			}

			return ServiceType.Unknown;
		}

		public static string GetParentDirectoryName(string fullpath)
		{
			var path = TrimServiceType(fullpath);

			var occurence = path.LastIndexOf('/');
			var result = path.Substring(0, occurence);

			return result;
		}

		internal static Uri TrimRestFragment(Uri directory)
		{
			var dir = directory.AbsoluteUri;
			var resultDir = dir.Replace(@"/rest/", @"/");

			return new Uri(resultDir);
		}

		public static Uri GetSoapRepresentation(Uri directory)
		{
			var trimmedUri = TrimRestFragment(directory);
			var resultPath = GetParentDirectoryName(trimmedUri.AbsoluteUri);

			return new Uri(resultPath);
		}

		private static ServiceType GetServiceType(string type)
		{
			if (string.Equals("mapserver", type, StringComparison.InvariantCultureIgnoreCase))
			{
				return ServiceType.MapService;
			}

			if (string.Equals("imageserver", type, StringComparison.InvariantCultureIgnoreCase))
			{
				return ServiceType.ImageService;
			}

			return ServiceType.Unknown;
		}

		internal static ServiceDirectory GetServiceDirectory(string path)
		{
			var address = JsonUtil.GetHttpJsonRequestResult(path);
			var rootdirectory = JsonUtil.Deserialize<ServiceDirectory>(address);
			return rootdirectory;
		}

		internal static string TrimLeadingDirectory(string serviceName)
		{
			if (serviceName.Contains(@"/"))
			{
				var occurence = serviceName.IndexOf(@"/");
				var lenght = serviceName.Length - occurence - 1;
				var result = serviceName.Substring(occurence + 1, lenght);
				return result;
			}

			return serviceName;
		}
	}
}
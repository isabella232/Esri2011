using System;
using System.Collections.Generic;
using System.Linq;
using EsriDE.Commons.Ags.Contracts;

namespace EsriDE.Commons.Ags
{
	public class AgsServicesDirectory
	{
        private readonly Uri _agsRootUri;
        private readonly Uri _deliveredUri;

        private readonly List<ServiceDirectory> _fullAgsServiceDirectory;
        private string _currentVersion;

		public AgsServicesDirectory(Uri url)
        {
            _deliveredUri = url;
            _agsRootUri = GetAgsRootUri(_deliveredUri);
            _fullAgsServiceDirectory = CreateFullAgsServiceDirectory(_agsRootUri);
        }

        #region Properties

        public string CurrentVersion
        {
            get //get-Accessor
            { return _currentVersion; }
        }

        public Uri Url
        {
            get //get-Accessor
            { return _agsRootUri; }
        }

        #endregion

        #region methods

        private static Uri GetAgsRootUri(Uri url)
        {
            try
            {
                var uriBuilder = new UriBuilder();
                uriBuilder.Scheme = url.Scheme;
                uriBuilder.Port = url.Port;
                uriBuilder.Host = url.Host;

                foreach (string segment in url.Segments)
                {
                    if (segment == @"/")
                    {
                        continue;
                    }

                    uriBuilder.Path += segment;
                    if (segment.ToLower() == @"rest/")
                    {
                        uriBuilder.Path += "services/";
                        break;
                    }
                }

                return uriBuilder.Uri;
            }
            catch
            {
                return null;
            }
        }

        private List<ServiceDirectory> CreateFullAgsServiceDirectory(Uri url)
        {
            var directories = new List<ServiceDirectory>();

            try
            {
                var rootdirectory =
                    JsonUtil.Deserialize<ServiceDirectory>(JsonUtil.GetHttpJsonRequestResult(url.AbsoluteUri));
                rootdirectory.Folder = "*root*";
                rootdirectory.Url = url;
                if (string.IsNullOrEmpty(rootdirectory.CurrentVersion))
                {
                    return null;
                }

                _currentVersion = rootdirectory.CurrentVersion;

                foreach (Service service in rootdirectory.Services)
                {
                    var uribuilder = new UriBuilder(url);
                    if (rootdirectory.Folder != "*root*")
                    {
                        uribuilder.Path += rootdirectory.Folder + @"/";
                    }
                    uribuilder.Path += service.Name + @"/";
                    uribuilder.Path += service.Type + @"/";

                    service.RESTServiceUrl = uribuilder.Uri;
                    service.Folder = rootdirectory.Folder;
                    service.RESTServerUrl = _agsRootUri;
                }


                directories.Add(rootdirectory);


                foreach (string folder in rootdirectory.Folders)
                {
                    var directory =
                        JsonUtil.Deserialize<ServiceDirectory>(
                            JsonUtil.GetHttpJsonRequestResult(url.AbsoluteUri + @"/" + folder + @"/"));
                    directory.Folder = folder;

                    var builder = new UriBuilder(url);
                    builder.Path += folder + @"/";
                    directory.Url = builder.Uri;

                    if (string.IsNullOrEmpty(directory.CurrentVersion))
                    {
                        return null;
                    }

                    foreach (Service service in directory.Services)
                    {
                        var uribuilder = new UriBuilder(url);
                        uribuilder.Path += service.Name + @"/";
                        uribuilder.Path += service.Type + @"/";

                        service.RESTServiceUrl = uribuilder.Uri;
                        service.Folder = rootdirectory.Folder;
                        service.RESTServerUrl = _agsRootUri;
                    }

                    directories.Add(directory);
                }

                return directories;
            }
            catch
            {
                return null;
            }
        }

        public List<Service> GetMapServices(bool includeSubDirectories)
        {
            var services = new List<Service>();
            if (_fullAgsServiceDirectory == null)
            {
                return services;
            }


            string[] segments = _deliveredUri.Segments;
            string subFolder = segments[segments.Length - 1].ToLower();
            subFolder = subFolder.Replace("/", string.Empty);

            foreach (ServiceDirectory serviceDirectory in _fullAgsServiceDirectory)
            {
                if (subFolder == serviceDirectory.Folder.ToLower() ||
                    (!includeSubDirectories && serviceDirectory.Folder == "*root*"))
                {
                    services.Clear();
                    services.AddRange(serviceDirectory.Services.Where(service => service.Type.ToLower() == "mapserver"));
                    return services;
                }

                services.AddRange(serviceDirectory.Services.Where(service => service.Type.ToLower() == "mapserver"));
            }

            return services;
        }

        public List<Service> GetImageServices(bool includeSubDirectories)
        {
            var services = new List<Service>();
            if (_fullAgsServiceDirectory == null)
            {
                return services;
            }

            string[] segments = _deliveredUri.Segments;
            string subFolder = segments[segments.Length - 1].ToLower();
            subFolder = subFolder.Replace("/", string.Empty);

            foreach (ServiceDirectory serviceDirectory in _fullAgsServiceDirectory)
            {
                if (subFolder == serviceDirectory.Folder.ToLower() ||
                    (!includeSubDirectories && serviceDirectory.Folder == "*root*"))
                {
                    services.Clear();
                    services.AddRange(serviceDirectory.Services.Where(service => service.Type.ToLower() == "imageserver"));
                    return services;
                }


                services.AddRange(serviceDirectory.Services.Where(service => service.Type.ToLower() == "imageserver"));
            }

            return services;
        }

        public List<ServiceDirectory> AgsServiceDirectory()
        {
            return _fullAgsServiceDirectory;
        }

        #endregion
	}
}
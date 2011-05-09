using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;

namespace EsriDE.Commons.Ags
{
	public static class JsonUtil
	{
		private const string _defaultJasonRequestParameter = @"f=json";

		public static string Serialize<T>(T obj)
		{
			var serializer = new DataContractJsonSerializer(obj.GetType());
			var ms = new MemoryStream();
			serializer.WriteObject(ms, obj);
			string retVal = Encoding.Default.GetString(ms.ToArray());
			ms.Dispose();
			return retVal;
		}

		public static T Deserialize<T>(string json)
		{
			var obj = Activator.CreateInstance<T>();
			var ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
			var serializer = new DataContractJsonSerializer(obj.GetType());
			obj = (T)serializer.ReadObject(ms);
			ms.Close();
			ms.Dispose();
			return obj;
		}

		public static string GetHttpJsonRequestResult(string url)
		{
			return GetHttpJsonRequestResult(url, _defaultJasonRequestParameter);
		}

		public static string GetHttpJsonRequestResult(string url, string jsonRequestParameter)
		{
			var fullUrl = string.Format(@"{0}?{1}", url, jsonRequestParameter);

			var req = (HttpWebRequest) WebRequest.Create(fullUrl);
			
			string result;
			using (var resp = (HttpWebResponse) req.GetResponse())
			{
				var reader = new StreamReader(resp.GetResponseStream());
				result = reader.ReadToEnd();
			}
			return result;
		}
	}
}
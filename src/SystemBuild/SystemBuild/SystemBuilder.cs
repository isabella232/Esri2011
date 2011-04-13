using System;

namespace EsriDE.Samples.ContentFinder.SystemBuild
{
	public class SystemBuilder
	{
		private Castle.Windsor.IWindsorContainer _container;

		public SystemBuilder()
		{
			var fullFilename = GetConfigFilename();
			_container = new Castle.Windsor.WindsorContainer(fullFilename);
		}

		public object InitializeSystem(Type type)
		{
			var result = _container.Resolve(type);
			return result;
		}

		private string GetConfigFilename()
		{
			return "CastleWindsor.config";

		}
	}
}
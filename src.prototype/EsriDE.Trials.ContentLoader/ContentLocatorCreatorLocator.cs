using System.Collections.Generic;

namespace EsriDE.Trials.ContentLoader
{
	public interface IContentLocatorCreatorLocator
	{
		IEnumerable<ContentLocatorCreator> GetAllContentLocatorCreators();
	}

	public class ContentLocatorCreatorLocator : IContentLocatorCreatorLocator
	{
		public IEnumerable<ContentLocatorCreator> GetAllContentLocatorCreators()
		{
			yield return new MxdContentLocatorCreator();
			yield return new AgsContentLocatorCreator();
			yield break;
		}
	}
}
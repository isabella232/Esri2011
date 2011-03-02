using EsriDE.Samples.ContentFinder.ContentAdapter.Contract;
using EsriDE.Samples.ContentFinder.DomainModel;

namespace EsriDE.Samples.ContentFinder.ContentAdapter
{
	internal class NullContentLocatorCreator : IContentLocatorCreator
	{
		public bool IsResponsibleFor(SourceBundle sourceBundle)
		{
			return false;
		}

		public IContentLocator CreateContentLocator(SourceBundle sourceBundle)
		{
			return new NullContentLocator();
		}
	}
}
//internal class ContentLocatorResolverOld : IContentLocatorResolver
//{
//    private readonly IEnumerable<IContentLocatorCreator> _contentLocatorCreators;

//    public ContentLocatorResolverOld(IEnumerable<IContentLocatorCreator> contentLocatorCreators)
//    {
//        _contentLocatorCreators = contentLocatorCreators;
//    }

//    public IContentLocator ResolveContentLocator(SourceBundle sourceBundle)
//    {
//        var contentLocatorCreator = GetContentLocatorCreator(sourceBundle);
//        var contentLocator = contentLocatorCreator.CreateContentLocator(sourceBundle);
//        return contentLocator;
//    }

//    protected virtual IContentLocatorCreator GetContentLocatorCreator(SourceBundle sourceBundle)
//    {
//        //foreach (var creator in _contentLocatorCreators)
//        //{
//        //    if (creator.IsResponsibleFor(sourceBundle))
//        //    {
//        //        return creator;
//        //    }
//        //}

//        return new NullContentLocatorCreator();
//    }
//}


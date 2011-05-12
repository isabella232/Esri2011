using EsriDE.Trials.ContentLoader.DomainModel;

namespace EsriDE.Trials.ContentLoader.CoR
{
	public interface IResolver
	{
		IFactory Resolve(Content content);
	}
}
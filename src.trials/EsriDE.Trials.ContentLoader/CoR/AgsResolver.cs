using EsriDE.Trials.ContentLoader.DomainModel;

namespace EsriDE.Trials.ContentLoader.CoR
{
	public class AgsResolver : Resolver
	{
		public AgsResolver(IResolver sucesseor, IFactory factory)
			: base(sucesseor, factory)
		{

		}

		protected override bool Responsible(Content content)
		{
			return content.GetType() == typeof(AgsContent);
		}
	}
}

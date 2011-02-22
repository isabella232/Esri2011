using EsriDE.Trials.ContentLoader.DomainModel;

namespace EsriDE.Trials.ContentLoader.CoR
{
	public class MxdResolver : Resolver
	{
		public MxdResolver(IResolver sucesseor, IFactory factory)
			: base(sucesseor, factory)
		{
			
		}

		protected override bool Responsible(Content content)
		{
			return content.GetType() == typeof (MxdContent);
		}
	}
}
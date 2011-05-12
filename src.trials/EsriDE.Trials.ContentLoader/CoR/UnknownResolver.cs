using EsriDE.Trials.ContentLoader.DomainModel;

namespace EsriDE.Trials.ContentLoader.CoR
{
	public class UnknownResolver : Resolver
	{
		//public UnknownResolver(IResolver sucsessor, IFactory factory) : base(sucsessor, factory)
		//{
		//}

		public UnknownResolver()
			: base(null, new UnknownFactory())
		{
		}

		protected override bool Responsible(Content content)
		{
			return true;
		}
	}
}
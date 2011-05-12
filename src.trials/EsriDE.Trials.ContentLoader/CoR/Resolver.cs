using EsriDE.Trials.ContentLoader.DomainModel;

namespace EsriDE.Trials.ContentLoader.CoR
{
	public abstract class Resolver : IResolver
	{
		private readonly IFactory _factory;
		protected IResolver Sucsessor { get; set; }

		public Resolver(IResolver sucsessor, IFactory factory)
		{
			_factory = factory;
			Sucsessor = sucsessor;
		}

		public IFactory Resolve(Content content)
		{
			if (Responsible(content))
				return _factory;
			else
			{
				return Sucsessor.Resolve(content);
			}
		}

		protected abstract bool Responsible(Content content);
	}
}
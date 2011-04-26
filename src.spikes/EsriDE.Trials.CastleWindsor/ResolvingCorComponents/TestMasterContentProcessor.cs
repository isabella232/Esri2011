using EsriDE.Trials.CastleWindsor.ResolvingCorComponents.Cor;

namespace EsriDE.Trials.CastleWindsor.ResolvingCorComponents
{
	public class TestMasterContentProcessor : IContentProcessor
	{
		private readonly ContentProcessor _firstProzessor;

		public TestMasterContentProcessor(ContentProcessor firstProzessor)
		{
			_firstProzessor = firstProzessor;
		}

		public void Process(string s)
		{
			_firstProzessor.Process(s);
		}
	}
}
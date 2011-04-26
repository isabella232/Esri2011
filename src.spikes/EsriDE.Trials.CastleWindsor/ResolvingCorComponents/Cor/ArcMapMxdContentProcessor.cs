using System;

namespace EsriDE.Trials.CastleWindsor.ResolvingCorComponents.Cor
{
	public class ArcMapMxdContentProcessor : AgdContentProcessor
	{
		public ArcMapMxdContentProcessor(ContentProcessor nextProcessor, IApplication application)
			: base(nextProcessor, "ArcMapMxdContentProcessor", application)
		{
			Console.WriteLine("ArcMapMxdContentProcessor.ctor()");
		}

		protected override void ProcessCore(string s)
		{
			Console.WriteLine("ArcMapMxdContentProcessor.ProcessCore: " + s);
		}
	}
}
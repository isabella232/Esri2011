using System;

namespace EsriDE.Trials.CastleWindsor.ResolvingCorComponents.Cor
{
	public class ArcMapAgsContentProcessor : AgdContentProcessor
	{
		public ArcMapAgsContentProcessor(ContentProcessor nextProcessor, IApplication application)
			: base(nextProcessor, "ArcMapAgsContentProcessor", application)
		{
			Console.WriteLine("ArcMapAgsContentProcessor.ctor()");
		}

		protected override void ProcessCore(string s)
		{
			Console.WriteLine("ArcMapAgsContentProcessor.ProcessCore: " + s);
		}
	}
}
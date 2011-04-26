using System;

namespace EsriDE.Trials.CastleWindsor.ResolvingCorComponents.Cor
{
	class EndProcessor : ContentProcessor
	{
		public EndProcessor() : base(null)
		{
			Console.WriteLine("EndProcessor.ctor()");
		}

		protected override bool IsResponsibleFor(string s)
		{
			return true;
		}

		protected override void ProcessCore(string s)
		{
			Console.WriteLine("EndProcessor.ProcessCore");
		}
	}
}
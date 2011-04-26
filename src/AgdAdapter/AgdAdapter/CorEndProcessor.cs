using System;
using EsriDE.Samples.ContentFinder.DomainModel;

namespace EsriDE.Samples.ContentFinder.AgdAdapter
{
	public class CorEndProcessor : CorBasedContentProcessor
	{
		public CorEndProcessor() : base(null)
		{
		}

		protected override bool IsResponsibleFor(Content content)
		{
			return true;
		}

		protected override void ProcessCore(Content content)
		{
			Console.WriteLine("Nothing to process.");
		}
	}
}
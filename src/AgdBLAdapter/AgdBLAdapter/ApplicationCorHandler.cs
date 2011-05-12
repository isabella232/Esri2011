using System;
using EsriDE.Commons.Patterns;
using EsriDE.Samples.ContentFinder.ApplicationAdapter.Contract;
using EsriDE.Samples.ContentFinder.DomainModel;

namespace EsriDE.Samples.ContentFinder.AgdBLAdapter
{
	public abstract class ApplicationCorHandler : ChainOfResponsibilityHandler<Content>
	{
		protected ApplicationCorHandler(ApplicationCorHandler successor, IApplicationAdapter applicationAdapter)
			: base(successor)
		{
			ApplicationAdapter = applicationAdapter;
		}

		protected IApplicationAdapter ApplicationAdapter { get; private set; }
	}

	public class EndApplicationCorHandler : ApplicationCorHandler
	{
		public EndApplicationCorHandler() : base(null, null)
		{
		}

		protected override bool IsResponsible(Content data)
		{
			return true;
		}

		protected override void ProcessCore(Content data)
		{
			return;
		}
	}
}

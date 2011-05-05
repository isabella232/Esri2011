using System;
using EsriDE.Samples.ContentFinder.ApplicationAdapter.Contract;
using EsriDE.Samples.ContentFinder.DomainModel;

namespace AgdBLAdapter.Tests
{
	public abstract class ApplicationCorHandler : CorHandler<Content>
	{
		protected IApplicationAdapter ApplicationAdapter { get; private set; }
		protected ApplicationCorHandler(ApplicationCorHandler nextLink, IApplicationAdapter applicationAdapter)
			: base(nextLink)
		{
			ApplicationAdapter = applicationAdapter;
		}
	}
}
using EsriDE.Samples.ContentFinder.ApplicationAdapter.Contract;
using EsriDE.Samples.ContentFinder.ContentAdapter.Mxd;
using EsriDE.Samples.ContentFinder.DomainModel;

namespace AgdBLAdapter.Tests
{
	public class ApplicationMxdCorHandler : ApplicationCorHandler
	{

		public ApplicationMxdCorHandler(ApplicationCorHandler nextLink, IApplicationAdapter applicationAdapter)
			: base(nextLink, applicationAdapter)
		{
		}

		protected override bool IsResponsible(Content data)
		{
			var result = typeof (MxdContent) == data.GetType();
			return result;
		}

		protected override void ProcessCore(Content data)
		{
			var app = ApplicationAdapter.Application;
			app.OpenDocument(data.Uri.LocalPath);
		}
	}
}
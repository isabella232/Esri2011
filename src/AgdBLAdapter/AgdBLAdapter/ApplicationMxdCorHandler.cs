using EsriDE.Samples.ContentFinder.ApplicationAdapter.Contract;
using EsriDE.Samples.ContentFinder.ContentAdapter.Mxd;
using EsriDE.Samples.ContentFinder.DomainModel;

namespace EsriDE.Samples.ContentFinder.AgdBLAdapter
{
	public class ApplicationMxdCorHandler : ApplicationCorHandler
	{
		public ApplicationMxdCorHandler(ApplicationCorHandler successor, IApplicationAdapter applicationAdapter)
			: base(successor, applicationAdapter)
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

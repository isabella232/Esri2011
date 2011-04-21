using EsriDE.Samples.ContentFinder.DomainModel;

namespace EsriDE.Samples.ContentFinder.AgdAdapter
{
	//public interface IApplication
	//{
	//    void OpenDocument(Uri uri);
		
	//}

	public interface IArcMapAdapter
	{
		void Process(Content content);
	}
}
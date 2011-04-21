namespace EsriDE.Samples.ContentFinder.AgdAdapter.Tests
{
	public class FakedContentFinderButton : ContentFinderButton
	{
		public void EmulateClick()
		{
			OnClicked();
		}
	}
}
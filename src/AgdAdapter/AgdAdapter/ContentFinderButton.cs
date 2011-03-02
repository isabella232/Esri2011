using System;

namespace EsriDE.Samples.ContentFinder.AgdAdapter
{
	public class ContentFinderButton : Button
	{
		protected override IButtonPresenter CreatePresenter()
		{
			return new ContentFinderButtonPresenter(this)
		}
	}
}
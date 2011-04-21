using System;

namespace EsriDE.Samples.ContentFinder.UI.Contract
{
	public interface IToggleableForm : IToggleableView
	{
		void SetParent();
		event Action Closing;
	}
}
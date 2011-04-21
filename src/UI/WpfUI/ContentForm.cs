using System;
using EsriDE.Samples.ContentFinder.UI.Contract;

namespace EsriDE.Samples.ContentFinder.WpfUI
{
	public class ContentForm : IToggleableForm
	{
		#region IFormView Members
		public void SetParent()
		{
		}

		public void Show()
		{
		}

		public void Hide()
		{
		}

		public event Action Closing;
		#endregion
	}
}
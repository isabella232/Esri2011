using System;
using EsriDE.Trials.CastleWindsor.ComplexUI.Contracts.Forms;

namespace EsriDE.Trials.CastleWindsor.ComplexUI.Implementations.Forms
{
	internal class FormView : IFormView
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
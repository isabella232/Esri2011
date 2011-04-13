using System;

namespace EsriDE.Trials.CastleWindsor.ComplexUI.Contracts.Forms
{
	internal interface IFormView
	{
		void SetParent();
		void Show();
		void Hide();

		event Action Closing;
	}
}
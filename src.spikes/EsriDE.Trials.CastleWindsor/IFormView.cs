using System;

namespace EsriDE.Trials.CastleWindsor
{
	internal interface IFormView
	{
		void SetParent();
		void Show();
		void Hide();

		event Action Closing;
	}

	class FormView : IFormView
	{
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
	}
}
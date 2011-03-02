using System;

namespace EsriDE.Samples.ContentFinder.AgdAdapter
{
	public abstract class ButtonModel : IButtonModel
	{
		public abstract void Do();

	}

	class ContentFinderButtonModel : ButtonModel
	{
		public override void Do()
		{
			var v = ConstructSystem();
		}

		private object ConstructSystem()
		{
			
		}
	}

	internal interface IUiManager
	{
		void Show();
	}
}
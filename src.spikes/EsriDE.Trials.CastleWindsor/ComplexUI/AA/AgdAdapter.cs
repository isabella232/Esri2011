using System;

namespace EsriDE.Trials.CastleWindsor.ComplexUI.AA
{
	public class AgdAdapter : IAgdAdapter
	{
		public AgdAdapter(IContentModel model)
		{
			model.ContentSelected +=
				content => Console.WriteLine(content.ToString());
		}

		#region IAgdAdapter Members
		public int WindowHandle()
		{
			return 0;
		}

		public void OpenSomething(object o)
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
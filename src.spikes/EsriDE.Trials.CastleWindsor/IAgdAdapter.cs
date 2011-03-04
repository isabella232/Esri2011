using System;

namespace EsriDE.Trials.CastleWindsor
{
	public interface IAgdAdapter
	{
		int WindowHandle();
		void OpenSomething(object o);
	}

	public class AgdAdapter : IAgdAdapter
	{
		public AgdAdapter(IContentModel model)
		{
			model.ContentSelected += 
				content => Console.WriteLine(content.ToString());
		}

		public int WindowHandle()
		{
			return 0;
		}

		public void OpenSomething(object o)
		{
			throw new NotImplementedException();
		}
	}
}
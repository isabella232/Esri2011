using System;

namespace EsriDE.Trials.CastleWindsor
{
	public interface IC
	{
	}

	class C : IC
	{
		public C()
		{
			Console.WriteLine("C.ctor()");
		}
	}
}
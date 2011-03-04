using System;

namespace EsriDE.Trials.CastleWindsor.Primitives
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
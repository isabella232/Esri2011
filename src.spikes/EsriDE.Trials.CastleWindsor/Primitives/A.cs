using System;

namespace EsriDE.Trials.CastleWindsor.Primitives
{
	public interface IA
	{
	}

	class A : IA
	{
		public A()
		{
			Console.WriteLine("A.ctor()");
		}
	}
}
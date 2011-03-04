using System;

namespace EsriDE.Trials.CastleWindsor
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
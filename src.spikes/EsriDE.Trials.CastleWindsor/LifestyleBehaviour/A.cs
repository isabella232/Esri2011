using System;

namespace EsriDE.Trials.CastleWindsor.LifestyleBehaviour
{
	public class A : IA
	{
		public A()
		{
			Console.WriteLine("A.ctor()");
		}

		~A()
		{
			Console.WriteLine("~A()");
		}
	}
}
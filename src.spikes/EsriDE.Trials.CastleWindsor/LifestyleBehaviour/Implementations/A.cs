using System;
using EsriDE.Trials.CastleWindsor.LifestyleBehaviour.Contracts;

namespace EsriDE.Trials.CastleWindsor.LifestyleBehaviour.Implementations
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
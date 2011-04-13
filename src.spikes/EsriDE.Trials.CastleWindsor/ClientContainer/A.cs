using System;

namespace EsriDE.Trials.CastleWindsor.ClientContainer
{
	public interface IA
	{
		
	}
	
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
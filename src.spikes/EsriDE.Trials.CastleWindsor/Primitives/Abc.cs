using System;

namespace EsriDE.Trials.CastleWindsor
{
	public interface IAbc
	{
		void DoThis();
	}

	public class Abc : IAbc
	{
		public Abc(IA a, IB b, IC c)
		{
			Console.WriteLine("Abc.ctor()");
		}

		public void DoThis()
		{
			
		}
	}
}
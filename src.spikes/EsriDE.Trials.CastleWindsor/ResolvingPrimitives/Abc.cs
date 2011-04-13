using System;

namespace EsriDE.Trials.CastleWindsor.ResolvingPrimitives
{
	public class Abc : IAbc
	{
		public Abc(IA a, IB b, IC c)
		{
			Console.WriteLine("Abc.ctor()");
		}

		#region IAbc Members
		public void DoThis()
		{
		}
		#endregion
	}
}
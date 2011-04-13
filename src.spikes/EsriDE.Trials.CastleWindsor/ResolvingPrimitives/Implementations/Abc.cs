using System;
using EsriDE.Trials.CastleWindsor.ResolvingPrimitives.Contracts;

namespace EsriDE.Trials.CastleWindsor.ResolvingPrimitives.Implementations
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
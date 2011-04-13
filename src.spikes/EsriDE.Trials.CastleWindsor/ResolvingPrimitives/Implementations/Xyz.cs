using System;
using EsriDE.Trials.CastleWindsor.ResolvingPrimitives.Contracts;

namespace EsriDE.Trials.CastleWindsor.ResolvingPrimitives.Implementations
{
	public class Xyz : IXyz
	{
		public Xyz()
		{
			Console.WriteLine("Xyz.ctor()");
		}

		#region IXyz Members
		public void DoThat()
		{
		}
		#endregion
	}
}
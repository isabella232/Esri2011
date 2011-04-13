using System;

namespace EsriDE.Trials.CastleWindsor.ResolvingPrimitives
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
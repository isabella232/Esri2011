using System;

namespace EsriDE.Trials.CastleWindsor.Primitives
{
	public interface IB
	{
	}

	class B : IB
	{
		public B()
		{
			Console.WriteLine("B.ctor()");
		}
	}
}
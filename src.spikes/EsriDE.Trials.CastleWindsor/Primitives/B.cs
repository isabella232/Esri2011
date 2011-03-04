using System;

namespace EsriDE.Trials.CastleWindsor
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
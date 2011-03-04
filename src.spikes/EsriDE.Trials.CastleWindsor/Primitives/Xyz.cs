using System;

namespace EsriDE.Trials.CastleWindsor.Primitives
{
	public interface IXyz
	{
		void DoThat();
	}
	
	public class Xyz : IXyz
	{
		public Xyz()
		{
			Console.WriteLine("Xyz.ctor()");
		}

		public void DoThat()
		{
			
		}
	}
}
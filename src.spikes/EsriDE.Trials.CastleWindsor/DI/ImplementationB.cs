using System;
using System.Reflection;

namespace EsriDE.Trials.CastleWindsor.DI
{
	public class ImplementationB : ContractB
	{
		public ImplementationB()
		{
			var info = string.Format("{0} -> {1}", MethodBase.GetCurrentMethod().DeclaringType, MethodBase.GetCurrentMethod());
			Console.WriteLine(info);
		}
	}
}
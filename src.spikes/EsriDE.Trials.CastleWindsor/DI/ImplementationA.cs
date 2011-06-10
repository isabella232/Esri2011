using System;
using System.Reflection;

namespace EsriDE.Trials.CastleWindsor.DI
{
	public class ImplementationA : ContractA
	{
		public ImplementationA(ContractB b, ContractC c)
		{
			var info = string.Format("{0} -> {1}", MethodBase.GetCurrentMethod().DeclaringType, MethodBase.GetCurrentMethod());
			Console.WriteLine(info);
		}
	}
}

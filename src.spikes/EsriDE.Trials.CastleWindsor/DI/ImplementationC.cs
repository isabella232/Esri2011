using System;
using System.Reflection;

namespace EsriDE.Trials.CastleWindsor.DI
{
	public class ImplementationC : ContractC
	{
		public ImplementationC(ContractD d, ContractE e)
		{
			var info = string.Format("{0} -> {1}", MethodBase.GetCurrentMethod().DeclaringType, MethodBase.GetCurrentMethod());
			Console.WriteLine(info);
		}
	}
}
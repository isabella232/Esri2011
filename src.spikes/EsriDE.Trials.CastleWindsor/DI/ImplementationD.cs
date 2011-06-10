using System;
using System.Reflection;

namespace EsriDE.Trials.CastleWindsor.DI
{
	public class ImplementationD : ContractD
	{
		public ImplementationD()
		{
			var info = string.Format("{0} -> {1}", MethodBase.GetCurrentMethod().DeclaringType, MethodBase.GetCurrentMethod());
			Console.WriteLine(info);
		}
	}
}
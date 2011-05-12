using System;

namespace EsriDE.Trials.Patterns.TemplateMethod
{
	public class Concrete : Abstract
	{
		protected override void MacheDies()
		{
			Console.WriteLine("MacheDies");
		}

		protected override void MacheJenes()
		{
			Console.WriteLine("MacheDies");
		}
	}
}
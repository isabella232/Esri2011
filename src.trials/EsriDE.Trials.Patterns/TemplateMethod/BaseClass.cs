using System;

namespace EsriDE.Trials.Patterns.TemplateMethod
{
	public abstract class Abstract
	{
		public void TemplateMethod()
		{
			MacheAmAnfang();
			MacheDies();
			MacheJenes();
			MacheAmEnde();
		}

		protected abstract void MacheDies();
		protected abstract void MacheJenes();

		private void MacheAmAnfang()
		{
			Console.WriteLine("MacheAmAnfang");
		}

		private void MacheAmEnde()
		{
			Console.WriteLine("MacheAmEnde");
		}
	}
}

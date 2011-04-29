using System;

namespace EsriDE.Trials.CastleWindsor.ResolvingWithSetterInjection
{
	public class Foo : IFoo
	{
		private ISomething _something;

		public void FooDo()
		{
			throw new NotImplementedException();
		}

		public ISomething Something
		{
			get { return _something; }
			set
			{
				Console.WriteLine(value);
				_something = value;
			}
		}
	}
}
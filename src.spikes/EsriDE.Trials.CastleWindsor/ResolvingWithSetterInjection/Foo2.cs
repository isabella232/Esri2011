using System;

namespace EsriDE.Trials.CastleWindsor.ResolvingWithSetterInjection
{
	public class Foo2 : IFoo2
	{
		private ISomething _something;

		public void FooDo2()
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
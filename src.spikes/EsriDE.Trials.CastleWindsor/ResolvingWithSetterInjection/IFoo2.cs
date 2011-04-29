namespace EsriDE.Trials.CastleWindsor.ResolvingWithSetterInjection
{
	public interface IFoo2
	{
		void FooDo2();
		ISomething Something { get; set; }
	}
}
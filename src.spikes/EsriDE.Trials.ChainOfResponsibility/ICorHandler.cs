namespace EsriDE.Trials.ChainOfResponsibility
{
	public interface ICorHandler<T>
	{
		void Process(T data);
		CorHandler<T> NextLink { get; set; }
	}
}
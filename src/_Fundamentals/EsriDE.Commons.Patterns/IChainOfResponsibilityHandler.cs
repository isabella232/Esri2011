namespace EsriDE.Commons.Patterns
{
	public interface IChainOfResponsibilityHandler<T>
	{
		IChainOfResponsibilityHandler<T> Successor { get; set; }

		void Process(T data);
	}
}
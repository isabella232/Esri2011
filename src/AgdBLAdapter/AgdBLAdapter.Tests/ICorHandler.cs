namespace AgdBLAdapter.Tests
{
	public interface ICorHandler<T>
	{
		void Process(T data);
		ICorHandler<T> NextLink { get; set; }
	}
}
using System;

namespace EsriDE.Trials.ChainOfResponsibility
{
	public class CorHandlerOne : CorHandler<IData>
	{
		public CorHandlerOne(CorHandler<IData> nextLink)
			: base(nextLink)
		{
		}

		public CorHandlerOne() 
			: base(null)
		{
		}

		protected override bool IsResponsible(IData data)
		{
			return typeof (DemoDataOne) == data.GetType();
		}

		protected override void ProcessCore(IData data)
		{
			Console.WriteLine("This is for 'CorHandlerOne'");
		}
	}

	public class CorHandlerTwo : CorHandler<IData>
	{
		public CorHandlerTwo()
			: base(null)
		{
		}

		public CorHandlerTwo(CorHandler<IData> nextLink)
			: base(nextLink)
		{
		}

		protected override bool IsResponsible(IData data)
		{
			return typeof (DemoDataTwo) == data.GetType();
		}

		protected override void ProcessCore(IData data)
		{
			Console.WriteLine("This is for 'CorHandlerTwo'");
		}
	}

	public abstract class CorHandlerCool<T> : CorHandler<T>
	{
		private readonly Func<T, bool> _responsibilityFunc;

		protected CorHandlerCool(CorHandler<T> nextLink)
			: base(nextLink)
		{
		}

		protected CorHandlerCool(CorHandler<T> nextLink, Func<T, bool> responsibilityFunc)
			: base(nextLink)
		{
			_responsibilityFunc = responsibilityFunc;
		}

		protected override bool IsResponsible(T data)
		{
			if (null != _responsibilityFunc)
			{
				return _responsibilityFunc(data);
			}
			else
			{
				return IsResponsibleCore(data);
			}
		}

		protected abstract bool IsResponsibleCore(T data);
	}

	public class CorHandlerCoolImpl : CorHandlerCool<IData>
	{
		//public CorHandlerCoolImpl(CorHandler<IData> nextLink)
		//    : base(nextLink)
		//{
		//}

		public CorHandlerCoolImpl(CorHandler<IData> nextLink, Func<IData, bool> responsibilityFunc)
			: base(nextLink, responsibilityFunc)
		{
		}

		protected override void ProcessCore(IData data)
		{
			Console.WriteLine(data);
		}

		protected override bool IsResponsibleCore(IData data)
		{
			throw new ApplicationException("Data: " + data);
		}
	}
}
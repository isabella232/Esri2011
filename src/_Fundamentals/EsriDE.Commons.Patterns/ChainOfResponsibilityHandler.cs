using System;
using System.Diagnostics;
using System.Reflection;

namespace EsriDE.Commons.Patterns
{
	public abstract class ChainOfResponsibilityHandler<T> : IChainOfResponsibilityHandler<T>
	{
		protected ChainOfResponsibilityHandler(IChainOfResponsibilityHandler<T> successor)
		{
			Successor = successor;
		}

		public void Process(T data)
		{
			if (IsResponsible(data))
			{
				ProcessCore(data);
			}
			else
			{
				CallNextHandler(data);
			}
		}

		private void CallNextHandler(T data)
		{
			if (null != Successor)
			{
				Successor.Process(data);
			}
			else
			{
				ProcessEndOfChain(data);
			}
		}

		protected virtual void ProcessEndOfChain(T data)
		{
			string message = string.Format("End from chain of responsibility reached. {0}Type info='{1}'",
										   Environment.NewLine, MethodBase.GetCurrentMethod().DeclaringType);
			Trace.WriteLine(message);
		}

		public IChainOfResponsibilityHandler<T> Successor { get; set; }

		protected abstract bool IsResponsible(T data);
		protected abstract void ProcessCore(T data);
	}
}
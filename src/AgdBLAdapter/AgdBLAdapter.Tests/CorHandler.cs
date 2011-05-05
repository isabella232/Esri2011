using System;
using System.Diagnostics;
using System.Reflection;

namespace AgdBLAdapter.Tests
{
	public abstract class CorHandler<T> : ICorHandler<T>
	{
		protected CorHandler(ICorHandler<T> nextLink)
		{
			NextLink = nextLink;
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
			if (null != NextLink)
			{
				NextLink.Process(data);
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

		public ICorHandler<T> NextLink { get; set; }

		protected abstract bool IsResponsible(T data);
		protected abstract void ProcessCore(T data);
	}
}
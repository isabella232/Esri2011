using System;
using System.Windows.Threading;
using PostSharp.Aspects;

namespace EsriDE.Commons.Aop
{
	[Serializable]
	public class GuiDispatchingAspectAttribute : MethodInterceptionAspect
	{
		public DispatcherPriority Priority { get; set; }

		public override void OnInvoke(
			MethodInterceptionArgs eventArgs)
		{
			DispatcherObject dispatcherObject =
				(DispatcherObject)eventArgs.Instance;

			if (dispatcherObject.CheckAccess())
			{
				// We are already in the GUI thread. Proceed.
				eventArgs.Proceed();
			}
			else
			{
				// Invoke the target method synchronously.  
				dispatcherObject.Dispatcher.Invoke(this.Priority, new Action(eventArgs.Proceed));
			}
		}
	}
}
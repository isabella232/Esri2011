using System;
using System.Diagnostics;
using System.Windows.Threading;
using PostSharp.Aspects;

namespace EsriDE.Commons.Aop
{
	[Serializable]
	public class GuiDispatchingAspectAttribute : MethodInterceptionAspect
	{
		public DispatcherPriority Priority { get; set; }

		public override void OnInvoke(MethodInterceptionArgs eventArgs)
		{
			var dispatcherObject = (DispatcherObject) eventArgs.Instance;

			if (dispatcherObject.CheckAccess())
			{
				// We are already in the GUI thread. Proceed.
				Debug.WriteLine("Must not dispatch.");
				eventArgs.Proceed();
			}
			else
			{
				// Invoke the target method synchronously. 
 				Debug.WriteLine("Must dispatch.");
				dispatcherObject.Dispatcher.Invoke(Priority, new Action(eventArgs.Proceed));
			}
		}
	}
}
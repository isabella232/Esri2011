using System;
using System.Diagnostics;
using PostSharp.Laos;

namespace EsriDE.Commons.Aop
{
	[Serializable]
	public class FullLogAspectAttribute : OnMethodBoundaryAspect
	{
		public override void OnEntry(MethodExecutionEventArgs eventArgs)
		{
			WriteDebugMethodInfos("--- Calling: {0} ---", eventArgs.Method.Name);
			WriteDebugArgumentInfos(eventArgs);
		}

		public override void OnExit(MethodExecutionEventArgs eventArgs)
		{
			WriteDebugMethodInfos("--- Leaving: {0} ---", eventArgs.Method.Name);
		}

		public override void OnSuccess(MethodExecutionEventArgs eventArgs)
		{
			WriteDebugMethodInfos("--- Success: {0} ---", eventArgs.Method.Name);
		}

		public override void OnException(MethodExecutionEventArgs eventArgs)
		{
			WriteDebugMethodInfos("--- Exception: {0} @ {1} ---", eventArgs.Exception.Message, eventArgs.Method.Name);
		}

		[Conditional("DEBUG")]
		private static void WriteDebugArgumentInfos(MethodExecutionEventArgs eventArgs)
		{
			object[] arguments = eventArgs.GetReadOnlyArgumentArray();
			foreach (object argument in arguments)
			{
				WriteDebugMethodInfos("--- Argument: {0} ---", argument.ToString());
			}
		}

		[Conditional("DEBUG")]
		private static void WriteDebugMethodInfos(string unformattedMessage, params string[] args)
		{
			string formattedMessage = string.Format(unformattedMessage, args);
			Debug.WriteLine(formattedMessage);
		}
	}
}
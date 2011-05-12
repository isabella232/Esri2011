using System;
using System.Threading;
using EsriDE.Samples.ContentFinder.ContentAdapter.Contract;
using EsriDE.Samples.ContentFinder.DomainModel;

namespace EsriDE.Samples.ContentFinder.ContentAdapter
{
	public abstract class ContentLocator : IContentLocator
	{
		protected enum RunningState
		{
			Stopped,
			Running
		}

		protected RunningState ActualRunningState { get; private set; }

		protected ContentLocator(SourceBundle sourceBundle)
		{
			SourceBundle = sourceBundle;
			ActualRunningState = RunningState.Stopped;
		}

		public void StartSearch()
		{
			ActualRunningState = RunningState.Running;
			var thread = CreateThread();
			thread.Start();
		}

		public void StopSearch()
		{
			ActualRunningState = RunningState.Stopped;
		}

		public event Action<Content> FoundContent;

		public event Action FinishedSearch;

		protected SourceBundle SourceBundle { get; private set; }


		protected abstract void Search();

		protected virtual void OnFoundContent(Content content)
		{
			Console.WriteLine("Content gefunden: " + content.Uri);

			var action = FoundContent;
			if (null != action)
			{
				action(content);
			}
		}

		protected virtual void OnFinishedSearch()
		{
			var action = FinishedSearch;
			if (null != action)
			{
				action();
			}
		}

		// Template Method Pattern: 
		private Thread CreateThread()
		{
			var thread = new Thread(Search);
			ConfigureThread(thread);
			return thread;
		}

		protected virtual void ConfigureThread(Thread thread)
		{
			thread.SetApartmentState(ApartmentState.STA);
			thread.Priority = ThreadPriority.BelowNormal;
		}

	}
}

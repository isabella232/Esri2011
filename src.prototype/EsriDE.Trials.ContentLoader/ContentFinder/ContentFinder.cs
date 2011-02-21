using System;
using System.Collections.Generic;
using System.Threading;
using EsriDE.Trials.ContentLoader.DomainModel;

namespace EsriDE.Trials.ContentLoader.ContentFinder
{
	public abstract class ContentFinder : IContentFinder
	{
		protected RunningState ActualRunningState { get; private set;}

		public ContentFinder(SourceBundle sourceBundle)
		{
			SourceBundle = sourceBundle;
			ActualRunningState = RunningState.Stopped;
		}

		public void StopSearch()
		{
			ActualRunningState = RunningState.Stopped;
		}

		public event Action<Content> FoundContent;
		public event Action FinishedSearch;

		public void StartSearch()
		{
			ActualRunningState = RunningState.Running;
			var thread = CreateThread();
			thread.Start();
		}

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

		protected abstract void Search();

		protected SourceBundle SourceBundle { get; private set; }

		protected virtual void OnFoundContent(Content content)
		{
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

		protected enum RunningState
		{
			Stopped,
			Running
		}
	}
}
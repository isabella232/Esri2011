using System;
using System.Windows;
using System.Windows.Forms;

namespace EsriDE.Trials.EmbeddingManager.Working
{
	public interface IEmbeddingHost<T>
	{
		void EmbeddSomething();
		T GetHost();

		IEmbedderHost EmbedderHost { get; set; }
	}

	public interface IEmbeddingClient<U>
	{
		
	}

	public interface IEmbedderHost
	{
		void EmbeddSomething();
	}

	public interface IEmbedderClient
	{
		void EmbeddSomething();
	}

	public interface ISimpleEmbedder
	{
		void EmbeddSomething();
	}

	public interface IEmbedder<T, U> : ISimpleEmbedder
	{
		void EmbeddSomething(T host, U client);
	}

	public class WinInWpfEmbedder : IEmbedder<UIElement, Control>
	{
		public WinInWpfEmbedder(UIElement u, Control c)
		{
		}

		public void EmbeddSomething(UIElement host, Control client)
		{
		}

		public void EmbeddSomething()
		{
		}
	}

	public class WpfWindow : Window, IEmbeddingHost<UIElement>
	{
		public WpfWindow(IEmbeddingManager embeddingManager)
		{
			
		}

		public void EmbeddSomething()
		{
			
		}

		public UIElement GetHost()
		{
			return this;
		}

		public IEmbedderHost EmbedderHost { get; set; }
	}
}
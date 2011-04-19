using System;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Threading;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NUnit.Framework;

namespace EsriDE.Trials.EmbeddingManager
{
	public interface IHelper
	{
		IEmbeddingManager<T> GetManager<T, U>();
	}

	public class Helper : IHelper
	{
		object GetManager()
		{
			var c = GetControl();

			var manager = new EmbeddingManager<Control, Control>(c, control => control);
			return manager;
		}

		public IEmbeddingManager<T> GetManager<T, U>()
		{
			var c = GetControl();
			object o = new object();

			EmbeddingManager<T, U> m = GetM<T, U>();
			return m as IEmbeddingManager<T>;

			//var manager = new EmbeddingManager<Control, Control>(c, control => control);
			object manager = null;
			return manager as IEmbeddingManager<T>;
		}

		private EmbeddingManager<T, U> GetM<T, U>()
		{
			object o = new object();
			return new EmbeddingManager<T, U>((U)o, null);
		}

		private Control GetControl()
		{
			object o = new object();
			return o as Control;
		}
	}

	[TestFixture]
	public class FixtureEmbeddingWithCastle
	{
		private IWindsorContainer _container;

		#region Setup/Teardown
		[SetUp]
		public void Setup()
		{
			_container = new WindsorContainer();
		}
		#endregion

		private EmbeddingManager<T, U> GetEmbeddingManager<T, U>()
		{
			object o = new object();
			return new EmbeddingManager<T, U>((U)o, null);
		}

		//[Test]
		//public void Do_GetEmbeddingManager()
		//{
		//    _container.Register(Component.For<Func<EmbeddingManager<T, U>>().Instance(() => GetEmbeddingManager<Control, Control>()));
		//}

		[Test]
		[RequiresSTA]
		public void Do()
		{
			var v = _container.Resolve<IEmbeddingManager<Control>>();

			_container.Register(Component.For<Action>().Instance(()  => GetEmbeddingManager<Control, Control>()));
			//_container.Register(Component.For<IEmbeddingManager<>().ImplementedBy<EmbeddingManager<Control, Control>());

			var winFormsUserControl = new WinFormsUserControl();
			var manager = new EmbeddingManager<Control, Control>(winFormsUserControl, control => control);

			var winFormsWindow = new WinFormsWindow(manager);
			winFormsWindow.ShowWindow();
		}


		[Test]
		//[RequiresSTA]
		public void Do_Viewing()
		{
			//System.Windows.Forms.comp
			//System.ComponentModel.Component
			_container.Register(Component.For<IFormView>().ImplementedBy<WinFormsFormView>());
			_container.Register(Component.For<ISomething>().ImplementedBy<MyUserControl>());

			//_container.Register(Component.For<Control, ISomething>().ImplementedBy<MyUserControl>());
			//_container.Register(Component.For<Func<Control, Control>>().Instance(c => c));

			//_container.Register(Component.For<IEmbeddingManager<Control>>().ImplementedBy<WinInWinEmbeddingManager>().LifeStyle.Transient);
			_container.Register(
				Component.For<Func<IEmbeddingManager<Control>>>().Instance(() => new EmbeddingManager<Control, Control>(_container.Resolve<ISomething>() as Control , c => c)));

			//var manager = new EmbeddingManager<Control, Control>(winFormsUserControl, control => control);
			var formView = _container.Resolve<IFormView>();
			formView.ShowView("Blabla");
		}
	}

	internal interface ISomething
	{
		void Do();
	}
	public class MyUserControl : UserControl, ISomething
	{
		public MyUserControl()
		{
			this.BackColor = Color.DeepSkyBlue;
		}

		public void Do()
		{
			Console.WriteLine("MyUserControl.Do");
		}
	}

	interface IFormView
	{
		void ShowView(string text);
		event Action<string> ViewShowed;
	}

	class FakeFormView : IFormView
	{
		private readonly IEmbeddingManager<Control> _embeddingManager;

		//public FakeFormView(IEmbeddingManager<Control> embeddingManager)
		//{
		//    _embeddingManager = embeddingManager;
		//}

		public FakeFormView(Func<IEmbeddingManager<Control>> embeddingManagerFunc)
		{
			_embeddingManager = embeddingManagerFunc();
		}

		public void ShowView(string text)
		{
			Console.WriteLine("FakeFormView.ShowView('" + text + "')");

			_embeddingManager.EmbedControl(EmbedControlHere);

			ViewShowed(text);
		}

		private void EmbedControlHere(Control obj)
		{
			Console.WriteLine("FakeFormView.EmbedControlHere('" + obj + "')");
		}

		public event Action<string> ViewShowed = delegate { };
	}
}
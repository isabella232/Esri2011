using System;
using System.Diagnostics;
using Castle.MicroKernel.Registration;
using EsriDE.Trials.CastleWindsor.LifestyleBehaviour.Contracts;
using EsriDE.Trials.CastleWindsor.LifestyleBehaviour.Implementations;
using NUnit.Framework;

namespace EsriDE.Trials.CastleWindsor.LifestyleBehaviour
{
	[TestFixture]
	public partial class Fixture
	{
		[Test]
		public void ShowTestFormInUsingScope()
		{
			using (var f = new SomeForm())
			{
				f.Show();
			}
		}

		[Test]
		public void Test_WorkingWithForms_WithoutTrulyTransient()
		{
			_container.Register(Component.For<IView>().ImplementedBy<SomeForm>().LifeStyle.Transient);

			OpenAndCloseMultipleTimes();
		}

		[Test]
		public void Test_WorkingWithForms_WithTrulyTransient()
		{
			_container.Kernel.ReleasePolicy = new TrulyTransientReleasePolicy();
			_container.Register(Component.For<IView>().ImplementedBy<SomeForm>().LifeStyle.Custom(typeof(TrulyTransientLifestyleManager)));

			OpenAndCloseMultipleTimes();
		}

		private void OpenAndCloseMultipleTimes()
		{
			for (int i = 0; i < 10; i++)
			{
				Trace.WriteLine("10");
				OpenAndCloseFormMultipleTimes(10);
				Trace.WriteLine("100");
				OpenAndCloseFormMultipleTimes(100);
			}

			while (!_fertig1 && !_fertig2);
		}

		private void OpenAndCloseFormMultipleTimes(int count)
		{
			var veryFirst = GC.GetTotalMemory(true);

			int i = 0;
			do
			{
				i++;
				var before = GC.GetTotalMemory(true);
				OpenAndCloseForm();
				var after = GC.GetTotalMemory(true);

				GC.Collect();
				var afterGC = GC.GetTotalMemory(true);

				//Console.WriteLine("*** before =" + before.ToString());
				//Console.WriteLine("*** after  =" + after.ToString());
				//Console.WriteLine("*** afterGC=" + afterGC.ToString());
			} while (i < count);

			GC.Collect();
			var veryLast = GC.GetTotalMemory(true);

			Trace.WriteLine("*** veryFirst=" + veryFirst);
			Trace.WriteLine("*** veryLast =" + veryLast);
			var diff = veryLast - veryFirst;
			Trace.WriteLine("*** diff     =" + diff);
		}

		private void OpenAndCloseForm()
		{
			_fertig1 = false;
			_fertig2 = false;
			OpenAndCloseFormInUsingScope();
			OpenAndCloseFormWithoutUsingScope();
		}

		private void OpenAndCloseFormWithoutUsingScope()
		{
			Console.WriteLine("********* Nicht-Using Variante *********");
			var f = _container.Resolve<IView>();
			Assert.That(f, !Is.Null, "Could not resolve IView");

			f.ShowView(_ => _fertig1 = _);
			f.CloseView();
		}

		private void OpenAndCloseFormInUsingScope()
		{
			Console.WriteLine("********* Using Variante *********");
			using (var f2 = (IDisposable)_container.Resolve<IView>())
			{
				Assert.That(f2, !Is.Null, "Could not resolve IView");
				var f3 = (IView)f2;

				f3.ShowView(_ => _fertig2 = _);
				f3.CloseView();
			}
		}
	}
}
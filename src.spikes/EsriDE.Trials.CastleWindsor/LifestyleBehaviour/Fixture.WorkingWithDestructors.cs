using System;
using EsriDE.Trials.CastleWindsor.LifestyleBehaviour.Implementations;
using NUnit.Framework;

namespace EsriDE.Trials.CastleWindsor.LifestyleBehaviour
{
	[TestFixture]
	public partial class Fixture
	{
		[Test]
		[Timeout(5000)]
		public void DestructorIsCalledAfter5Seconds()
		{
			DestructorIsCalledAfter3Seconds_DoSomething();
			GC.Collect();

			while (!_fertig1);
		}

		[Test]
		public void DestructorIsCalledAfter3Seconds()
		{
			DestructorIsCalledAfter3Seconds_DoSomething();
			GC.Collect();

			Assert.That(_fertig1, Is.EqualTo(false).After(3000, 50));
		}

		private void DestructorIsCalledAfter3Seconds_DoSomething()
		{
			var b = new B();
			Console.WriteLine("b is created. b=" + b);
			b.SetAction(_ => _fertig1 = _);
		}

		[Test]
		public void DestructorIsNotCalledAfter3SecondsIfTimerRuns()
		{
			DestructorIsNotCalledAfter3SecondsIfTimerRuns_DoSomething();
			GC.Collect();

			Assert.That(_fertig1, Is.EqualTo(false).After(3000, 50));
		}

		private void DestructorIsNotCalledAfter3SecondsIfTimerRuns_DoSomething()
		{
			var b = new B(startWithTimer: true);
			Console.WriteLine("b is created. b=" + b);
			b.SetAction(_ => _fertig1 = _);
		}
	}
}

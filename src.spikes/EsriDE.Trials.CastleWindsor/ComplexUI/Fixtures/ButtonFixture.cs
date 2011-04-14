using System;
using System.Diagnostics;
using EsriDE.Trials.CastleWindsor.ComplexUI.Fixtures.WidgetFakes;
using EsriDE.Trials.CastleWindsor.ComplexUI.Implementations.Buttons;
using NUnit.Framework;

namespace EsriDE.Trials.CastleWindsor.ComplexUI.Fixtures
{
	[TestFixture]
	public class ButtonFixture
	{
		[Test]
		[Explicit]
		public void ClickingButton()
		{
			var sut = new Button();

			for (int i = 0; i < 10; i++)
			{
				var veryFirst = GC.GetTotalMemory(true);
				for (int j = 0; j < 500; j++)
				{
					sut.OnClick();
				}
				GC.Collect();
				var veryLast = GC.GetTotalMemory(true);

				Trace.WriteLine("*** veryFirst=" + veryFirst);
				Trace.WriteLine("*** veryLast =" + veryLast);
				var diff = veryLast - veryFirst;
				Trace.WriteLine("*** diff     =" + diff);
			}
		}
	}
}
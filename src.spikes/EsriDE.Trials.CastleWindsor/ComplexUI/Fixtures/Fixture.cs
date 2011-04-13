using EsriDE.Trials.CastleWindsor.ComplexUI.Fixtures.WidgetFakes;
using EsriDE.Trials.CastleWindsor.ComplexUI.Implementations.Buttons;
using NUnit.Framework;

namespace EsriDE.Trials.CastleWindsor.ComplexUI.Fixtures
{
	[TestFixture]
	public class Fixture
	{
		[Test]
		public void Do()
		{
			var sut = new Button();
			sut.OnClick();
		}

		[Test]
		public void Open()
		{
			var form = new TestFormForButtonHosting();
			form.ShowDialog();
		}
	}
}
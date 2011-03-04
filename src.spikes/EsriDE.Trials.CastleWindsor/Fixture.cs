using NUnit.Framework;

namespace EsriDE.Trials.CastleWindsor
{
	[TestFixture]
	public class Fixture
	{
		[Test]
		public void Do2()
		{
			var sut = new Button();
			sut.OnClick();
		}

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
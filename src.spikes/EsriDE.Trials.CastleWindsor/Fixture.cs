using NUnit.Framework;

namespace EsriDE.Trials.CastleWindsor
{
	[TestFixture]
	public class ButtonFixture
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
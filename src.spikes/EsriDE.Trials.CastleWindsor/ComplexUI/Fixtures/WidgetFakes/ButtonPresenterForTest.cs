using EsriDE.Trials.CastleWindsor.ComplexUI.AA;
using EsriDE.Trials.CastleWindsor.ComplexUI.Buttons;
using EsriDE.Trials.CastleWindsor.ComplexUI.Forms;

namespace EsriDE.Trials.CastleWindsor.ComplexUI.Fixtures.WidgetFakes
{
	public class ButtonPresenterForTest : ButtonPresenter
	{
		public ButtonPresenterForTest(IToggleFormVisibilityModel toggleFormVisibility) : base(toggleFormVisibility)
		{
		}

		public void EmulateEventing(VisibilityState visibilityState)
		{
			SetButtonCheckedState(visibilityState);
		}
	}
}
namespace EsriDE.Trials.CastleWindsor
{
	public class ButtonPresenterForTest : ButtonPresenter
	{
		public ButtonPresenterForTest(IToggleFormVisibilityModel toggleFormVisibility) : base(toggleFormVisibility)
		{
		}

		public void EmulateEventing(Visibility visibility)
		{
			SetButtonCheckedState(visibility);
		}
	}
}
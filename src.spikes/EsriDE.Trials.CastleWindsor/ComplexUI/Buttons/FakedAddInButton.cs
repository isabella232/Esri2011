namespace EsriDE.Trials.CastleWindsor.ComplexUI.Buttons
{
	public abstract class FakedAddInButton
	{
		public bool Enabled { get; protected set; }
		public bool Checked { get; protected set; }

		public abstract void OnClick();
	}
}
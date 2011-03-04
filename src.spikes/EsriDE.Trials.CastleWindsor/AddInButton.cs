namespace EsriDE.Trials.CastleWindsor
{
	public abstract class AddInButton
	{
		public bool Enabled { get; protected set; }
		public bool Checked { get; protected set; }

		public abstract void OnClick();
	}
}
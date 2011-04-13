using EsriDE.Trials.CastleWindsor.ComplexUI.Forms;

namespace EsriDE.Trials.CastleWindsor.ComplexUI.Fixtures
{
	public interface IEventSubscriber
	{
		void Handler(VisibilityState visibilityState);
	}
}
using EsriDE.Trials.CastleWindsor.ComplexUI.Contracts.DomainModel;

namespace EsriDE.Trials.CastleWindsor.ComplexUI.Fixtures
{
	public interface IEventSubscriber
	{
		void Handler(VisibilityState visibilityState);
	}
}
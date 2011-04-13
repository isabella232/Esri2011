using EsriDE.Trials.CastleWindsor.ComplexUI.AA;

namespace EsriDE.Trials.CastleWindsor.ComplexUI.Contracts.Forms
{
	public interface IFormPresenter
	{
		void SetModel(IToggleFormVisibilityModel model);
		void UnsetModel();
	}
}
using Castle.MicroKernel.Lifestyle;

namespace EsriDE.Trials.CastleWindsor.ClientContainer
{
	/// <summary>
	/// a custom Lifestyle, it will ihnerit from the standard class so if the TrulyTransientReleasePolicy policy
	/// isn't used these objects are handled as standard transient objects 
	/// </summary>
	public class TrulyTransientLifestyleManager : TransientLifestyleManager
	{
	}
}
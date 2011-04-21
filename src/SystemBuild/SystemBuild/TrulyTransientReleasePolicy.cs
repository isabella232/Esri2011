using System;
using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Releasers;

namespace EsriDE.Samples.ContentFinder.SystemBuild
{
	/// <summary>
	///   Inherits from the default ReleasePolicy; do not track our own transient objects.
	///   Only tracks components that have decommission steps
	///   registered or have pooled lifestyle.
	/// </summary>
	[Serializable]
	public class TrulyTransientReleasePolicy : LifecycledComponentsReleasePolicy
	{
		public override void Track(object instance, Burden burden)
		{
			ComponentModel model = burden.Model;

			// to modify the way Castle handles the Transient object uncomment the following lines
			//if (model.LifestyleType == LifestyleType.Transient)
			//   return;

			// we skip the tracking for object marked with our custom Transient lifestyle manager
			if ((model.LifestyleType == LifestyleType.Custom) &&
			    (typeof (TrulyTransientLifestyleManager) == model.CustomLifestyle))
				return;

			base.Track(instance, burden);
		}
	}
}
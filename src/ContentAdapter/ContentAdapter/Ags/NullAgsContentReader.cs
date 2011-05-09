using System;
using System.Drawing;

namespace EsriDE.Samples.ContentFinder.ContentAdapter.Ags
{
	public class NullAgsContentReader : AgsContentReader
	{
		public override AgsContent ReadContent(Uri uri)
		{
			return new AgsContent("Unhandable Service", new Bitmap(1, 1), uri);
		}
	}
}
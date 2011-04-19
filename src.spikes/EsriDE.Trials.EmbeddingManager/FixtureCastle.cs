using System;
using Castle.Core.Logging;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NUnit.Framework;

namespace EsriDE.Trials.EmbeddingManager
{
	[TestFixture]
	public partial class FixtureCastle
	{
		private class Something
		{
			private readonly int i;

			public Something(Func<int> myFunc)
			{
				i = myFunc();
			}

			public int I
			{
				get { return i; }
			}
		}
	}
}
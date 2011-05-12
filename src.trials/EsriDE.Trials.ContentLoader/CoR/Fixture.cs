using EsriDE.Trials.ContentLoader.DomainModel;
using NUnit.Framework;

namespace EsriDE.Trials.ContentLoader.CoR
{
	[TestFixture]
	public class Fixture
	{
		[Test]
		public void Do()
		{
			IResolver nullResolver = new UnknownResolver();
			IResolver agsResolver = new AgsResolver(nullResolver, new AgsFactory());
			IResolver resolver = new MxdResolver(agsResolver, new MxdFactory());

			IFactory factory = resolver.Resolve(new MxdContent("MxdContent", null, null, string.Empty, string.Empty));
			Assert.That(factory, !Is.Null);
			Assert.That(factory.GetType(), Is.EqualTo(typeof(MxdFactory)));

			factory = resolver.Resolve(new AgsContent("AgsContent", null, null));
			Assert.That(factory, !Is.Null);
			Assert.That(factory.GetType(), Is.EqualTo(typeof(AgsFactory)));

			factory = resolver.Resolve(new UnknownContent());
			Assert.That(factory, !Is.Null);
			Assert.That(factory.GetType(), Is.EqualTo(typeof(UnknownFactory)));
		}

		private class UnknownContent : Content
		{
			public UnknownContent()
				: base(string.Empty, null, null)
			{ }
		}
	}
}
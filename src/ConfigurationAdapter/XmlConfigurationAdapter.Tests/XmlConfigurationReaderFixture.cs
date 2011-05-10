using System;
using NUnit.Framework;

//http://www.codeproject.com/KB/linq/LINQtoXML.aspx
namespace EsriDE.Samples.ContentFinder.XmlConfigurationAdapter.Tests
{
	// ReSharper disable InconsistentNaming

	[TestFixture]
	public class XmlConfigurationReaderFixture
	{
		private XmlConfigurationReader _sut;
		private readonly Uri _uri = new Uri("http://nothing.to.do");

		[SetUp]
		public void Setup()
		{
			_sut = new XmlConfigurationReader();
		}

		[Test]
		public void ReadingConfiguration_WithMeaningfulUri_ThrowsApplicationException()
		{
			// entweder
			var ex = Assert.Throws<ApplicationException>(() => _sut.ReadConfiguration(_uri));
			Assert.That(ex.Message,
				Is.EqualTo("Lesen mit individueller Uri ist noch nicht implementiert."));

			// oder
			Assert.That(() => _sut.ReadConfiguration(_uri),
				Throws.TypeOf<ApplicationException>().With.Property("Message").EqualTo("Lesen mit individueller Uri ist noch nicht implementiert."));
		}

		[Test]
		public void ReadingConfiguration_WithNullUri_DoesNotThrowException()
		{
			Assert.DoesNotThrow(() => _sut.ReadConfiguration(null));
		}
	}

	// ReSharper restore InconsistentNaming
}
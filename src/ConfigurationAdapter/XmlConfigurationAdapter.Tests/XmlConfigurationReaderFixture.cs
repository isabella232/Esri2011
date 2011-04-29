using System;
using System.Linq;
using System.Xml.Linq;
using EsriDE.Samples.ContentFinder.DomainModel;
using NUnit.Framework;

//http://www.codeproject.com/KB/linq/LINQtoXML.aspx
namespace EsriDE.Samples.ContentFinder.XmlConfigurationAdapter.Tests
{
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
		public void ReadingConfiguration_WithMeaningfulUri_ThrowsException()
		{
			//var ex = Assert.Throws<Exception>(() => _sut.ReadConfiguration(_uri));
			//Assert.That(ex.Message,
			//    Is.EqualTo("Lesen mit individueller _uri ist noch nicht implementiert."));

			//Assert.Throws<NotImplementedException>(_sut.ReadConfiguration(_uri));

			Assert.That(() => _sut.ReadConfiguration(_uri),
				Throws.TypeOf<NotImplementedException>().With.Property("Message").EqualTo("Lesen mit individueller _uri ist noch nicht implementiert."));
		}

		[Test]
		public void ReadingConfiguration_WithNullUri_DoesNotThrowException()
		{
			Assert.DoesNotThrow(
				() => _sut.ReadConfiguration(_uri));

		}
	}
}
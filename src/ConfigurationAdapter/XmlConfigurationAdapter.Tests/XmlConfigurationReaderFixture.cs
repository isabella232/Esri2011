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

		[Test]
		public void Spike()
		{
			

			foreach (var sourceBundle in r)
			{
				Console.WriteLine("Type=" + sourceBundle.Type);
				foreach (var source in sourceBundle.Sources)
				{
					Console.WriteLine("Uri=" + source.Uri);
				}
			}

			//Enum.Parse(typeof (RecursivityPolicy), "true");
			//{
			//    ID = Convert.ToInt32(c.Attribute("ID").Value),
			//    Forename = c.Element("Forename").Value,
			//    Surname = c.Element("Surname").Value,
			//    DOB = c.Element("DOB").Value,
			//    Location = c.Element("Location").Value

			//}).FirstOrDefault();

			//        XElement sourceBundles = new XElement("FilteringByCategory",
			//from category in rss.Element("channel").Elements("item")
			//where (string)category.Element("category") == "C Sharp"
			//select new XElement("item",
			//    category.Elements()
			//    )
			//);

			//        var q = from c in xmlSource.contact
			//                where c.contactId < 4
			//                select c.firstName + " " + c.lastName;

			//        XElement filterCategory =
			//new XElement("FilteringByCategory",
			//from category in rss.Element("channel").Elements("item")
			//where (string)category.Element("category") == "C Sharp"
			//select new XElement("item",
			//    category.Elements()
			//    )
			//);

			//        foreach (string name in q)
			//            Console.WriteLine("Customer name = {0}", name);
		}
	}
}
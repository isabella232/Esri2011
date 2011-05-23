using System;
using System.Diagnostics;
using EsriDE.Samples.ContentFinder.BL.Contract;
using EsriDE.Samples.ContentFinder.DomainModel;

namespace EsriDE.Samples.ContentFinder.App
{
	public class SimpleWriterContentProcessorAdapter : IContentProcessorAdapter
	{
		public void Process(Content content)
		{
			Trace.WriteLine(content.Uri);
		}
	}
}
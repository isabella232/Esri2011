using System;
using EsriDE.Samples.ContentFinder.DomainModel;

namespace EsriDE.Samples.ContentFinder.WpfUI
{
	public interface IContentProvider
	{
		event Action<Content> NewContent;
	}
}
using System;
using EsriDE.Samples.ContentFinder.DomainModel;

namespace EsriDE.Samples.ContentFinder.UI.Contract
{
	public interface IPortal
	{
		event Action<Content> ContentSelected;
	}
}
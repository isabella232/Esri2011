using System;
using EsriDE.Samples.ContentFinder.DomainModel;

namespace EsriDE.Samples.ContentFinder.BL.Contract
{
	public interface IController
	{
		void Start();
		void Stop();

		event Action SearchComplete;
		event Action<Content> ContentFound;
	}
}
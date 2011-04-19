using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Windows.Threading;
using EsriDE.Samples.ContentFinder.DomainModel;

namespace EsriDE.Samples.ContentLoader.UI.Wpf
{
	public class ObservableCollectionEx<T> : ObservableCollection<T> where T : Content
	{
		//// Override the event so this class can access it
		public override event NotifyCollectionChangedEventHandler CollectionChanged;

		protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
		{
			try
			{
				// Be nice - use BlockReentrancy like MSDN said
				using (BlockReentrancy())
				{
					NotifyCollectionChangedEventHandler eventHandler = CollectionChanged;
					if (eventHandler == null)
						return;

					var delegates = eventHandler.GetInvocationList();
					foreach (NotifyCollectionChangedEventHandler handler in delegates)
					{
						var dispatcherObject = handler.Target as DispatcherObject;

						// If the subscriber is a DispatcherObject and different thread
						if (dispatcherObject != null && dispatcherObject.CheckAccess() == false)
						{
							// Invoke handler in the target dispatcher's thread
							dispatcherObject.Dispatcher.Invoke(DispatcherPriority.DataBind, handler, this, e);
						}
						else // Execute handler as is
							handler(this, e);
					}
				}
			}
			catch (Exception exception)
			{
				Debug.WriteLine(exception);
			}
		}

		//public bool TryGetContentItem(Guid guid, out T contentItem)
		//{
		//    try
		//    {
		//        contentItem = this.Single(x => x.Id == guid);
		//        return true;
		//    }
		//    catch (InvalidOperationException)
		//    {
		//        contentItem = default(T);
		//        return false;
		//    }
		//}
	}
}
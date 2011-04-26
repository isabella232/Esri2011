using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
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

		public bool TryGetContentItem(Uri uri, out Content content)
		{
			try
			{
				//content = this.Single(x => x.Uri.ToString().Equals(uri.ToString()));
				content = this.Single(x => 0 == Uri.Compare(x.Uri, uri,
					UriComponents.AbsoluteUri, UriFormat.Unescaped, StringComparison.CurrentCultureIgnoreCase));
				return true;
			}
			catch (InvalidOperationException)
			{
				content = default(Content);
				return false;
			}
		}

		
	}
}
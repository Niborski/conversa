using System.Collections.Generic;

namespace Conversa.Collections
{
	public sealed class CollectionChangedEventData<T>
	{
		/// Gets the action that caused the event.
		public CollectionChangedAction Action
		{
			get;
			private set;
		}
		
		/// Gets the list of new items involved in the change.
		public IList<T> NewItems
		{
			get;
			private set;
		}
		
		/// Gets the list of items affected by a Replace, Remove, or Move action.
		public IList<T> OldItems
		{
			get;
			private set;
		}
		
		public CollectionChangedEventData(CollectionChangedAction action, T item)
		{
			this.Action = action;

			if (action == CollectionChangedAction.Add)
			{
				this.NewItems = new List<T> { item };
			}
			else if (action != CollectionChangedAction.Reset)
			{
				this.OldItems = new List<T> { item };
			}
		} 
				
		public CollectionChangedEventData(CollectionChangedAction action
										, IList<T> 				  newItems = null
										, IList<T>				  oldItems = null)		
		{
			this.Action   = action;
			this.NewItems = newItems;
			this.OldItems = OldItems;
		} 
	}
}
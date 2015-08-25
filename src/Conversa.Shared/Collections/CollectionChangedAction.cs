// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Collections
{
    /// Describes the action that caused a collection change.
    public enum CollectionChangedAction
	{
		/// An item was added to the collection.
		Add
		/// An item was moved within the collection.
	  , Move
		/// An item was removed from the collection.
	  , Remove
		/// An item was replaced in the collection.
	  , Replace
		/// The content of the collection was cleared.
	  , Reset		
	}
}

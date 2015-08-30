// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.InstantMessaging
{
    /// <summary>
    /// Specifies the type of change that occurred for a ChatMessageStoreChanged event.
    /// </summary>
    public enum ChatStoreChangedEventKind  : int
    {
        /// <summary>
        /// Notifications have been missed.
        /// </summary>
        NotificationsMissed  = 0	
        /// <summary>
        /// The chat store has been modified.
        /// </summary>
      , StoreModified = 1
        /// <summary>
        /// A chat message has been created.
        /// </summary>
      , MessageCreated = 2
        /// <summary>
        /// A chat message has been changed.
        /// </summary>
      , MessageModified = 3	
        /// <summary>
        /// A chat message has been deleted.
        /// </summary>
      , MessageDeleted = 4	
        /// <summary>
        /// A chat conversation has been modified.
        /// </summary>
      , ConversationModified = 5	
        /// <summary>
        /// A chat conversation has been deleted.
        /// </summary>
      , ConversationDeleted = 6	
    }
}

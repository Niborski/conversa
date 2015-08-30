// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.InstantMessaging
{
    /// <summary>
    /// Provides the data for the ChatMessageStoreChanged event.
    /// </summary>
    public sealed class ChatMessageStoreChangedEventData
    {
        /// <summary>
        /// The ID of the object that changed.
        /// </summary>
        public string Id
        {
            get;
        }

        /// <summary>
        /// The type of change that happened.
        /// </summary>
        public ChatStoreChangedEventKind Kind
        {
            get;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatMessageStoreChangedEventData"/> class.
        /// </summary>
        /// <param name="id">The ID of the object that changed.</param>
        /// <param name="kind">The type of change that happened.</param>
        internal ChatMessageStoreChangedEventData(string id, ChatStoreChangedEventKind kind)
        {
            this.Id   = id;
            this.Kind = kind;
        }
    }
}

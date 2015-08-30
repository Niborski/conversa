// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Foundation.Metadata;

namespace Conversa.Net.Xmpp.InstantMessaging
{
    /// <summary>
    /// Provides functionality for reading batches of conversations from the ChatMessageStore.
    /// </summary>
    public sealed class ChatConversationReader
    {
        private string conversationId;

        /// <summary>
        /// Asynchronously reads batches of conversations from the ChatMessageStore.
        /// </summary>
        /// <returns>The list of conversations.</returns>
        [RemoteAsync]
        public async Task<IReadOnlyList<ChatConversation>> ReadBatchAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchronously reads batches of conversations from the ChatMessageStore.
        /// </summary>
        /// <param name="count">Specifies the size of the batch to read.</param>
        /// <returns>The list of conversations.</returns>
        [RemoteAsync]
        public async Task<IReadOnlyList<ChatConversation>> ReadBatchAsync(int count)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatConversationReader"/> class.
        /// </summary>
        internal ChatConversationReader()
        {
        }
    }
}

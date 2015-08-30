// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.DataStore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Conversa.Net.Xmpp.InstantMessaging
{
    /// <summary>
    /// Provides methods for reading messages from the message store.
    /// </summary>
    public sealed class ChatMessageReader
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChatMessageReader"/> class.
        /// </summary>
        internal ChatMessageReader()
        {
        }

        /// <summary>
        /// Returns a batch list of chat messages from the message store.
        /// </summary>
        /// <returns>An asynchronous operation that returns a list of chat messages upon successful completion.</returns>
        public async Task<IReadOnlyList<ChatMessage>> ReadBatchAsync()
        {
            return await ReadBatchAsync(10).ConfigureAwait(false);
        }

        /// <summary>
        /// Returns a batch list of chat messages from the message store limited to the specified size.
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<IReadOnlyList<ChatMessage>> ReadBatchAsync(int count)
        {
            return await DataSource<ChatMessage>.ReadBatchAsync(count).ConfigureAwait(false);
        }
    }
}

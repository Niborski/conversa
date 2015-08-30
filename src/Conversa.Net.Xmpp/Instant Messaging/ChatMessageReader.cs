// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.DataStore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Conversa.Net.Xmpp.InstantMessaging
{
    /// <summary>
    /// Provides methods for reading messages from the message store.
    /// </summary>
    public sealed class ChatMessageReader
    {
        internal ChatMessageReader()
        {
        }

        /// <summary>
        /// Retrieves a message specified by an identifier from the message store.
        /// </summary>
        /// <param name="localChatMessageId"></param>
        /// <returns></returns>
        public async Task<ChatMessage> GetMessageAsync(string localChatMessageId)
        {
            return await DataSource<ChatMessage>.FirstOrDefaultAsync(m => m.Id == localChatMessageId).ConfigureAwait(false);
        }

        public async Task<IReadOnlyList<ChatMessage>> ReadBatchAsync(Expression<Func<ChatMessage, bool>> predicate = null)
        {
            return await ReadBatchAsync(10, predicate).ConfigureAwait(false);
        }

        public async Task<IReadOnlyList<ChatMessage>> ReadBatchAsync(int count, Expression<Func<ChatMessage, bool>> predicate = null)
        {
            return await DataSource<ChatMessage>.ReadBatchAsync(count, predicate).ConfigureAwait(false);
        }
    }
}

// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Conversa.Net.Xmpp.InstantMessaging
{
    /// <summary>
    /// Provides functionality to search for chat messages in the ChatMessageStore.
    /// </summary>
    public sealed class ChatSearchReader
    {
        private ChatQueryOptions searchCriteria;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatSearchReader"/> class.
        /// </summary>
        internal ChatSearchReader(ChatQueryOptions searchCriteria)
        {
            this.searchCriteria = searchCriteria;
        }

        /// <summary>
        /// Returns a batch of found items matching the search criteria.
        /// </summary>
        /// <returns>A list of items matching the search criteria.</returns>
        public async Task<IReadOnlyList<ChatMessage>> ReadBatchAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a batch of found items matching the search criteria.
        /// </summary>
        /// <param name="count">The maximum number of items to return.</param>
        /// <returns>A list of items matching the search criteria.</returns>
        public async Task<IReadOnlyList<ChatMessage>> ReadBatchAsync(int count)
        {
            throw new NotImplementedException();
        }
    }
}

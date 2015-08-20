using System;
using System.Collections.Generic;
using Windows.Foundation;

namespace Conversa.Net.Xmpp.InstantMessaging
{
    /// <summary>
    /// Provides functionality to search for chat messages in the ChatMessageStore.
    /// </summary>
    public sealed class ChatSearchReader
    {
        /// <summary>
        /// Returns a batch of found items matching the search criteria.
        /// </summary>
        /// <returns>A list of items matching the search criteria.</returns>
        public IAsyncOperation<IReadOnlyList<ChatMessage>> ReadBatchAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a batch of found items matching the search criteria.
        /// </summary>
        /// <param name="count">The maximum number of items to return.</param>
        /// <returns>A list of items matching the search criteria.</returns>
        public IAsyncOperation<IReadOnlyList<ChatMessage>> ReadBatchAsync(int count)
        {
            throw new NotImplementedException();
        }
    }
}

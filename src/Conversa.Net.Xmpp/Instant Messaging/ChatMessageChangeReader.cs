using System;
using System.Collections.Generic;
using Windows.Foundation;

namespace Conversa.Net.Xmpp.InstantMessaging
{
    /// <summary>
    /// Provides methods for reading and accepting message change revisions.
    /// </summary>
    public sealed class ChatMessageChangeReader
    {
        /// <summary>
        /// Accepts all the changes up to and including the latest change to the message.
        /// </summary>
        public void AcceptChanges()
        {
        }

        /// <summary>
        /// Accepts all the changes up to a specified change.
        /// </summary>
        /// <param name="lastChangeToAcknowledge">The last change to acknowledge.</param>
        public void AcceptChangesThrough(ChatMessageChange lastChangeToAcknowledge)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a batch list of chat message change objects from the message store’s change tracker.
        /// </summary>
        /// <returns>An asynchronous operation that returns a list of changes.</returns>
        public IAsyncOperation<IReadOnlyList<ChatMessageChange>> ReadBatchAsync()
        {
            throw new NotImplementedException();
        }
    }
}

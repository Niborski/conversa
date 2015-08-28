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
            return await DataSource<ChatMessage>
                .Query()
                .Where(m => m.Id == localChatMessageId)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
        }

        public async Task<IReadOnlyList<ChatMessage>> ReadBatchAsync()
        {
            return await ReadBatchAsync(10).ConfigureAwait(false);
        }

        public async Task<IReadOnlyList<ChatMessage>> ReadBatchAsync(int count)
        {
            return await DataSource<ChatMessage>
                .Query()
                .OrderByDescending(m => m.LocalTimestamp)
                .Take(count)
                .ToListAsync()
                .ConfigureAwait(false);
        }
    }
}

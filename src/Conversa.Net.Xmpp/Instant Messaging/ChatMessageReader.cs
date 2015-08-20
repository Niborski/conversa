using System;
using System.Collections.Generic;
using Windows.Foundation;

namespace Conversa.Net.Xmpp.InstantMessaging
{
    public sealed class ChatMessageReader
    {
        public IAsyncOperation<IReadOnlyList<ChatMessage>> ReadBatchAsync()
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IReadOnlyList<ChatMessage>> ReadBatchAsync(int count)
        {
            throw new NotImplementedException();
        }
    }
}

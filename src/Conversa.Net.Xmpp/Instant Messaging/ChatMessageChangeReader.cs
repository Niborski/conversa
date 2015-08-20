using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace Conversa.Net.Xmpp.InstantMessaging
{
    public sealed class ChatMessageChangeReader
    {
        public void AcceptChanges()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lastChangeToAcknowledge">The last change to acknowledge.</param>
        public void AcceptChangesThrough(ChatMessageChange lastChangeToAcknowledge)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>An asynchronous operation that returns a list of changes.</returns>
        public IAsyncOperation<IReadOnlyList<ChatMessageChange>> ReadBatchAsync()
        {
            throw new NotImplementedException();
        }
    }
}

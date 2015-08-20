using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conversa.Net.Xmpp.InstantMessaging
{
    public sealed class ChatMessageChange
    {
        /// <summary>
        /// Gets the type of change represented by the object.
        /// </summary>
        public ChatMessageChangeType ChangeType
        {
            get;
        }
    }
}

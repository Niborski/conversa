using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conversa.Net.Xmpp.InstantMessaging
{
    public sealed class ChatMessageChangedEventArgs
        : EventArgs
    {
        public ChatMessageChangedDeferral GetDeferral()
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conversa.Net.Xmpp.InstantMessaging
{
    public enum ChatMessageChangeType : int
    {
        MessageCreated     = 0	
      , MessageModified    = 1	
      , MessageDeleted     = 2	
      , ChangeTrackingLost = 3	
    }
}

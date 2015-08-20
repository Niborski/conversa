using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conversa.Net.Xmpp.InstantMessaging
{
    public enum ChatMessageValidationStatus : int
    {
        Valid                   = 0
      , NoRecipients            = 1        
      , InvalidData             = 2
      , MessageTooLarge         = 3	
      , TooManyRecipients       = 4	
      , TransportInactive       = 5	
      , TransportNotFound       = 6	
      , TooManyAttachments      = 7	
      , InvalidRecipients       = 8	
      , InvalidBody             = 9	
      , InvalidOther            = 10	
      , ValidWithLargeMessage   = 11
    }
}

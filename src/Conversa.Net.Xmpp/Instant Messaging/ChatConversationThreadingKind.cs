using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conversa.Net.Xmpp.InstantMessaging
{
    /// <summary>
    /// Specifies how a conversation is threaded.
    /// </summary>
    public enum ChatConversationThreadingKind : int
    {
        /// <summary>
        /// By participants
        /// </summary>
        Participants = 0	
        /// <summary>
        /// By contact ID
        /// </summary>
      , ContactId  = 1	
        /// <summary>
        /// By conversation ID
        /// </summary>        
      , ConversationId = 2	        
        /// <summary>
        /// Custom threading defined by the app
        /// </summary>        
      , Custom = 3	        
    }
}

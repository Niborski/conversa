// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

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

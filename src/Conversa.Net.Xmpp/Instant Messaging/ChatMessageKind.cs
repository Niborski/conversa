// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.InstantMessaging
{
    /// <summary>
    /// Specifies the type of chat message.
    /// </summary>
    public enum ChatMessageKind : int
    {
        /// <summary>
        /// A standard chat message
        /// </summary>
        Standard = 0	
        /// <summary>
        /// A file transfer request
        /// </summary>
      , FileTransferRequest = 1	
        /// <summary>
        /// A non-SMS/MMS message written to the device by the app
        /// </summary>
      , TransportCustom = 2	        
        /// <summary>
        /// A conversation the user joined
        /// </summary>
      , JoinedConversation = 3	
        /// <summary>
        /// A conversation the user left
        /// </summary>
      , LeftConversation = 4	
        /// <summary>
        /// A conversation that another user joined
        /// </summary>
      , OtherParticipantJoinedConversation  = 5	
        /// <summary>
        /// A conversation that another user left
        /// </summary>
      , OtherParticipantLeftConversation = 6
    }
}

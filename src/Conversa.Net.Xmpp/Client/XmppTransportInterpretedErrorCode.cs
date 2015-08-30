// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.InstantMessaging
{
    /// <summary>
    /// Specifies an interpretation for the error code.
    /// </summary>
    public enum XmppTransportInterpretedErrorCode
    {
        /// <summary>
        /// There was no error.
        /// </summary>
        None = 0	
        /// <summary>
        /// There is no interpretation for the error code.
        /// </summary>
      , Unknown = 1	
        /// <summary>
        /// An invalid recipient address
        /// </summary>
      , InvalidRecipientAddress = 2	
        /// <summary>
        /// A network connectivity error
        /// </summary>
      , NetworkConnectivity = 3	
        /// <summary>
        /// A service denied error
        /// </summary>
      , ServiceDenied = 4	
        /// <summary>
        /// A timeout error
        /// </summary>
      , Timeout = 5	        
    }
}

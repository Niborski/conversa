// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.InstantMessaging
{
    /// <summary>
    /// Specifies the category of a transport error.
    /// </summary>
    public enum XmppTransportErrorCodeCategory : int
    {
        /// <summary>
        /// No specific category for the error code
        /// </summary>
        None = 0	
        /// <summary>
        /// An HTTP error
        /// </summary>
      , Http = 1	
        /// <summary>
        /// Can't connect to the network
        /// </summary>
      , Network = 2	
        /// <summary>
        /// An MMS server error
        /// </summary>
      , MmsServer = 3
    }
}

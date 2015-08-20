// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Client
{
    /// <summary>
    /// Describes the current state of the connection to a XMPP Server.
    /// </summary>
    public enum XmppTransportState
    {
        /// <summary>
        /// The connection is being opened
        /// </summary>
        Opening,

        /// <summary>
        /// The connection is open and the authentication and resource binding has been done correctly.
        /// </summary>
        Open,

        /// <summary>
        /// The connection is starting the authentication process
        /// </summary>
        Authenticating,

        /// <summary>
        /// The authentication process has finished successfully
        /// </summary>
        Authenticated,

        /// <summary>
        /// The authentication process has failed
        /// </summary>
        AuthenticationFailure,

        /// <summary>
        /// The connection is being closed.
        /// </summary>
        Closing,

        /// <summary>
        /// The connection is closed.
        /// </summary>
        Closed,
    }
}

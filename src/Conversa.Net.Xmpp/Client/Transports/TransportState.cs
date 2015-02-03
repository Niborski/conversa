// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Client.Transports
{
    public enum TransportState
    {
        /// <summary>
        /// The transport connection is being opened.
        /// </summary>
        Opening,

        /// <summary>
        /// The transport connection is being upgraded to a secure connection.
        /// </summary>
        UpgradingToSsl,

        /// <summary>
        /// The transport connection has been upgraded to a secure connection.
        /// </summary>
        UpgradedToSsl,

        /// <summary>
        /// The transport connection is open and the authentication has been done correctly.
        /// </summary>
        Open,

        /// <summary>
        /// The transport connection is being closed.
        /// </summary>
        Closing,

        /// <summary>
        /// The transport connection is closed.
        /// </summary>
        Closed,
    }
}

// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Core
{
    /// <summary>
    /// Presence Stanza
    /// </summary>
    /// <remarks>
    /// RFC 6121:  Instant Messaging and Presence
    /// </remarks>
    public partial class Presence
    {
        /// <summary>
        /// Configures the presence status as unavailable/offline
        /// </summary>
        /// <returns></returns>
        public Presence AsUnavailable()
        {
            this.Type          = PresenceType.Unavailable;
            this.TypeSpecified = true;
            this.Show          = ShowType.Offline;

            return this;
        }
    }
}

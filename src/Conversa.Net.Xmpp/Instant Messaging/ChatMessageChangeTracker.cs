// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using System;

namespace Conversa.Net.Xmpp.InstantMessaging
{
    /// <summary>
    /// Provides methods to enable and retrieve message change revisions.
    /// </summary>
    public sealed class ChatMessageChangeTracker
    {
        private bool enabled;
        private ChatMessageChangeReader reader;

        public ChatMessageChangeTracker()
        {
        }

        /// <summary>
        /// Enables change tracking for the messages in the message store.
        /// </summary>
        public void Enable()
        {
            this.enabled = true;
        }

        /// <summary>
        /// Returns a ChatMessageChangeReader class object which provides a collection of message revisions from the message store.
        /// </summary>
        /// <returns>The change reader associated with the change tracker.</returns>
        public ChatMessageChangeReader GetChangeReader()
        {
            return this.reader;
        }

        /// <summary>
        /// Resets change tracking for the messages in the message store. The first revision begins with the next message change.
        /// </summary>
        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}

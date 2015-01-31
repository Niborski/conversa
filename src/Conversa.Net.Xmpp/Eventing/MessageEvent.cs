// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.Core;

namespace Conversa.Net.Xmpp.Eventing
{
    /// <summary>
    /// Activity event for headline and normal messages
    /// </summary>
    public sealed class MessageEvent
        : Event
    {
        /// <summary>
        /// Gets the message information
        /// </summary>
        public Message Message
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="">XmppMessageEvent</see> class.
        /// </summary>
        /// <param name="message">The message information</param>
        public MessageEvent(Message message)
        {
            this.Message = message;
        }
    }
}

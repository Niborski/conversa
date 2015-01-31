// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.ChatStates
{
    using System.Xml.Serialization;

    /// <summary>
    /// Chat State Notifications
    /// </summary>
    /// <remarks>
    /// XEP-0085 Chat State Notifications
    /// </remarks>
    [XmlTypeAttribute(Namespace = "http://jabber.org/protocol/chatstates")]
    [XmlRootAttribute("active", Namespace = "http://jabber.org/protocol/chatstates", IsNullable = false)]
    public partial class ActiveChatState
    {
        public ActiveChatState()
        {
        }
    }
}

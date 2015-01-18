// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.InstantMessaging
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
    public partial class ChatStateActive
    {
        public ChatStateActive()
        {
        }
    }

    [XmlTypeAttribute(Namespace = "http://jabber.org/protocol/chatstates")]
    [XmlRootAttribute("composing", Namespace = "http://jabber.org/protocol/chatstates", IsNullable = false)]
    public partial class ChatStateComposing
    {
        public ChatStateComposing()
        {
        }
    }

    [XmlTypeAttribute(Namespace = "http://jabber.org/protocol/chatstates")]
    [XmlRootAttribute("gone", Namespace = "http://jabber.org/protocol/chatstates", IsNullable = false)]
    public partial class ChatStateGone
    {
        public ChatStateGone()
        {
        }
    }

    [XmlTypeAttribute(Namespace = "http://jabber.org/protocol/chatstates")]
    [XmlRootAttribute("inactive", Namespace = "http://jabber.org/protocol/chatstates", IsNullable = false)]
    public partial class ChatStateInactive
    {
        public ChatStateInactive()
        {
        }
    }

    [XmlTypeAttribute(Namespace = "http://jabber.org/protocol/chatstates")]
    [XmlRootAttribute("paused", Namespace = "http://jabber.org/protocol/chatstates", IsNullable = false)]
    public partial class ChatStatePaused
    {
        public ChatStatePaused()
        {
        }
    }
}

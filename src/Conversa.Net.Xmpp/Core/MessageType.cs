// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Core
{
    using System.Xml.Serialization;

    /// <summary>
    /// Message Stanza
    /// </summary>
    /// <remarks>
    /// RFC 6120: XMPP Core
    /// </remarks>
    [XmlTypeAttribute(Namespace = "jabber:client")]
    public enum MessageType
    {
        /// <remarks/>
        [XmlEnumAttribute("chat")]
        Chat,

        /// <remarks/>
        [XmlEnumAttribute("error")]
        Error,

        /// <remarks/>
        [XmlEnumAttribute("groupchat")]
        GroupChat,

        /// <remarks/>
        [XmlEnumAttribute("headline")]
        Headline,

        /// <remarks/>
        [XmlEnumAttribute("normal")]
        Normal,
    }
}

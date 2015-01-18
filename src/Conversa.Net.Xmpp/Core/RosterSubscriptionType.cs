// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Core
{
    using System.Xml.Serialization;

    /// <summary>
    /// Roster Management
    /// </summary>
    /// <remarks>
    /// RFC 6121:  Instant Messaging and Presence
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "jabber:iq:roster")]
    public enum RosterSubscriptionType
    {
        /// <remarks/>
        [XmlEnumAttribute("both")]
        Both,

        /// <remarks/>
        [XmlEnumAttribute("from")]
        From,

        /// <remarks/>
        [XmlEnumAttribute("none")]
        None,

        /// <remarks/>
        [XmlEnumAttribute("remove")]
        Remove,

        /// <remarks/>
        [XmlEnumAttribute("to")]
        To,
    }
}

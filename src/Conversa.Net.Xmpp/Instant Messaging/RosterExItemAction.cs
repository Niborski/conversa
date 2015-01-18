// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.InstantMessaging
{
    using System.Xml.Serialization;

    /// <summary>
    /// Roster Item Exchange
    /// </summary>
    /// <remarks>
    /// XEP-0144: Roster Item Exchange
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://jabber.org/protocol/rosterx")]
    public enum RosterExItemAction
    {
        /// <remarks/>
        [XmlEnumAttribute("add")]
        Add,

        /// <remarks/>
        [XmlEnumAttribute("delete")]
        Delete,

        /// <remarks/>
        [XmlEnumAttribute("modify")]
        Modify,
    }
}

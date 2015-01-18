// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Privacy
{
    using System.Xml.Serialization;

    /// <summary>
    /// Privacy Lists
    /// </summary>
    /// <remarks>
    /// XEP-0016: Privacy Lists
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "jabber:iq:privacy")]
    public enum PrivacyAction
    {
        /// <remarks/>
        [XmlEnumAttribute("allow")]
        Alow,

        /// <remarks/>
        [XmlEnumAttribute("deny")]
        Deny
    }
}

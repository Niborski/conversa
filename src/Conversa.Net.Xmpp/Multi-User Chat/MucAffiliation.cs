// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.MultiUserChat
{
    using System.Xml.Serialization;

    /// <summary>
    /// Multi-User Chat
    /// </summary>
    /// <remarks>
    /// XEP-0045: Multi-User Chat
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true)]
    public enum MucAffiliation
    {
        /// <remarks/>
        [XmlEnumAttribute("admin")]
        Admin,

        /// <remarks/>
        [XmlEnumAttribute("member")]
        Member,

        /// <remarks/>
        [XmlEnumAttribute("none")]
        None,

        /// <remarks/>
        [XmlEnumAttribute("outcast")]
        Outcast,

        /// <remarks/>
        [XmlEnumAttribute("owner")]
        Owner,
    }
}

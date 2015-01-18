// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.PublishSubscribe
{
    using System.Xml.Serialization;

    /// <summary>
    /// Publish-Subscribe
    /// </summary>
    /// <remarks>
    /// XEP-0060: Publish-Subscribe
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://jabber.org/protocol/pubsub")]
    public enum PubSubAffiliationType
    {
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

        /// <remarks/>
        [XmlEnumAttribute("publisher")]
        Publisher,
    }
}

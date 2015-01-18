// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Core
{
    using Conversa.Net.Xmpp.Shared;
    using System.Xml.Serialization;

    /// <summary>
    /// Stream Compression
    /// </summary>
    /// <remarks>
    /// XEP-0138 Stream Compression
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://jabber.org/protocol/compress")]
    [XmlRootAttribute(Namespace = "http://jabber.org/protocol/compress", IsNullable = false)]
    public partial class CompressFailure
    {
        [XmlElementAttribute("processing-failed", typeof(Empty), Namespace = "http://jabber.org/protocol/compress")]
        [XmlElementAttribute("setup-failed", typeof(Empty), Namespace = "http://jabber.org/protocol/compress")]
        [XmlElementAttribute("unsupported-method", typeof(Empty), Namespace = "http://jabber.org/protocol/compress")]
        [XmlElementAttribute("bad-request", typeof(Empty), Namespace = "urn:ietf:params:xml:ns:xmpp-stanzas")]
        [XmlElementAttribute("conflict", typeof(Empty), Namespace = "urn:ietf:params:xml:ns:xmpp-stanzas")]
        [XmlElementAttribute("feature-not-implemented", typeof(Empty), Namespace = "urn:ietf:params:xml:ns:xmpp-stanzas")]
        [XmlElementAttribute("forbidden", typeof(Empty), Namespace = "urn:ietf:params:xml:ns:xmpp-stanzas")]
        [XmlElementAttribute("gone", typeof(string), Namespace = "urn:ietf:params:xml:ns:xmpp-stanzas")]
        [XmlElementAttribute("internal-server-error", typeof(Empty), Namespace = "urn:ietf:params:xml:ns:xmpp-stanzas")]
        [XmlElementAttribute("item-not-found", typeof(Empty), Namespace = "urn:ietf:params:xml:ns:xmpp-stanzas")]
        [XmlElementAttribute("jid-malformed", typeof(Empty), Namespace = "urn:ietf:params:xml:ns:xmpp-stanzas")]
        [XmlElementAttribute("not-acceptable", typeof(Empty), Namespace = "urn:ietf:params:xml:ns:xmpp-stanzas")]
        [XmlElementAttribute("not-allowed", typeof(Empty), Namespace = "urn:ietf:params:xml:ns:xmpp-stanzas")]
        [XmlElementAttribute("not-authorized", typeof(Empty), Namespace = "urn:ietf:params:xml:ns:xmpp-stanzas")]
        [XmlElementAttribute("payment-required", typeof(Empty), Namespace = "urn:ietf:params:xml:ns:xmpp-stanzas")]
        [XmlElementAttribute("policy-violation", typeof(Empty), Namespace = "urn:ietf:params:xml:ns:xmpp-stanzas")]
        [XmlElementAttribute("recipient-unavailable", typeof(Empty), Namespace = "urn:ietf:params:xml:ns:xmpp-stanzas")]
        [XmlElementAttribute("redirect", typeof(string), Namespace = "urn:ietf:params:xml:ns:xmpp-stanzas")]
        [XmlElementAttribute("registration-required", typeof(Empty), Namespace = "urn:ietf:params:xml:ns:xmpp-stanzas")]
        [XmlElementAttribute("remote-server-not-found", typeof(Empty), Namespace = "urn:ietf:params:xml:ns:xmpp-stanzas")]
        [XmlElementAttribute("remote-server-timeout", typeof(Empty), Namespace = "urn:ietf:params:xml:ns:xmpp-stanzas")]
        [XmlElementAttribute("resource-constraint", typeof(Empty), Namespace = "urn:ietf:params:xml:ns:xmpp-stanzas")]
        [XmlElementAttribute("service-unavailable", typeof(Empty), Namespace = "urn:ietf:params:xml:ns:xmpp-stanzas")]
        [XmlElementAttribute("subscription-required", typeof(Empty), Namespace = "urn:ietf:params:xml:ns:xmpp-stanzas")]
        [XmlElementAttribute("text", typeof(Text), Namespace = "urn:ietf:params:xml:ns:xmpp-stanzas")]
        [XmlElementAttribute("undefined-condition", typeof(Empty), Namespace = "urn:ietf:params:xml:ns:xmpp-stanzas")]
        [XmlElementAttribute("unexpected-request", typeof(Empty), Namespace = "urn:ietf:params:xml:ns:xmpp-stanzas")]
        [XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items
        {
            get;
            set;
        }

        [XmlElementAttribute("ItemsElementName")]
        [XmlIgnore]
        public CompressFailureType[] ItemsElementName
        {
            get;
            set;
        }

        public CompressFailure()
        {
        }
    }
}

// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Core
{
    using System.Xml.Serialization;

    /// <summary>
    /// Stream Compression
    /// </summary>
    /// <remarks>
    /// XEP-0138 Stream Compression
    /// </remarks>
    [XmlTypeAttribute(Namespace = "http://jabber.org/protocol/compress", IncludeInSchema = false)]
    public enum CompressFailureType
    {
        /// <remarks/>
        [XmlEnumAttribute("processing-failed")]
        ProcessingFailed,

        /// <remarks/>
        [XmlEnumAttribute("setup-failed")]
        SetupFailed,

        /// <remarks/>
        [XmlEnumAttribute("unsupported-method")]
        UnsupportedMethod,

        /// <remarks/>
        [XmlEnumAttribute("urn:ietf:params:xml:ns:xmpp-stanzas:bad-request")]
        BadRequest,

        /// <remarks/>
        [XmlEnumAttribute("urn:ietf:params:xml:ns:xmpp-stanzas:conflict")]
        Conflict,

        /// <remarks/>
        [XmlEnumAttribute("urn:ietf:params:xml:ns:xmpp-stanzas:feature-not-implemented")]
        FeatureNotImplemented,

        /// <remarks/>
        [XmlEnumAttribute("urn:ietf:params:xml:ns:xmpp-stanzas:forbidden")]
        Forbidden,

        /// <remarks/>
        [XmlEnumAttribute("urn:ietf:params:xml:ns:xmpp-stanzas:gone")]
        Gone,

        /// <remarks/>
        [XmlEnumAttribute("urn:ietf:params:xml:ns:xmpp-stanzas:internal-server-error")]
        InternalServerError,

        /// <remarks/>
        [XmlEnumAttribute("urn:ietf:params:xml:ns:xmpp-stanzas:item-not-found")]
        ItemNotFound,

        /// <remarks/>
        [XmlEnumAttribute("urn:ietf:params:xml:ns:xmpp-stanzas:jid-malformed")]
        JidMalformed,

        /// <remarks/>
        [XmlEnumAttribute("urn:ietf:params:xml:ns:xmpp-stanzas:not-acceptable")]
        NotAcceptable,

        /// <remarks/>
        [XmlEnumAttribute("urn:ietf:params:xml:ns:xmpp-stanzas:not-allowed")]
        NotAllowed,

        /// <remarks/>
        [XmlEnumAttribute("urn:ietf:params:xml:ns:xmpp-stanzas:not-authorized")]
        NotAuthorized,

        /// <remarks/>
        [XmlEnumAttribute("urn:ietf:params:xml:ns:xmpp-stanzas:payment-required")]
        PaymentRequired,

        /// <remarks/>
        [XmlEnumAttribute("urn:ietf:params:xml:ns:xmpp-stanzas:policy-violation")]
        PolicyViolation,

        /// <remarks/>
        [XmlEnumAttribute("urn:ietf:params:xml:ns:xmpp-stanzas:recipient-unavailable")]
        RecipientUnavailable,

        /// <remarks/>
        [XmlEnumAttribute("urn:ietf:params:xml:ns:xmpp-stanzas:redirect")]
        Redirect,

        /// <remarks/>
        [XmlEnumAttribute("urn:ietf:params:xml:ns:xmpp-stanzas:registration-required")]
        RegistrationRequired,

        /// <remarks/>
        [XmlEnumAttribute("urn:ietf:params:xml:ns:xmpp-stanzas:remote-server-not-found")]
        RemoteServerNotFound,

        /// <remarks/>
        [XmlEnumAttribute("urn:ietf:params:xml:ns:xmpp-stanzas:remote-server-timeout")]
        RemoteServerTimeout,

        /// <remarks/>
        [XmlEnumAttribute("urn:ietf:params:xml:ns:xmpp-stanzas:resource-constraint")]
        ResourceConstraint,

        /// <remarks/>
        [XmlEnumAttribute("urn:ietf:params:xml:ns:xmpp-stanzas:service-unavailable")]
        ServiceUnavailable,

        /// <remarks/>
        [XmlEnumAttribute("urn:ietf:params:xml:ns:xmpp-stanzas:subscription-required")]
        SubscriptionRequired,

        /// <remarks/>
        [XmlEnumAttribute("urn:ietf:params:xml:ns:xmpp-stanzas:text")]
        Text,

        /// <remarks/>
        [XmlEnumAttribute("urn:ietf:params:xml:ns:xmpp-stanzas:undefined-condition")]
        UndefinedCondition,

        /// <remarks/>
        [XmlEnumAttribute("urn:ietf:params:xml:ns:xmpp-stanzas:unexpected-request")]
        UnexpectedRequest,
    }
}

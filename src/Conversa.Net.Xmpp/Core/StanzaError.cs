// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Core
{
    using Conversa.Net.Xmpp.Shared;
    using System.Xml.Serialization;

    /// <remarks/>
    [XmlTypeAttribute(Namespace = "jabber:client")]
    [XmlRootAttribute("error", Namespace = "jabber:client", IsNullable = false)]
    public partial class StanzaError
    {
        /// <remarks/>
        [XmlElementAttribute("bad-request", Namespace = "urn:ietf:params:xml:ns:xmpp-stanzas")]
        public string BadRequest
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("conflict", Namespace = "urn:ietf:params:xml:ns:xmpp-stanzas")]
        public string Conflict
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("feature-not-implemented", Namespace = "urn:ietf:params:xml:ns:xmpp-stanzas")]
        public string FeatureNotImplemented
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("forbidden", Namespace = "urn:ietf:params:xml:ns:xmpp-stanzas")]
        public string Forbidden
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("gone", Namespace = "urn:ietf:params:xml:ns:xmpp-stanzas")]
        public string Gone
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("internal-server-error", Namespace = "urn:ietf:params:xml:ns:xmpp-stanzas")]
        public string InternalServerError
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("item-not-found", Namespace = "urn:ietf:params:xml:ns:xmpp-stanzas")]
        public string ItemNotFound
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("jid-malformed", Namespace = "urn:ietf:params:xml:ns:xmpp-stanzas")]
        public string JidMalformed
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("not-acceptable", Namespace = "urn:ietf:params:xml:ns:xmpp-stanzas")]
        public string NotAcceptable
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("not-allowed", Namespace = "urn:ietf:params:xml:ns:xmpp-stanzas")]
        public string NotAllowed
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("payment-required", Namespace = "urn:ietf:params:xml:ns:xmpp-stanzas")]
        public string PaymentRequired
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("recipient-unavailable", Namespace = "urn:ietf:params:xml:ns:xmpp-stanzas")]
        public string RecipientUnavailable
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("redirect", Namespace = "urn:ietf:params:xml:ns:xmpp-stanzas")]
        public string Redirect
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("registration-required", Namespace = "urn:ietf:params:xml:ns:xmpp-stanzas")]
        public string RegistrationRequired
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("remote-server-not-found", Namespace = "urn:ietf:params:xml:ns:xmpp-stanzas")]
        public string RemoteServerNotFound
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("remote-server-timeout", Namespace = "urn:ietf:params:xml:ns:xmpp-stanzas")]
        public string RemoteServerTimeout
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("resource-constraint", Namespace = "urn:ietf:params:xml:ns:xmpp-stanzas")]
        public string resourceconstraint
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("service-unavailable", Namespace = "urn:ietf:params:xml:ns:xmpp-stanzas")]
        public string ServiceUnavailable
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("subscription-required", Namespace = "urn:ietf:params:xml:ns:xmpp-stanzas")]
        public string SubscriptionRequired
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("undefined-condition", Namespace = "urn:ietf:params:xml:ns:xmpp-stanzas")]
        public string UndefinedCondition
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("unexpected-request", Namespace = "urn:ietf:params:xml:ns:xmpp-stanzas")]
        public string UnexpectedRequest
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("text", Namespace = "urn:ietf:params:xml:ns:xmpp-stanzas")]
        public Text Text
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlAttributeAttribute("code")]
        public short Code
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlIgnoreAttribute]
        public bool CodeSpecified
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlAttributeAttribute("type")]
        public StanzaErrorType Type
        {
            get;
            set;
        }

        public StanzaError()
        {
        }
    }
}

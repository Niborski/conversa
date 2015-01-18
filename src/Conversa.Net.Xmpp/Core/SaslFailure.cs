// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Core
{
    using Conversa.Net.Xmpp.Shared;
    using System.Xml.Serialization;

    /// <summary>
    /// SASL Authentication
    /// </summary>
    /// <remarks>
    /// RFC 6120: XMPP Core
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "urn:ietf:params:xml:ns:xmpp-sasl")]
    [XmlRootAttribute("failure", Namespace = "urn:ietf:params:xml:ns:xmpp-sasl", IsNullable = false)]
    public partial class SaslFailure
    {
        [XmlElementAttribute("aborted", typeof(Empty), Namespace = "urn:ietf:params:xml:ns:xmpp-sasl")]
        [XmlElementAttribute("account-disabled", typeof(Empty), Namespace = "urn:ietf:params:xml:ns:xmpp-sasl")]
        [XmlElementAttribute("credentials-expired", typeof(Empty), Namespace = "urn:ietf:params:xml:ns:xmpp-sasl")]
        [XmlElementAttribute("encryption-required", typeof(Empty), Namespace = "urn:ietf:params:xml:ns:xmpp-sasl")]
        [XmlElementAttribute("incorrect-encoding", typeof(Empty), Namespace = "urn:ietf:params:xml:ns:xmpp-sasl")]
        [XmlElementAttribute("invalid-authzid", typeof(Empty), Namespace = "urn:ietf:params:xml:ns:xmpp-sasl")]
        [XmlElementAttribute("invalid-mechanism", typeof(Empty), Namespace = "urn:ietf:params:xml:ns:xmpp-sasl")]
        [XmlElementAttribute("malformed-request", typeof(Empty), Namespace = "urn:ietf:params:xml:ns:xmpp-sasl")]
        [XmlElementAttribute("mechanism-too-weak", typeof(Empty), Namespace = "urn:ietf:params:xml:ns:xmpp-sasl")]
        [XmlElementAttribute("not-authorized", typeof(Empty), Namespace = "urn:ietf:params:xml:ns:xmpp-sasl")]
        [XmlElementAttribute("temporary-auth-failure", typeof(Empty), Namespace = "urn:ietf:params:xml:ns:xmpp-sasl")]
        [XmlElementAttribute("transition-needed", typeof(Empty), Namespace = "urn:ietf:params:xml:ns:xmpp-sasl")]
        [XmlChoiceIdentifierAttribute("FailureType")]
        public Empty Item
        {
            get;
            set;
        }

        [XmlIgnore]
        public SaslFailureType FailureType
        {
            get;
            set;
        }

        [XmlElementAttribute("text", Namespace = "urn:ietf:params:xml:ns:xmpp-sasl")]
        public Text Text
        {
            get;
            set;
        }

        public SaslFailure()
        {
            this.Text = new Text();
        }
    }
}

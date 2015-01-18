// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Core
{
    using Conversa.Net.Xmpp.Shared;
    using System.Xml.Serialization;

    /// <summary>
    /// XML Streams
    /// </summary>
    /// <remarks>
    /// RFC 6120: XMPP core
    /// </remarks>
    [XmlTypeAttribute(TypeName = "error", Namespace = "http://etherx.jabber.org/streams")]
    [XmlRootAttribute("error", Namespace = "http://etherx.jabber.org/streams", IsNullable = false)]
    public partial class StreamError
    {
        /// <remarks/>
        [XmlElementAttribute("bad-format", Namespace = "urn:ietf:params:xml:ns:xmpp-streams", IsNullable = true)]
        public string BadFormat
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("bad-namespace-prefix", Namespace = "urn:ietf:params:xml:ns:xmpp-streams", IsNullable = true)]
        public string BadNamespacePrefix
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("conflict", Namespace = "urn:ietf:params:xml:ns:xmpp-streams", IsNullable = true)]
        public string Conflict
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("connection-timeout", Namespace = "urn:ietf:params:xml:ns:xmpp-streams", IsNullable = true)]
        public string ConnectionTimeout
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("host-gone", Namespace = "urn:ietf:params:xml:ns:xmpp-streams", IsNullable = true)]
        public string HostGone
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("host-unknown", Namespace = "urn:ietf:params:xml:ns:xmpp-streams", IsNullable = true)]
        public string HostUnknown
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("improper-addressing", Namespace = "urn:ietf:params:xml:ns:xmpp-streams", IsNullable = true)]
        public string ImproperAddressing
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("internal-server-error", Namespace = "urn:ietf:params:xml:ns:xmpp-streams", IsNullable = true)]
        public string InternalServerError
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("invalid-from", Namespace = "urn:ietf:params:xml:ns:xmpp-streams", IsNullable = true)]
        public string InvalidFrom
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("invalid-id", Namespace = "urn:ietf:params:xml:ns:xmpp-streams", IsNullable = true)]
        public string InvalidID
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("invalid-namespace", Namespace = "urn:ietf:params:xml:ns:xmpp-streams", IsNullable = true)]
        public string InvalidNamespace
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("invalid-xml", Namespace = "urn:ietf:params:xml:ns:xmpp-streams", IsNullable = true)]
        public string InvalidXml
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("not-authorized", Namespace = "urn:ietf:params:xml:ns:xmpp-streams", IsNullable = true)]
        public string NotAuthorized
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("policy-violation", Namespace = "urn:ietf:params:xml:ns:xmpp-streams", IsNullable = true)]
        public string PolicyViolation
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("remote-connection-failed", Namespace = "urn:ietf:params:xml:ns:xmpp-streams", IsNullable = true)]
        public string RemoteConnectionFailed
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("resource-constraint", Namespace = "urn:ietf:params:xml:ns:xmpp-streams", IsNullable = true)]
        public string ResourceConstraint
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("restricted-xml", Namespace = "urn:ietf:params:xml:ns:xmpp-streams", IsNullable = true)]
        public string RestrictedXml
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("see-other-host", Namespace = "urn:ietf:params:xml:ns:xmpp-streams", IsNullable = true)]
        public string SeeOtherHost
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("system-shutdown", Namespace = "urn:ietf:params:xml:ns:xmpp-streams", IsNullable = true)]
        public string SystemShutdown
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("undefined-condition", Namespace = "urn:ietf:params:xml:ns:xmpp-streams", IsNullable = true)]
        public string UndefinedCondition
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("unsupported-encoding", Namespace = "urn:ietf:params:xml:ns:xmpp-streams", IsNullable = true)]
        public string UnsupportedEncoding
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("unsupported-stanza-type", Namespace = "urn:ietf:params:xml:ns:xmpp-streams", IsNullable = true)]
        public string UnsupportedStanzaType
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("unsupported-version", Namespace = "urn:ietf:params:xml:ns:xmpp-streams", IsNullable = true)]
        public string UnsupportedVersion
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("xml-not-well-formed", Namespace = "urn:ietf:params:xml:ns:xmpp-streams", IsNullable = true)]
        public string XmlNotWellFormed
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("text", Namespace = "urn:ietf:params:xml:ns:xmpp-streams", IsNullable = true)]
        public Text Text
        {
            get;
            set;
        }

        public StreamError()
        {
        }
    }
}

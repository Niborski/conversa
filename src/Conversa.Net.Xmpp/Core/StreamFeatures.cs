// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Core
{
    using Conversa.Net.Xmpp.AdvancedMessageProcessing;
    using Conversa.Net.Xmpp.Caps;
    using Conversa.Net.Xmpp.InBandRegistration;
    using System.Xml.Serialization;

    /// <summary>
    /// XML Streams
    /// </summary>
    /// <remarks>
    /// RFC 6120: XMPP core
    /// </remarks>
    [XmlTypeAttribute(Namespace = "http://etherx.jabber.org/streams")]
    [XmlRootAttribute("features", Namespace = "http://etherx.jabber.org/streams", IsNullable = false)]
    public partial class StreamFeatures
    {
        /// <summary>
        /// Support for Advanced Message Processing
        /// </summary>
        /// <remarks>
        /// XEP-0079: Advanced Message Processing
        /// </remarks>
        [XmlElementAttribute("amp", Namespace = "http://jabber.org/features/amp")]
        public AmpFeature AdvancedMessageProcessing
        {
           get;
           set;
        }

        /// <summary>
        /// Support for Stream Compression
        /// </summary>
        /// <remarks>
        /// XEP-0138: Stream Compression
        /// </remarks>
        [XmlElementAttribute("compression", Namespace = "http://jabber.org/features/compress")]
        public StreamCompressionFeature Compression
        {
            get;
            set;
        }

        /// <summary>
        /// Support for In-Band Registration
        /// </summary>
        /// <remarks>
        /// XEP-0077: In-Band Registration
        /// </remarks>
        [XmlElementAttribute("register", Namespace = "http://jabber.org/features/iq-register")]
        public RegisterFeature Register
        {
            get;
            set;
        }

        /// <summary>
        /// Support for Resource Binding
        /// </summary>
        /// <remarks>
        /// RFC 6120: XMPP Core
        /// </remarks>
        [XmlElementAttribute("bind", Namespace = "urn:ietf:params:xml:ns:xmpp-bind")]
        public Bind Bind
        {
            get;
            set;
        }

        /// <summary>
        /// Support for Simple Authentication and Security Layer (SASL)
        /// </summary>
        /// <remarks>
        /// RFC 6120: XMPP Core
        /// </remarks>
        [XmlElementAttribute("mechanisms", Namespace = "urn:ietf:params:xml:ns:xmpp-sasl")]
        public SaslMechanisms Mechanisms
        {
            get;
            set;
        }

        /// <summary>
        /// Support for IM Session Establishment
        /// </summary>
        /// <remarks>
        /// RFC 6121: XMPP IM
        /// </remarks>
        [XmlElementAttribute("session", Namespace = "urn:ietf:params:xml:ns:xmpp-session")]
        public Session Session
        {
            get;
            set;
        }

        /// <summary>
        /// Support for Transport Layer Security (TLS)
        /// </summary>
        /// <remarks>
        /// RFC 6120: XMPP Core
        /// </remarks>
        [XmlElementAttribute("starttls", Namespace = "urn:ietf:params:xml:ns:xmpp-tls")]
        public StartTls StartTls
        {
            get;
            set;
        }

        /// <summary>
        /// Support for Stream Management
        /// </summary>
        /// <remarks>
        /// XEP-0198: Stream Management
        /// </remarks>
        //[XmlElementAttribute("sm", Namespace = "urn:xmpp:sm:3")]
        //public StreamManagement StreamManagement
        //{
        //    get;
        //    set;
        //}

        [XmlElementAttribute("c", Namespace = "http://jabber.org/protocol/caps")]
        public EntityCapabilities EntityCapabilities
        {
            get;
            set;
        }

        public StreamFeatures()
        {
        }
    }
}

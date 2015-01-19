// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Core
{
    using Conversa.Net.Xmpp.Shared;
    using System.Xml.Serialization;

    /// <summary>
    /// STARTTLS Negotiation
    /// <remarks>
    /// RFC 6120: XMPP Core
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "urn:ietf:params:xml:ns:xmpp-tls")]
    [XmlRootAttribute("starttls", Namespace = "urn:ietf:params:xml:ns:xmpp-tls", IsNullable = false)]
    public partial class StartTls
    {
        [XmlElementAttribute("required", Namespace = "urn:ietf:params:xml:ns:xmpp-tls")]
        public Empty Required
        {
            get;
            set;
        }

        [XmlIgnore]
        public bool RequiredSpecified
        {
            get;
            set;
        }

        public StartTls()
        {
        }
    }
}

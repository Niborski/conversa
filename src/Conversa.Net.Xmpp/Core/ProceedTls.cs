// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Core
{
    using System.Xml.Serialization;

    /// <summary>
    /// STARTTLS Negotiation
    /// </summary>
    /// <remarks>
    /// RFC 6120: XMPP Core
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "urn:ietf:params:xml:ns:xmpp-tls")]
    [XmlRootAttribute("proceed", Namespace = "urn:ietf:params:xml:ns:xmpp-tls", IsNullable = false)]
    public partial class TlsProceed
    {
        public TlsProceed()
        {
        }
    }
}

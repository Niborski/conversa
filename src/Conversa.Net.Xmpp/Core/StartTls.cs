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
        private Empty? required;

        [XmlElementAttribute("required", Namespace = "urn:ietf:params:xml:ns:xmpp-tls")]
        public Empty Required
        {
            get
            {
                if (this.required.HasValue)
                {
                    return this.required.Value;
                }
                else
                {
                    return default(Empty);
                }
            }
            set { this.required = value; }
        }

        [XmlIgnore]
        public bool RequiredSpecified
        {
            get { return this.required.HasValue; }
            set
            {
                if (!value)
                {
                    this.required = null;
                }
            }
        }

        public StartTls()
        {
        }
    }
}

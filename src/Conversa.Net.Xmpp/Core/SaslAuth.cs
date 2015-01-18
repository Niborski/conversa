// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Core
{
    using System.Xml.Serialization;

    /// <summary>
    /// SASL Authentication
    /// </summary>
    /// <remarks>
    /// RFC 6120: XMPP Core
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "urn:ietf:params:xml:ns:xmpp-sasl")]
    [XmlRootAttribute("auth", Namespace = "urn:ietf:params:xml:ns:xmpp-sasl", IsNullable = false)]
    public partial class SaslAuth
    {
        [XmlAttributeAttribute("mechanism", DataType = "NMTOKEN")]
        public string Mechanism
        {
            get;
            set;
        }

        [XmlTextAttribute]
        public string Value
        {
            get;
            set;
        }

        public SaslAuth()
        {
        }
    }
}

// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Core
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    /// <summary>
    /// SASL Authentication
    /// </summary>
    /// <remarks>
    /// RFC 6120: XMPP Core
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "urn:ietf:params:xml:ns:xmpp-sasl")]
    [XmlRootAttribute("mechanisms", Namespace = "urn:ietf:params:xml:ns:xmpp-sasl", IsNullable = false)]
    public partial class SaslMechanisms
    {
        [XmlElementAttribute("mechanism", DataType = "NMTOKEN")]
        public List<string> Mechanism
        {
            get;
            set;
        }

        public SaslMechanisms()
        {
            this.Mechanism = new List<string>();
        }
    }
}

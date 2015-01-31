// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Discovery
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    /// <summary>
    /// Service Discovery
    /// </summary>
    /// <remarks>
    /// XEP-0030: Service Discovery
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://jabber.org/protocol/disco#info")]
    [XmlRootAttribute("query", Namespace = "http://jabber.org/protocol/disco#info", IsNullable = false)]
    public partial class ServiceInfo
    {
        [XmlElementAttribute("identity")]
        public List<ServiceIdentity> Identities
        {
            get;
            set;
        }

        [XmlElementAttribute("feature")]
        public List<ServiceFeature> Features
        {
            get;
            set;
        }

        [XmlAttribute("node")]
        public string Node
        {
            get;
            set;
        }

        public ServiceInfo()
        {
            this.Identities = new List<ServiceIdentity>();
            this.Features   = new List<ServiceFeature>();
        }

        public override string ToString()
        {
            return this.Node;
        }
    }
}

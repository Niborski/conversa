// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.ServiceDiscovery
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    /// <summary>
    /// Service Discovery
    /// </summary>
    /// <remarks>
    /// XEP-0030: Service Discovery
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://jabber.org/protocol/disco#items")]
    [XmlRootAttribute("query", Namespace = "http://jabber.org/protocol/disco#items", IsNullable = false)]
    public partial class ServiceItem
    {
        [XmlElementAttribute("item")]
        public List<ServiceItemDetail> Items
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

        public ServiceItem()
        {
            this.Items = new List<ServiceItemDetail>();
        }
    }
}

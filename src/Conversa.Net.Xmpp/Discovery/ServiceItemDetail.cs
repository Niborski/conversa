// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Discovery
{
    using Conversa.Net.Xmpp.Shared;
    using System.Xml.Serialization;

    /// <summary>
    /// Service Discovery
    /// </summary>
    /// <remarks>
    /// XEP-0030: Service Discovery
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://jabber.org/protocol/disco#items")]
    [XmlRootAttribute("item", Namespace = "http://jabber.org/protocol/disco#items", IsNullable = false)]
    public partial class ServiceItemDetail
    {
        [XmlAttribute("jid")]
        public string Jid
        {
            get;
            set;
        }

        [XmlAttribute("name")]
        public string Name
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

        [XmlTextAttribute]
        public Empty Value
        {
            get;
            set;
        }

        public ServiceItemDetail()
        {
        }
    }
}

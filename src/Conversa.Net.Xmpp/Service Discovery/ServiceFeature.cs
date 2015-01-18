// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.ServiceDiscovery
{
    using Conversa.Net.Xmpp.Shared;
    using System.Xml.Serialization;

    /// <summary>
    /// Service Discovery
    /// </summary>
    /// <remarks>
    /// XEP-0030: Service Discovery
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://jabber.org/protocol/disco#info")]
    [XmlRootAttribute("feature", Namespace = "http://jabber.org/protocol/disco#info", IsNullable = false)]
    public partial class ServiceFeature
    {
        [XmlAttribute("var")]
        public string Name
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

        public ServiceFeature()
        {
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}

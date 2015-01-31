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
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://jabber.org/protocol/disco#info")]
    [XmlRootAttribute("identity", Namespace = "http://jabber.org/protocol/disco#info", IsNullable = false)]
    public partial class ServiceIdentity
    {
        [XmlAttribute("category")]
        public string Category
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

        [XmlAttribute("type")]
        public string Type
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

        public ServiceIdentity()
        {
        }

        public override string ToString()
        {
            return this.Category + "/" + this.Name + "/" + this.Type;
        }
    }
}

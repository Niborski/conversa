// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.PublishSubscribe
{
    using System.Xml.Serialization;

    /// <summary>
    /// Publish-Subscribe
    /// </summary>
    /// <remarks>
    /// XEP-0060: Publish-Subscribe
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://jabber.org/protocol/pubsub#event")]
    [XmlRootAttribute("collection", Namespace = "http://jabber.org/protocol/pubsub#event", IsNullable = false)]
    public partial class PubSubEventCollection
    {
        /// <remarks/>
        [XmlElementAttribute("associate", typeof(PubSubEventAssociate))]
        [XmlElementAttribute("disassociate", typeof(PubSubEventDisassociate))]
        public object Item
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlAttributeAttribute("node")]
        public string Node
        {
            get;
            set;
        }
    }
}

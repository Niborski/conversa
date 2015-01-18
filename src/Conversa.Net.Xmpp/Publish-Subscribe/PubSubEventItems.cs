// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.PublishSubscribe
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    /// <summary>
    /// Publish-Subscribe
    /// </summary>
    /// <remarks>
    /// XEP-0060: Publish-Subscribe
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://jabber.org/protocol/pubsub#event")]
    [XmlRootAttribute("items", Namespace = "http://jabber.org/protocol/pubsub#event", IsNullable = false)]
    public partial class PubSubEventItems
    {
        /// <remarks/>
        [XmlElementAttribute("item", typeof(PubSubItem))]
        [XmlElementAttribute("retract", typeof(PubSubEventRetract))]
        public List<object> Items
        {
            get;
            private set;
        }

        /// <remarks/>
        [XmlAttributeAttribute("node")]
        public string Node
        {
            get;
            set;
        }

        public PubSubEventItems()
        {
            this.Items = new List<object>();
        }
    }
}

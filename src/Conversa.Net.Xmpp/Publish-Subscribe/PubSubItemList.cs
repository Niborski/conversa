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
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://jabber.org/protocol/pubsub")]
    [XmlRootAttribute("items", Namespace = "http://jabber.org/protocol/pubsub", IsNullable = false)]
    public partial class PubSubItemList
    {
        /// <remarks/>
        [XmlElementAttribute("item")]
        public List<PubSubItem> Items
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlAttributeAttribute("max_items", DataType = "positiveInteger")]
        public string MaxItems
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

        /// <remarks/>
        [XmlAttributeAttribute("subid")]
        public string Subid
        {
            get;
            set;
        }

        public PubSubItemList()
        {
            this.Items = new List<PubSubItem>();
        }
    }
}

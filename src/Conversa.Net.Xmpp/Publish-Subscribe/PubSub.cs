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
    [XmlRootAttribute("pubsub", Namespace = "http://jabber.org/protocol/pubsub", IsNullable = false)]
    public class PubSub
    {
        /// <remarks/>
        [XmlElementAttribute("affiliations", typeof(PubSubAffiliationList))]
        [XmlElementAttribute("configure", typeof(PubSubConfigure))]
        [XmlElementAttribute("create", typeof(PubSubCreate))]
        [XmlElementAttribute("items", typeof(PubSubItemList))]
        [XmlElementAttribute("options", typeof(PubSubOptions))]
        [XmlElementAttribute("publish", typeof(PubSubPublish))]
        [XmlElementAttribute("retract", typeof(PubSubRetract))]
        [XmlElementAttribute("subscribe", typeof(PubSubSubscribe))]
        [XmlElementAttribute("subscription", typeof(PubSubSubscription))]
        [XmlElementAttribute("subscriptions", typeof(PubSubSubscriptionList))]
        [XmlElementAttribute("unsubscribe", typeof(PubSubUnsubscribe))]
        public List<object> Items
        {
            get;
            private set;
        }

        public PubSub()
        {
            this.Items = new List<object>();
        }
    }
}

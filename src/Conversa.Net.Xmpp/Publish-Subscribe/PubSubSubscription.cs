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
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://jabber.org/protocol/pubsub")]
    [XmlRootAttribute("subscription", Namespace = "http://jabber.org/protocol/pubsub", IsNullable = false)]
    public class PubSubSubscription
    {
        /// <remarks/>
        [XmlElementAttribute("subscribe-options")]
        public PubSubSubscribeOptions SubscribeOptions
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlAttributeAttribute("jid")]
        public string Jid
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

        /// <remarks/>
        [XmlAttributeAttribute("subscription")]
        public PubSubSubscriptionType Subscription
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlIgnoreAttribute]
        public bool SubscriptionSpecified
        {
            get;
            set;
        }

        public PubSubSubscription()
        {
            this.SubscribeOptions = new PubSubSubscribeOptions();
        }
    }
}

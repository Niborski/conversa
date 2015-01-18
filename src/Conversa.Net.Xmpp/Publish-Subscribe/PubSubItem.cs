// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.PublishSubscribe
{
    using Conversa.Net.Xmpp.PersonalEventing;
    using System.Xml.Serialization;

    /// <summary>
    /// Publish-Subscribe
    /// </summary>
    /// <remarks>
    /// XEP-0060: Publish-Subscribe
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://jabber.org/protocol/pubsub")]
    [XmlRootAttribute("item", Namespace = "http://jabber.org/protocol/pubsub", IsNullable = false)]
    public class PubSubItem
    {
        /// <remarks/>
        [XmlElementAttribute("tune", typeof(Tune), Namespace = "http://jabber.org/protocol/tune")]
        [XmlElementAttribute("mood", typeof(Mood), Namespace = "http://jabber.org/protocol/mood")]
        public object Item
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlAttributeAttribute("id")]
        public string Id
        {
            get;
            set;
        }
    }
}

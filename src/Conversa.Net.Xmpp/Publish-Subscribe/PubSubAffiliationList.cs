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
    [XmlRootAttribute("affiliations", Namespace = "http://jabber.org/protocol/pubsub", IsNullable = false)]
    public partial class PubSubAffiliationList
    {
        /// <remarks/>
        [XmlElementAttribute("affiliation")]
        public List<PubSubAffiliation> Affiliations
        {
            get;
            private set;
        }

        public PubSubAffiliationList()
        {
            this.Affiliations = new List<PubSubAffiliation>();
        }
    }
}

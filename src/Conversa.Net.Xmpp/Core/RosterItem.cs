// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Core
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    /// <summary>
    /// Roster Management
    /// </summary>
    /// <remarks>
    /// RFC 6121:  Instant Messaging and Presence
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "jabber:iq:roster")]
    [XmlRootAttribute("item", Namespace = "jabber:iq:roster", IsNullable = false)]
    public partial class RosterItem
    {
        /// <remarks/>
        [XmlElementAttribute("group")]
        public List<string> Groups
        {
            get;
            private set;
        }

        /// <remarks/>
        [XmlAttributeAttribute("approved")]
        public bool Approved
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlIgnoreAttribute]
        public bool ApprovedSpecified
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlAttributeAttribute("ask")]
        public RosterAsk Ask
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlIgnoreAttribute]
        public bool AskSpecified
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
        [XmlAttributeAttribute("name")]
        public string Name
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlAttributeAttribute("subscription")]
        public RosterSubscriptionType Subscription
        {
            get;
            set;
        }

        public RosterItem()
        {
            this.Subscription = RosterSubscriptionType.None;
            this.Groups       = new List<string>();
        }
    }
}

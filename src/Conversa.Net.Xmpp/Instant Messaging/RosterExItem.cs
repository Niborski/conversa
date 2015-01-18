// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.InstantMessaging
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Xml.Serialization;

    /// <summary>
    /// Roster Item Exchange
    /// </summary>
    /// <remarks>
    /// XEP-0144: Roster Item Exchange
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://jabber.org/protocol/rosterx")]
    [XmlRootAttribute("item", Namespace = "http://jabber.org/protocol/rosterx", IsNullable = false)]
    public partial class RosterExItem
    {
        [XmlElementAttribute("group")]
        public List<string> Groups
        {
            get;
            private set;
        }

        [XmlAttribute("action")]
        [DefaultValueAttribute(RosterExItemAction.Add)]
        public RosterExItemAction Action
        {
            get;
            set;
        }

        [XmlAttribute("jid")]
        public string Jid
        {
            get;
            set;
        }

        [XmlAttribute("name")]
        public string name
        {
            get;
            set;
        }

        public RosterExItem()
        {
            this.Groups = new List<string>();
            this.Action = RosterExItemAction.Add;
        }
    }
}

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
    [XmlRootAttribute("query", Namespace = "jabber:iq:roster", IsNullable = false)]
    public partial class Roster
    {
        /// <remarks/>
        [XmlElementAttribute("item")]
        public List<RosterItem> Items
        {
            get;
            private set;
        }

        /// <remarks/>
        [XmlAttributeAttribute("ver")]
        public string Version
        {
            get;
            set;
        }

        public Roster()
        {
            this.Items = new List<RosterItem>();
        }
    }
}

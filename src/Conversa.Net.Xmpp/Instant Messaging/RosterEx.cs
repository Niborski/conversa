// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.InstantMessaging
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    /// <summary>
    /// Roster Item Exchange
    /// </summary>
    /// <remarks>
    /// XEP-0144: Roster Item Exchange
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://jabber.org/protocol/rosterx")]
    [XmlRootAttribute("x", Namespace = "http://jabber.org/protocol/rosterx", IsNullable = false)]
    public partial class RosterEx
    {
        [XmlElementAttribute("item")]
        public List<RosterExItem> Items
        {
            get;
            private set;
        }

        public RosterEx()
        {
            this.Items = new List<RosterExItem>();
        }
    }
}

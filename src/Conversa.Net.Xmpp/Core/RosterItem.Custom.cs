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
        /// <summary>
        /// Gets a value indicating whether subscription is "Pending Out"
        /// </summary>
        [XmlIgnoreAttribute]
        public bool IsPendingOut
        {
            get { return this.AskSpecified && this.Ask == RosterAsk.Subscribe; }
        }
    }
}
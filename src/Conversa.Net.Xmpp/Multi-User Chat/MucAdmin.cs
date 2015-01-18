// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.MultiUserChat
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    /// <summary>
    /// Multi-User Chat
    /// </summary>
    /// <remarks>
    /// XEP-0045: Multi-User Chat
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://jabber.org/protocol/muc#admin")]
    [XmlRootAttribute("query", Namespace = "http://jabber.org/protocol/muc#admin", IsNullable = false)]
    public partial class MucAdmin
    {
        [XmlElementAttribute("item")]
        public List<MucAdminItem> Items
        {
            get;
            set;
        }

        public MucAdmin()
        {
            this.Items = new List<MucAdminItem>();
        }
    }
}

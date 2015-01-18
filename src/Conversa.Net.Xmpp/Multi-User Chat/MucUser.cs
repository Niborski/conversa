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
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://jabber.org/protocol/muc#user")]
    [XmlRootAttribute("x", Namespace = "http://jabber.org/protocol/muc#user", IsNullable = false)]
    public partial class MucUser
    {
        [XmlElementAttribute("decline", typeof(MucUserDecline))]
        [XmlElementAttribute("destroy", typeof(MucUserDestroy))]
        [XmlElementAttribute("invite", typeof(MucUserInvite))]
        [XmlElementAttribute("item", typeof(MucUserItem))]
        [XmlElementAttribute("status", typeof(MucUserStatus))]
        [XmlElementAttribute("password", typeof(string))]
        public List<object> Items
        {
            get;
            set;
        }

        public MucUser()
        {
            this.Items = new List<object>();
        }
    }
}

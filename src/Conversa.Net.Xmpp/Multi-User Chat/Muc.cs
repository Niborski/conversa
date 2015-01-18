// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.MultiUserChat
{
    using System.Xml.Serialization;

    /// <summary>
    /// Multi-User Chat
    /// </summary>
    /// <remarks>
    /// XEP-0045: Multi-User Chat
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://jabber.org/protocol/muc")]
    [XmlRootAttribute("x", Namespace = "http://jabber.org/protocol/muc", IsNullable = false)]
    public partial class Muc
    {
        [XmlElementAttribute("history")]
        public MucHistory History
        {
            get;
            set;
        }

        [XmlElementAttribute("password")]
        public string Password
        {
            get;
            set;
        }

        public Muc()
        {
            this.History = new MucHistory();
        }
    }
}

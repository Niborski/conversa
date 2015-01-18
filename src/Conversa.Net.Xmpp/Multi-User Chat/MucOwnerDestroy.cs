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
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://jabber.org/protocol/muc#owner")]
    [XmlRootAttribute("destroy", Namespace = "http://jabber.org/protocol/muc#owner", IsNullable = false)]
    public partial class MucOwnerDestroy
    {
        [XmlElementAttribute("password")]
        public string Password
        {
            get;
            set;
        }

        [XmlElementAttribute("reason")]
        public string Reason
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

        public MucOwnerDestroy()
        {
        }
    }
}

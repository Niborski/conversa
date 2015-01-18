// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.MultiUserChat
{
    using Conversa.Net.Xmpp.DataForms;
    using System.Xml.Serialization;

    /// <summary>
    /// Multi-User Chat
    /// </summary>
    /// <remarks>
    /// XEP-0045: Multi-User Chat
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://jabber.org/protocol/muc#owner")]
    [XmlRootAttribute("query", Namespace = "http://jabber.org/protocol/muc#owner", IsNullable = false)]
    public partial class MucOwner
    {
        [XmlElementAttribute("destroy", typeof(MucOwnerDestroy))]
        [XmlElementAttribute("x", typeof(DataForm), Namespace = "jabber:x:data")]
        public object Item
        {
            get;
            set;
        }

        public MucOwner()
        {
        }
    }
}

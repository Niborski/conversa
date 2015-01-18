// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Core
{
    using System.Xml.Serialization;

    /// <summary>
    /// Message Stanza
    /// </summary>
    /// <remarks>
    /// RFC 6120: XMPP Core
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "jabber:client")]
    [XmlRootAttribute("thread", Namespace = "jabber:client", IsNullable = false)]
    public partial class MessageThread
    {
        [XmlAttributeAttribute("parent", DataType = "NMTOKEN")]
        public string Parent
        {
            get;
            set;
        }

        [XmlTextAttribute(DataType = "NMTOKEN")]
        public string Value
        {
            get;
            set;
        }

        public MessageThread()
        {
        }
    }
}

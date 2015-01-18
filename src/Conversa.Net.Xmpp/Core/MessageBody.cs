// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Core
{
    using Conversa.Net.Xmpp.Shared;
    using System.Xml.Serialization;

    /// <summary>
    /// Message Stanza
    /// </summary>
    /// <remarks>
    /// RFC 6120: XMPP Core
    /// </remarks>
    [XmlTypeAttribute(Namespace = "jabber:client")]
    [XmlRootAttribute("body", Namespace = "jabber:client", IsNullable = false)]
    public partial class MessageBody
        : Text
    {
        public MessageBody()
            : base()
        {
        }
    }
}

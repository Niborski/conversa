// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.XmppPing
{
    using System.Xml.Serialization;

    /// <summary>
    /// XMPP Ping
    /// </summary>
    /// <remarks>
    /// XEP-0199 XMPP Ping
    /// </remarks>
    [XmlTypeAttribute(Namespace = "urn:xmpp:ping")]
    [XmlRootAttribute("ping", Namespace = "urn:xmpp:ping", IsNullable = false)]
    public partial class Ping
    {
        public Ping()
        {
        }
    }
}

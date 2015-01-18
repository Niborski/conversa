// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Core
{
    using System.Xml.Serialization;

    /// <summary>
    /// Resource Binding
    /// </summary>
    /// <remarks>
    /// RFC 6120: XMPP Core
    /// </remarks>
    [XmlTypeAttribute(Namespace = "urn:ietf:params:xml:ns:xmpp-bind", IncludeInSchema = false)]
    public enum BindType
    {
        /// <remarks/>
        [XmlEnumAttribute("jid")]
        Jid,

        /// <remarks/>
        [XmlEnumAttribute("resource")]
        Resource,
    }
}

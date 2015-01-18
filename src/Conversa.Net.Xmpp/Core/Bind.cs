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
    [XmlTypeAttribute(Namespace = "urn:ietf:params:xml:ns:xmpp-bind")]
    [XmlRootAttribute("bind", Namespace = "urn:ietf:params:xml:ns:xmpp-bind", IsNullable = false)]
    public partial class Bind
    {
        /// <remarks/>
        [XmlElementAttribute("resource")]
        public string Resource
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("jid")]
        public string Jid
        {
            get;
            set;
        }

        public Bind()
        {
        }
    }
}

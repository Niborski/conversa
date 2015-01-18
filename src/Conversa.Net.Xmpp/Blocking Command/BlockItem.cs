// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Blocking
{
    using Conversa.Net.Xmpp.Shared;
    using System.Xml.Serialization;

    /// <summary>
    /// Blocking Command
    /// </summary>
    /// <remarks>
    /// XEP-0191: Blocking Command
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "urn:xmpp:blocking")]
    [XmlRootAttribute("item", Namespace = "urn:xmpp:blocking", IsNullable = false)]
    public partial class BlockItem
    {
        [XmlAttribute("jid")]
        public string Jid
        {
            get;
            set;
        }

        [XmlTextAttribute]
        public Empty Value
        {
            get;
            set;
        }

        public BlockItem()
        {
        }
    }
}

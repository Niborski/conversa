// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Blocking
{
    using Conversa.Net.Xmpp.Core;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    /// <summary>
    /// Blocking Command
    /// </summary>
    /// <remarks>
    /// XEP-0191: Blocking Command
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "urn:xmpp:blocking")]
    [XmlRootAttribute("Blocking", Namespace = "urn:xmpp:blocking", IsNullable = false)]
    public partial class Block
    {
        [XmlElementAttribute("item")]
        public List<BlockItem> Items
        {
            get;
            private set;
        }

        public Block()
        {
            this.Items = new List<BlockItem>();
        }

        public Block(XmppAddress address)
            : this()
        {
            this.Items.Add(new BlockItem { Jid = address.BareAddress });
        }

        public Block(BlockItem item)
            : this()
        {
            this.Items.Add(item);
        }

        public Block(BlockItem[] items)
            : this()
        {
            this.Items.AddRange(items);
        }
    }
}

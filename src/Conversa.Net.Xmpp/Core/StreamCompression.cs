// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Core
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    /// <summary>
    /// Stream Compression
    /// </summary>
    /// <remarks>
    /// XEP-0138 Stream Compression
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://jabber.org/protocol/compress")]
    [XmlRootAttribute("compress", Namespace = "http://jabber.org/protocol/compress", IsNullable = false)]
    public partial class StreamCompression
    {
        [XmlElementAttribute("method", typeof(string), DataType = "NCName")]
        public List<string> Methods
        {
            get;
            set;
        }

        public StreamCompression()
        {
        }
    }
}

// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Bookmarks
{
    using Conversa.Net.Xmpp.Shared;
    using System.Xml.Serialization;

    /// <summary>
    /// Bookmarks
    /// </summary>
    /// <remarks>
    /// XEP-0048: Bookmarks
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "storage:bookmarks")]
    [XmlRootAttribute("url", Namespace = "storage:bookmarks", IsNullable = false)]
    public partial class BookmarkUrl
    {
        [XmlAttribute("name")]
        public string Name
        {
            get;
            set;
        }

        [XmlAttributeAttribute("url")]
        public string Url
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

        public BookmarkUrl()
        {
        }
    }
}

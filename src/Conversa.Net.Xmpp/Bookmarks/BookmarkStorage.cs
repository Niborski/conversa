// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Bookmarks
{
    using System.Xml.Serialization;

    /// <summary>
    /// Bookmarks
    /// </summary>
    /// <remarks>
    /// XEP-0048: Bookmarks
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "storage:bookmarks")]
    [XmlRootAttribute("storage", Namespace = "storage:bookmarks", IsNullable = false)]
    public partial class BookmarkStorage
    {
        [XmlElementAttribute("conference", typeof(BookmarkConference))]
        [XmlElementAttribute("url", typeof(BookmarkUrl))]
        public object Item
        {
            get;
            set;
        }

        public BookmarkStorage()
        {
        }
    }
}

// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Bookmarks
{
    using System.ComponentModel;
    using System.Xml.Serialization;

    /// <summary>
    /// Bookmarks
    /// </summary>
    /// <remarks>
    /// XEP-0048: Bookmarks
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "storage:bookmarks")]
    [XmlRootAttribute("conference", Namespace = "storage:bookmarks", IsNullable = false)]
    public partial class BookmarkConference
    {
        [XmlElementAttribute("nick")]
        public string Nick
        {
            get;
            set;
        }

        [XmlElementAttribute("password")]
        public string Password
        {
            get;
            set;
        }

        [XmlAttribute("autojoin")]
        [DefaultValueAttribute(false)]
        public bool Autojoin
        {
            get;
            set;
        }

        [XmlAttribute("jid")]
        public string Jid
        {
            get;
            set;
        }

        [XmlAttribute("name")]
        public string Name
        {
            get;
            set;
        }

        public BookmarkConference()
        {
            this.Autojoin = false;
        }
    }
}

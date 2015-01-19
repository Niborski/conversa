// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Privacy
{
    using Conversa.Net.Xmpp.Shared;
    using System.Xml.Serialization;

    /// <summary>
    /// Privacy Lists
    /// </summary>
    /// <remarks>
    /// XEP-0016: Privacy Lists
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "jabber:iq:privacy")]
    [XmlRootAttribute("item", Namespace = "jabber:iq:privacy", IsNullable = false)]
    public partial class PrivacyItem
    {
        [XmlAttribute("action")]
        public PrivacyAction Action
        {
            get;
            set;
        }

        [XmlAttribute("order")]
        public uint Order
        {
            get;
            set;
        }

        [XmlAttribute("value")]
        public string Value
        {
            get;
            set;
        }

        [XmlElementAttribute("iq")]
        public Empty InfoQuery
        {
            get;
            set;
        }

        [XmlIgnore]
        public bool InfoQuerySpecified
        {
            get;
            set;
        }

        [XmlElementAttribute("message")]
        public Empty Message
        {
            get;
            set;
        }

        [XmlIgnore]
        public bool MessageSpecified
        {
            get;
            set;
        }

        [XmlElementAttribute("presence-in")]
        public Empty PresenceIn
        {
            get;
            set;
        }

        [XmlIgnore]
        public bool PresenceInSpecified
        {
            get;
            set;
        }

        [XmlElementAttribute("presence-out")]
        public Empty PresenceOut
        {
            get;
            set;
        }

        [XmlIgnore]
        public bool PresenceOutSpecified
        {
            get;
            set;
        }

        [XmlAttribute("type")]
        public PrivacyItemType Type
        {
            get;
            set;
        }

        [XmlIgnore]
        public bool TypeSpecified
        {
            get;
            set;
        }

        public PrivacyItem()
        {
        }
    }
}

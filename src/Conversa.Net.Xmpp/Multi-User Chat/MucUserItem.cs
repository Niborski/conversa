// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.MultiUserChat
{
    using Conversa.Net.Xmpp.Shared;
    using System.Xml.Serialization;

    /// <summary>
    /// Multi-User Chat
    /// </summary>
    /// <remarks>
    /// XEP-0045: Multi-User Chat
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://jabber.org/protocol/muc#user")]
    [XmlRootAttribute("item", Namespace = "http://jabber.org/protocol/muc#user", IsNullable = false)]
    public partial class MucUserItem
    {
        [XmlElementAttribute("actor", Namespace = "http://jabber.org/protocol/muc#user")]
        public MucActor Actor
        {
            get;
            set;
        }

        [XmlElementAttribute("reason")]
        public string Reason
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

        [XmlAttribute("nick")]
        public string Nick
        {
            get;
            set;
        }

        [XmlElementAttribute("continue", Namespace = "http://jabber.org/protocol/muc#user")]
        public Empty Continue
        {
            get;
            set;
        }

        [XmlIgnore]
        public bool ContinueSpecified
        {
            get;
            set;
        }

        [XmlAttribute("affiliation", Namespace = "http://jabber.org/protocol/muc#user")]
        public MucAffiliation Affiliation
        {
            get;
            set;
        }

        [XmlIgnore]
        public bool AffiliationSpecified
        {
            get;
            set;
        }

        [XmlAttribute("role", Namespace = "http://jabber.org/protocol/muc#user")]
        public MucRole Role
        {
            get;
            set;
        }

        [XmlIgnore]
        public bool RoleSpecified
        {
            get;
            set;
        }

        public MucUserItem()
        {
            this.Actor = new MucActor();
        }
    }
}

// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.MultiUserChat
{
    using System.Xml.Serialization;

    /// <summary>
    /// Multi-User Chat
    /// </summary>
    /// <remarks>
    /// XEP-0045: Multi-User Chat
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://jabber.org/protocol/muc#admin")]
    [XmlRootAttribute("item", Namespace = "http://jabber.org/protocol/muc#admin", IsNullable = false)]
    public partial class MucAdminItem
    {
        [XmlElementAttribute("actor", Namespace = "http://jabber.org/protocol/muc#admin")]
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

        [XmlAttribute("affiliation", Namespace = "http://jabber.org/protocol/muc#admin")]
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

        [XmlAttribute("role", Namespace = "http://jabber.org/protocol/muc#admin")]
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

        public MucAdminItem()
        {
            this.Actor = new MucActor();
        }
    }
}

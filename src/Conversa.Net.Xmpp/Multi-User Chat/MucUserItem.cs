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
        private Empty?          @continue;
        private MucAffiliation? affiliation;
        private MucRole?        role;

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
            get
            {
                if (this.@continue.HasValue)
                {
                    return this.@continue.Value;
                }
                else
                {
                    return default(Empty);
                }
            }
            set { this.@continue = value; }
        }

        [XmlIgnore]
        public bool ContinueSpecified
        {
            get { return this.@continue.HasValue; }
            set
            {
                if (!value)
                {
                    this.@continue = null;
                }
            }
        }

        [XmlAttribute("affiliation", Namespace = "http://jabber.org/protocol/muc#user")]
        public MucAffiliation Affiliation
        {
            get
            {
                if (this.affiliation.HasValue)
                {
                    return this.affiliation.Value;
                }
                else
                {
                    return default(MucAffiliation);
                }
            }
            set { this.affiliation = value; }
        }

        [XmlIgnore]
        public bool AffiliationSpecified
        {
            get { return this.affiliation.HasValue; }
            set
            {
                if (!value)
                {
                    this.affiliation = null;
                }
            }
        }

        [XmlAttribute("role", Namespace = "http://jabber.org/protocol/muc#user")]
        public MucRole Role
        {
            get
            {
                if (this.role.HasValue)
                {
                    return this.role.Value;
                }
                else
                {
                    return default(MucRole);
                }
            }
            set { this.role = value; }
        }

        [XmlIgnore]
        public bool RoleSpecified
        {
            get { return this.role.HasValue; }
            set
            {
                if (!value)
                {
                    this.role = null;
                }
            }
        }

        public MucUserItem()
        {
            this.Actor = new MucActor();
        }
    }
}

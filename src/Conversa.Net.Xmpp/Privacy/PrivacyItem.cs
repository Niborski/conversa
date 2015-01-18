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
        private Empty?           infoQuery;
        private Empty?           message;
        private Empty?           presenceIn;
        private Empty?           presenceOut;
        private PrivacyItemType? type;

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
            get
            {
                if (this.infoQuery.HasValue)
                {
                    return this.infoQuery.Value;
                }
                else
                {
                    return default(Empty);
                }
            }
            set { this.infoQuery = value; }
        }

        [XmlIgnore]
        public bool InfoQuerySpecified
        {
            get { return this.infoQuery.HasValue; }
            set
            {
                if (!value)
                {
                    this.infoQuery = null;
                }
            }
        }

        [XmlElementAttribute("message")]
        public Empty Message
        {
            get
            {
                if (this.message.HasValue)
                {
                    return this.message.Value;
                }
                else
                {
                    return default(Empty);
                }
            }
            set
            {
                this.message = value;
            }
        }

        [XmlIgnore]
        public bool MessageSpecified
        {
            get { return this.message.HasValue; }
            set
            {
                if (!value)
                {
                    this.message = null;
                }
            }
        }

        [XmlElementAttribute("presence-in")]
        public Empty PresenceIn
        {
            get
            {
                if (this.presenceIn.HasValue)
                {
                    return this.presenceIn.Value;
                }
                else
                {
                    return default(Empty);
                }
            }
            set { this.presenceIn = value; }
        }

        [XmlIgnore]
        public bool PresenceInSpecified
        {
            get { return this.presenceIn.HasValue; }
            set
            {
                if (!value)
                {
                    this.presenceIn = null;
                }
            }
        }

        [XmlElementAttribute("presence-out")]
        public Empty PresenceOut
        {
            get
            {
                if (this.presenceOut.HasValue)
                {
                    return this.presenceOut.Value;
                }
                else
                {
                    return default(Empty);
                }
            }
            set
            {
                this.presenceOut = value;
            }
        }

        [XmlIgnore]
        public bool PresenceOutSpecified
        {
            get { return this.presenceOut.HasValue; }
            set
            {
                if (!value)
                {
                    this.presenceOut = null;
                }
            }
        }

        [XmlAttribute("type")]
        public PrivacyItemType Type
        {
            get
            {
                if (this.type.HasValue)
                {
                    return this.type.Value;
                }
                else
                {
                    return default(PrivacyItemType);
                }
            }
            set { this.type = value; }
        }

        [XmlIgnore]
        public bool TypeSpecified
        {
            get { return this.type.HasValue; }
            set
            {
                if (!value)
                {
                    this.type = null;
                }
            }
        }

        public PrivacyItem()
        {
        }
    }
}

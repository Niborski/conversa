// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Core
{
    using Conversa.Net.Xmpp.Caps;
    using Conversa.Net.Xmpp.InstantMessaging;
    using Conversa.Net.Xmpp.MultiUserChat;
    using System.Collections.Generic;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    /// <summary>
    /// Presence Stanza
    /// </summary>
    /// <remarks>
    /// RFC 6121:  Instant Messaging and Presence
    /// </remarks>
    [XmlType(Namespace = "jabber:client")]
    [XmlRoot("presence", Namespace = "jabber:client", IsNullable = false)]
    public partial class Presence
    {
        /// <remarks/>
        [XmlElementAttribute("show")]
        public ShowType Show
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("status")]
        public Status Status
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("priority")]
        public sbyte Priority
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("c", Type = typeof(EntityCapabilities), Namespace = "http://jabber.org/protocol/caps")]
        [XmlElementAttribute("x", Type = typeof(VCardAvatar), Namespace = "vcard-temp:x:update")]
        [XmlElementAttribute("x", Type = typeof(Muc), Namespace = "http://jabber.org/protocol/muc")]
        [XmlElementAttribute("x", Type = typeof(MucUser), Namespace = "http://jabber.org/protocol/muc#user")]
        public List<object> Items
        {
            get;
            private set;
        }

        /// <remarks/>
        [XmlElementAttribute("error")]
        public StanzaError Error
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlAttributeAttribute("from")]
        public string From
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlAttributeAttribute("id", DataType = "NMTOKEN")]
        public string Id
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlAttributeAttribute("to")]
        public string To
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlAttributeAttribute("type")]
        public PresenceType Type
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlIgnoreAttribute]
        public bool TypeSpecified
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlAttributeAttribute("lang", Form = XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/XML/1998/namespace")]
        public string Lang
        {
            get;
            set;
        }

        public Presence()
        {
            this.Id    = IdentifierGenerator.Generate();
            this.Items = new List<object>();
        }
    }
}

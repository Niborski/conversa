// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Core
{
    using Conversa.Net.Xmpp.AdHocCommands;
    using Conversa.Net.Xmpp.Blocking;
    using Conversa.Net.Xmpp.Discovery;
    using Conversa.Net.Xmpp.InBandRegistration;
    using Conversa.Net.Xmpp.InstantMessaging;
    using Conversa.Net.Xmpp.LastActivity;
    using Conversa.Net.Xmpp.MultiUserChat;
    using Conversa.Net.Xmpp.PublishSubscribe;
    using Conversa.Net.Xmpp.XmppPing;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    /// <summary>
    /// Info/Query (IQ)
    /// </summary>
    /// <remarks>
    /// RFC 6120: XMPP Core
    /// </remarks>
    [XmlType(Namespace = "jabber:client")]
    [XmlRoot("iq", Namespace = "jabber:client", IsNullable = false)]
    public partial class InfoQuery
    {
        /// <summary>
        /// enables completion of ad-hoc commands
        /// </summary>
        /// <remarks>
        /// XEP-0050
        /// </remarks>
        [XmlElementAttribute("command", Namespace = "http://jabber.org/protocol/commands")]
        public Command Command
        {
            get;
            set;
        }

        /// <summary>
        /// defines an XMPP protocol extension for communicating information about the last activity associated with an XMPP entity
        /// </summary>
        /// <remarks>
        /// XEP-0012: Last Activity
        /// </remarks>
        [XmlElementAttribute("query", Namespace = "jabber:iq:last")]
        public LastActivity LastActivity
        {
            get;
            set;
        }

        /// <summary>
        /// enables interaction for the purpose of service discovery
        /// </summary>
        /// <remarks>
        /// XEP-0030
        /// </remarks>
        [XmlElementAttribute("query", Namespace = "http://jabber.org/protocol/disco#info")]
        public ServiceInfo ServiceInfo
        {
            get;
            set;
        }

        /// <summary>
        /// enables interaction for the purpose of service discovery
        /// </summary>
        /// <remarks>
        /// XEP-0030
        /// </remarks>
        [XmlElementAttribute("query", Namespace = "http://jabber.org/protocol/disco#items")]
        public ServiceItem ServiceItem
        {
            get;
            set;
        }

        /// <summary>
        /// enables administration of multi user chat rooms
        /// </summary>
        /// <remarks>
        /// XEP-0045
        /// </remarks>
        [XmlElementAttribute("query", Namespace = "http://jabber.org/protocol/muc#admin")]
        public MucAdmin MucAdmin
        {
            get;
            set;
        }

        /// <summary>
        /// enables interaction with a publish-subscribe service
        /// </summary>
        /// <remarks>
        /// XEP-0060
        /// </remarks>
        [XmlElementAttribute("pubsub", Namespace = "http://jabber.org/protocol/pubsub")]
        public PubSub PubSub
        {
            get;
            set;
        }

        /// <summary>
        /// enables registering with a server or service
        /// </summary>
        /// <remarks>
        /// XEP-0077
        /// </remarks>
        [XmlElementAttribute("query", Namespace = "jabber:iq:register")]
        public Register Register
        {
            get;
            set;
        }

        /// <summary>
        /// enables adding or editing a roster item
        /// </summary>
        /// <remarks>
        /// XEP-0147
        /// </remarks>
        [XmlElementAttribute("query", Namespace = "jabber:iq:roster")]
        public Roster Roster
        {
            get;
            set;
        }

        /// <summary>
        /// enables retrieval of an entity's vCard data
        /// </summary>
        /// <remarks>
        /// XEP-0054
        /// </remarks>
        [XmlElementAttribute("x", Namespace = "vcard-temp")]
        public VCardData VCardData
        {
            get;
            set;
        }

        [XmlElementAttribute("bind", Namespace = "urn:ietf:params:xml:ns:xmpp-bind")]
        public Bind Bind
        {
            get;
            set;
        }

        [XmlElementAttribute("session", Namespace = "urn:ietf:params:xml:ns:xmpp-session")]
        public Session Session
        {
            get;
            set;
        }

        [XmlElementAttribute("ping", Namespace = "urn:xmpp:ping")]
        public Ping Ping
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("x", Namespace = "http://jabber.org/protocol/rosterx")]
        public RosterEx RosterEx
        {
            get;
            set;
        }

        [XmlElementAttribute("blocklist", Namespace = "urn:xmpp:blocking")]
        public BlockList BlockList
        {
            get;
            set;
        }

        [XmlElementAttribute("block", Namespace = "urn:xmpp:blocking")]
        public Block Block
        {
            get;
            set;
        }

        [XmlElementAttribute("unblock", Namespace = "urn:xmpp:blocking")]
        public Unblock Unblock
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("error", Namespace = "jabber:client")]
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
        public InfoQueryType Type
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

        public InfoQuery()
        {
            this.Id = IdentifierGenerator.Generate();
        }
    }
}

// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Core
{
    using Conversa.Net.Xmpp.ChatStates;
    using Conversa.Net.Xmpp.DataForms;
    using Conversa.Net.Xmpp.DelayedDelivery;
    using Conversa.Net.Xmpp.MultiUserChat;
    using Conversa.Net.Xmpp.PublishSubscribe;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    /// <summary>
    /// Message Stanza
    /// </summary>
    /// <remarks>
    /// RFC 6120: XMPP Core
    /// </remarks>
    [XmlTypeAttribute(Namespace = "jabber:client")]
    [XmlRootAttribute("message", Namespace = "jabber:client", IsNullable = false)]
    public partial class Message
    {
        /// <remarks/>
        [XmlElementAttribute("subject", typeof(MessageSubject))]
        public MessageSubject Subject
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("body", typeof(MessageBody))]
        public MessageBody Body
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("thread", typeof(MessageThread))]
        public MessageThread Thread
        {
            get;
            set;
        }

        /// <summary>
        /// enables mediated invitations to multi user chat rooms
        /// </summary>
        /// <remarks>
        /// XEP-0045
        /// </remarks>
        [XmlElementAttribute("x", Namespace = "http://jabber.org/protocol/muc#user")]
        public MucUser MucUser
        {
            get;
            set;
        }

        /// <summary>
        /// Delayed delivery timestamp
        /// </summary>
        [XmlElementAttribute("delay", Namespace = "urn:xmpp:delay")]
        public Delay Delay
        {
            get;
            set;
        }

        /// <summary>
        /// Allows to send voice requests to multi user chat rooms
        /// </summary>
        /// <remarks>
        /// XEP-0045
        /// </remarks>
        [XmlElementAttribute("x", Namespace = "jabber:x:data")]
        public DataForm MucRequests
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("active", typeof(ActiveChatState), Namespace = "http://jabber.org/protocol/chatstates")]
        [XmlElementAttribute("composing", typeof(ComposingChatState), Namespace = "http://jabber.org/protocol/chatstates")]
        [XmlElementAttribute("gone", typeof(GoneChatState), Namespace = "http://jabber.org/protocol/chatstates")]
        [XmlElementAttribute("inactive", typeof(InactiveChatState), Namespace = "http://jabber.org/protocol/chatstates")]
        [XmlElementAttribute("paused", typeof(PausedChatState), Namespace = "http://jabber.org/protocol/chatstates")]
        [XmlElementAttribute("event", typeof(PubSubEvent), Namespace = "http://jabber.org/protocol/pubsub#event")]
        public List<Object> Items
        {
            get;
            private set;
        }

        /// <remarks/>
        [XmlElement("error")]
        public StanzaError Error
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlIgnore]
        public XmppAddress FromAddress
        {
            get;
            private set;
        }

        private string from;
        [XmlAttributeAttribute("from")]
        public string From
        {
            get { return from; }
            set
            {
                this.from        = value;
                this.FromAddress = value;    
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute("id", DataType = "NMTOKEN")]
        public string Id
        {
            get;
            set;
        }

        [XmlIgnore]
        public string ToAddress
        {
            get;
            set;
        }

        private string to;
        /// <remarks/>
        [XmlAttributeAttribute("to")]
        public string To
        {
            get { return to; }
            set
            {
                this.to        = value;
                this.ToAddress = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute("type")]
        [DefaultValueAttribute(MessageType.Normal)]
        public MessageType Type
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

        public Message()
        {
            this.Id    = IdentifierGenerator.Generate();
            this.Type  = MessageType.Normal;
            this.Items = new List<object>();
        }
    }
}

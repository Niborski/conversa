// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Core
{
    using Conversa.Net.Xmpp.InstantMessaging;
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

        /// <remarks/>
        [XmlElementAttribute("active", typeof(ChatStateActive), Namespace = "http://jabber.org/protocol/chatstates")]
        [XmlElementAttribute("composing", typeof(ChatStateComposing), Namespace = "http://jabber.org/protocol/chatstates")]
        [XmlElementAttribute("gone", typeof(ChatStateGone), Namespace = "http://jabber.org/protocol/chatstates")]
        [XmlElementAttribute("inactive", typeof(ChatStateInactive), Namespace = "http://jabber.org/protocol/chatstates")]
        [XmlElementAttribute("paused", typeof(ChatStatePaused), Namespace = "http://jabber.org/protocol/chatstates")]
        [XmlElementAttribute("event", typeof(PubSubEvent), Namespace = "http://jabber.org/protocol/pubsub#event")]
        [XmlElementAttribute("x", Type = typeof(MucUser), Namespace = "http://jabber.org/protocol/muc#user")]
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

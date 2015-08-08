// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.Core;
using System;
using System.Collections.Generic;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Conversa.Net.Xmpp.Bosh
{
    /// <summary>
    /// XEP-0124: Bidirectional-streams Over Synchronous HTTP (BOSH)
    /// XEP-0206: XMPP Over BOSH
    /// </summary>
    [XmlTypeAttribute("body", AnonymousType = true, Namespace = "http://jabber.org/protocol/httpbind")]
    [XmlRootAttribute(ElementName="body", Namespace = "http://jabber.org/protocol/httpbind", IsNullable = false)]
    public partial class HttpBind
    {
        /// <remarks/>
        [XmlElementAttribute("stream:error", Type = typeof(StreamError), Namespace = "jabber:client", Form = XmlSchemaForm.Qualified)]
        [XmlElementAttribute("failure", Type = typeof(SaslFailure), Namespace = "urn:ietf:params:xml:ns:xmpp-sasl")]
        [XmlElementAttribute("auth", Type = typeof(SaslAuth), Namespace = "urn:ietf:params:xml:ns:xmpp-sasl")]
        [XmlElementAttribute("challenge", Type = typeof(SaslChallenge), Namespace = "urn:ietf:params:xml:ns:xmpp-sasl")]
        [XmlElementAttribute("response", Type = typeof(SaslResponse), Namespace = "urn:ietf:params:xml:ns:xmpp-sasl")]
        [XmlElementAttribute("success", Type = typeof(SaslSuccess), Namespace = "urn:ietf:params:xml:ns:xmpp-sasl")]
        [XmlElementAttribute("features", Type = typeof(StreamFeatures), Namespace = "http://etherx.jabber.org/streams", Form = XmlSchemaForm.Qualified)]
        [XmlElementAttribute("iq", Type = typeof(InfoQuery), Namespace = "jabber:client")]
        [XmlElementAttribute("presence", Type = typeof(Presence), Namespace = "jabber:client")]
        [XmlElementAttribute("message", Type = typeof(Message), Namespace = "jabber:client")]
        [XmlElementAttribute("uri", typeof(string))]
        public List<Object> Items
        {
            get;
            private set;
        }

        /// <remarks/>
        [XmlAttributeAttribute("accept")]
        public string Accept
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlAttributeAttribute("ack", DataType = "positiveInteger")]
        public string Ack
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlAttributeAttribute("authid")]
        public string AuthId
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlAttributeAttribute("charsets", DataType = "NMTOKENS")]
        public string Charsets
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlAttributeAttribute("condition")]
        public HttpBindCondition Condition
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlIgnoreAttribute]
        public bool ConditionSpecified
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlAttributeAttribute("content")]
        public string Content
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
        [XmlAttributeAttribute("hold")]
        public byte Hold
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlIgnoreAttribute]
        public bool HoldSpecified
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlAttributeAttribute("inactivity")]
        public short Inactivity
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool InactivitySpecified
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlAttributeAttribute("key")]
        public string Key
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlAttributeAttribute("maxpause")]
        public short MaxPause
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool MaxPauseSpecified
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlAttributeAttribute("newkey")]
        public string NewKey
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlAttributeAttribute("pause")]
        public short Pause
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool PauseSpecified
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlAttributeAttribute("polling")]
        public short Polling
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool PollingSpecified
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlAttributeAttribute("report", DataType = "positiveInteger")]
        public string Report
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlAttributeAttribute("requests")]
        public byte Requests
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool RequestsSpecified
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlAttributeAttribute("rid", DataType = "positiveInteger")]
        public string Rid
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlAttributeAttribute("route")]
        public string Route
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlAttributeAttribute("sid")]
        public string Sid
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlAttributeAttribute("stream")]
        public string Stream
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlAttributeAttribute("time")]
        public short Time
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlIgnoreAttribute]
        public bool TimeSpecified
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
        public HttpBindType Type
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
        [XmlAttributeAttribute("ver")]
        public string Ver
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlAttributeAttribute("wait")]
        public short Wait
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlIgnoreAttribute]
        public bool WaitSpecified
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

        [XmlAttributeAttribute("restart", Form = XmlSchemaForm.Qualified, Namespace = "urn:xmpp:xbosh")]
        public bool Restart
        {
            get;
            set;
        }

        [XmlAttributeAttribute("restartlogic", Form = XmlSchemaForm.Qualified, Namespace = "urn:xmpp:xbosh")]
        public bool RestartLogicField
        {
            get;
            set;
        }

        [XmlAttributeAttribute("version", Form = XmlSchemaForm.Qualified, Namespace = "urn:xmpp:xbosh")]
        public string VersionField
        {
            get;
            set;
        }

        public HttpBind()
        {
            this.Items = new List<Object>();
        }
    }
}

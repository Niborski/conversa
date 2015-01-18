// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Core
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    /// <remarks/>
    [XmlTypeAttribute(Namespace = "http://etherx.jabber.org/streams")]
    [XmlRootAttribute("stream", Namespace = "http://etherx.jabber.org/streams", IsNullable = false)]
    public class Stream
    {
        /// <remarks/>
        [XmlElementAttribute("features")]
        public StreamFeatures Features
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("presence", typeof(Presence), Namespace = "jabber:client")]
        [XmlElementAttribute("iq", typeof(InfoQuery), Namespace = "jabber:client")]
        [XmlElementAttribute("message", typeof(Message), Namespace = "jabber:client")]
        public List<object> Items
        {
            get;
            private set;
        }

        /// <remarks/>
        [XmlElementAttribute("error")]
        public StreamError Error
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
        [XmlAttributeAttribute("version")]
        public string Version
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

        public Stream()
        {
            this.Items = new List<Object>();
        }
    }
}

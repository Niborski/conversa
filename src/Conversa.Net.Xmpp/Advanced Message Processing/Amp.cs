// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.AdvancedMessageProcessing
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    /// <summary>
    /// Advanced Message Processing
    /// </summary>
    /// <remarks>
    /// XEP-0079: Advanced Message Processing
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://jabber.org/protocol/amp")]
    [XmlRootAttribute("amp", Namespace = "http://jabber.org/protocol/amp", IsNullable = false)]
    public partial class Amp
    {
        [XmlElementAttribute("rule")]
        public List<AmpRule> Rules
        {
            get;
            private set;
        }

        [XmlAttribute("from")]
        public string From
        {
            get;
            set;
        }

        [XmlAttributeAttribute("per-hop")]
        public bool PerHop
        {
            get;
            set;
        }

        [XmlAttributeAttribute("status", DataType = "NCName")]
        public string Status
        {
            get;
            set;
        }

        [XmlAttribute("to")]
        public string To
        {
            get;
            set;
        }

        public Amp()
        {
            this.Rules  = new List<AmpRule>();
            this.PerHop = false;
        }
    }
}

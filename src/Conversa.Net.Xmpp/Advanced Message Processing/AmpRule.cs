// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.AdvancedMessageProcessing
{
    using System.Xml.Serialization;

    /// <summary>
    /// Advanced Message Processing
    /// </summary>
    /// <remarks>
    /// XEP-0079: Advanced Message Processing
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://jabber.org/protocol/amp")]
    [XmlRootAttribute(Namespace = "http://jabber.org/protocol/amp", IsNullable = false)]
    public partial class AmpRule
    {
        [XmlAttributeAttribute("action", DataType = "NCName")]
        public string Action
        {
            get;
            set;
        }

        [XmlAttributeAttribute("condition", DataType = "NCName")]
        public string Condition
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

        public AmpRule()
        {
        }
    }
}

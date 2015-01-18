// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.DelayedDelivery
{
    using System.Xml.Serialization;

    /// <summary>
    /// Delayed Delivery
    /// </summary>
    /// <remarks>
    /// XEP-0203 Delayed Delivery
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "urn:xmpp:delay")]
    [XmlRootAttribute("delay", Namespace = "urn:xmpp:delay", IsNullable = false)]
    public partial class DelayedDelivery
    {
        [XmlAttribute("from")]
        public string From
        {
            get;
            set;
        }

        [XmlAttribute("stamp")]
        public string Stamp
        {
            get;
            set;
        }

        [XmlTextAttribute]
        public string Value
        {
            get;
            set;
        }

        public DelayedDelivery()
        {
        }
    }
}

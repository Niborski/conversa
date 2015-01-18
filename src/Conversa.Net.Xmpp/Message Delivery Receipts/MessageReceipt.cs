// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.MessageDelivery
{
    using Conversa.Net.Xmpp.Shared;
    using System.Xml.Serialization;

    /// <summary>
    /// Message Delivery Receipts
    /// </summary>
    /// <remarks>
    /// XEP-0184: Message Delivery Receipts
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "urn:xmpp:receipts")]
    [XmlRootAttribute("received", Namespace = "urn:xmpp:receipts", IsNullable = false)]
    public partial class MessageReceipt
    {
        [XmlAttribute("id")]
        public string Id
        {
            get;
            set;
        }

        [XmlTextAttribute]
        public Empty Value
        {
            get;
            set;
        }

        public MessageReceipt()
        {
        }
    }
}

// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.InBandRegistration
{
    using System.Xml.Serialization;

    /// <summary>
    /// In-Band Registration
    /// </summary>
    /// <remarks>
    /// XEP-0077 In-Band Registration
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "jabber:x:oob")]
    public partial class OutOfBandAddress
    {
        [XmlElementAttribute("url")]
        public string Url
        {
            get;
            set;
        }

        [XmlElementAttribute("desc")]
        public string Description
        {
            get;
            set;
        }

        public OutOfBandAddress()
        {
        }
    }
}

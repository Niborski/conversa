// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Core
{
    using System.Xml.Schema;
    using System.Xml.Serialization;

    /// <summary>
    /// User Nickname
    /// </summary>
    /// <remarks>
    /// XEP-0172: User Nickname
    /// </remarks>
    [XmlTypeAttribute(Namespace = "http://jabber.org/protocol/nick")]
    [XmlRootAttribute("nick", Namespace = "http://jabber.org/protocol/nick", IsNullable = false)]
    public partial class Nickname
    {
        /// <remarks/>
        [XmlAttributeAttribute("nick", Form = XmlSchemaForm.Qualified, Namespace = "http://jabber.org/protocol/nick")]
        public string Nick
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlTextAttribute]
        public string Value
        {
            get;
            set;
        }

        public Nickname()
        {
        }
    }
}

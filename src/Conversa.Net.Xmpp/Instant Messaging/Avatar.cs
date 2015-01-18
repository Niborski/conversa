// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using System.Xml.Serialization;

namespace Conversa.Net.Xmpp.InstantMessaging
{
    [XmlTypeAttribute(Namespace = "")]
    [XmlRootAttribute("avatar", Namespace = "", IsNullable = false)]
    public sealed class Avatar
    {
        private string contact;
        private string hash;

        [XmlElement("contact")]
        public string Contact
        {
            get { return this.contact; }
            set { this.contact = value; }
        }

        [XmlElement("hash")]
        public string Hash
        {
            get { return this.hash; }
            set { this.hash = value; }
        }

        public Avatar()
        {
        }

        public Avatar(string contactId, string hash)
        {
            this.contact    = contactId;
            this.hash       = hash;
        }
    }
}

// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.InstantMessaging
{
    using System.Xml.Serialization;

    /// <summary>
    /// vCards
    /// </summary>
    /// <remarks>
    /// XEP-0054: vcard-temp
    /// </remarks>
    [XmlTypeAttribute(Namespace = "vcard-temp")]
    [XmlRootAttribute("x", Namespace = "vcard-temp", IsNullable = false)]
    public partial class VCardData
    {
        [XmlElementAttribute("NICKNAME")]
        public string NickName
        {
            get;
            set;
        }

        [XmlElementAttribute("JABBERID")]
        public string Address
        {
            get;
            set;
        }

        [XmlElementAttribute("PHOTO")]
        public VCardPhoto Photo
        {
            get;
            set;
        }

        public VCardData()
        {
            this.Photo = new VCardPhoto();
        }
    }
}

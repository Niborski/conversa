// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.InstantMessaging
{
    using System.Xml.Serialization;

    /// <summary>
    /// vCard-Based Avatars
    /// </summary>
    /// <remarks>
    /// XEP-0153: vCard-Based Avatars
    /// </remarks>
    [XmlTypeAttribute(Namespace = "vcard-temp")]
    [XmlRootAttribute("PHOTO", Namespace = "vcard-temp", IsNullable = false)]
    public partial class VCardPhoto
    {
        [XmlElementAttribute("TYPE")]
        public string Type
        {
            get;
            set;
        }

        [XmlElementAttribute("BINVAL", DataType = "base64Binary")]
        public byte[] Photo
        {
            get;
            set;
        }

        public VCardPhoto()
        {
        }
    }
}

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
    [XmlTypeAttribute(Namespace = "vcard-temp:x:update")]
    [XmlRootAttribute("x", Namespace = "vcard-temp:x:update", IsNullable = false)]
    public partial class VCardAvatar
    {
        [XmlElementAttribute("photo")]
        public string Photo
        {
            get;
            set;
        }

        public VCardAvatar()
        {
        }
    }
}

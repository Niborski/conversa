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
    [XmlTypeAttribute(Namespace = "jabber:iq:register", IncludeInSchema = false)]
    public enum RegisterType
    {
        /// <remarks/>
        [XmlEnumAttribute("address")]
        Address,

        /// <remarks/>
        [XmlEnumAttribute("city")]
        City,

        /// <remarks/>
        [XmlEnumAttribute("date")]
        Date,

        /// <remarks/>
        [XmlEnumAttribute("email")]
        Email,

        /// <remarks/>
        [XmlEnumAttribute("first")]
        First,

        /// <remarks/>
        [XmlEnumAttribute("instructions")]
        Instructions,

        /// <remarks/>
        [XmlEnumAttribute("key")]
        Key,

        /// <remarks/>
        [XmlEnumAttribute("last")]
        Last,

        /// <remarks/>
        [XmlEnumAttribute("misc")]
        Misc,

        /// <remarks/>
        [XmlEnumAttribute("name")]
        Name,

        /// <remarks/>
        [XmlEnumAttribute("nick")]
        Nick,

        /// <remarks/>
        [XmlEnumAttribute("password")]
        Password,

        /// <remarks/>
        [XmlEnumAttribute("phone")]
        Phone,

        /// <remarks/>
        [XmlEnumAttribute("registered")]
        Registered,

        /// <remarks/>
        [XmlEnumAttribute("remove")]
        Remove,

        /// <remarks/>
        [XmlEnumAttribute("state")]
        State,

        /// <remarks/>
        [XmlEnumAttribute("text")]
        Text,

        /// <remarks/>
        [XmlEnumAttribute("url")]
        Url,

        /// <remarks/>
        [XmlEnumAttribute("username")]
        Username,

        /// <remarks/>
        [XmlEnumAttribute("zip")]
        Zip,
    }
}

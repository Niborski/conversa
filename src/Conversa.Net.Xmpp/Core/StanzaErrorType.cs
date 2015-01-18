// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Core
{
    using System.Xml.Serialization;

    /// <remarks/>
    [XmlTypeAttribute(Namespace = "jabber:client")]
    public enum StanzaErrorType
    {
        /// <remarks/>
        [XmlEnumAttribute("auth")]
        Auth,

        /// <remarks/>
        [XmlEnumAttribute("cancel")]
        Cancel,

        /// <remarks/>
        [XmlEnumAttribute("continue")]
        Continue,

        /// <remarks/>
        [XmlEnumAttribute("modify")]
        Modify,

        /// <remarks/>
        [XmlEnumAttribute("wait")]
        Wait,
    }
}

// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Core
{
    using System.Xml.Serialization;

    /// <remarks/>
    [XmlType(Namespace = "jabber:client")]
    [XmlRoot("show", Namespace = "jabber:client", IsNullable = false)]
    public enum ShowType
    {
        /// <remarks/>
        [XmlIgnoreAttribute]
        Offline = -1,

        /// <remarks/>
        [XmlEnumAttribute("away")]
        Away = 0,

        /// <remarks/>
        [XmlEnumAttribute("chat")]
        Online = 1,

        /// <remarks/>
        [XmlEnumAttribute("dnd")]
        Busy = 2,

        /// <remarks/>
        [XmlEnumAttribute("xa")]
        ExtendedAway = 4
    }
}

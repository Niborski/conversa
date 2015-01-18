// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.DataForms.Validation
{
    using System.Xml.Serialization;

    /// <summary>
    /// Data Forms Validation
    /// </summary>
    /// <remarks>
    /// XEP-0122: Data Forms Validation
    /// </remarks>
    [XmlTypeAttribute(Namespace = "http://jabber.org/protocol/xdata-validate", IncludeInSchema = false)]
    public enum DataFormValidationType
    {
        /// <remarks/>
        [XmlEnumAttribute("basic")]
        Basic,

        /// <remarks/>
        [XmlEnumAttribute("open")]
        Open,

        /// <remarks/>
        [XmlEnumAttribute("range")]
        Range,

        /// <remarks/>
        [XmlEnumAttribute("regex")]
        Regex,
    }
}

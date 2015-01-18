// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.DataForms
{
    using System.Xml.Serialization;

    /// <summary>
    /// XMPP Data Forms
    /// </summary>
    /// <remarks>
    /// XEP-0004: Data Forms
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "jabber:x:data")]
    public enum DataFormType
    {
        /// <remarks/>
        [XmlEnumAttribute("cancel")]
        Cancel,

        /// <remarks/>
        [XmlEnumAttribute("form")]
        Form,

        /// <remarks/>
        [XmlEnumAttribute("result")]
        Result,

        /// <remarks/>
        [XmlEnumAttribute("submit")]
        Submit
    }
}

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
    [XmlRootAttribute("option", Namespace = "jabber:x:data", IsNullable = false)]
    public partial class DataFormFieldOption
    {
        [XmlElementAttribute("value")]
        public string Value
        {
            get;
            set;
        }

        [XmlAttribute("label")]
        public string Label
        {
            get;
            set;
        }

        public DataFormFieldOption()
        {
        }
    }
}

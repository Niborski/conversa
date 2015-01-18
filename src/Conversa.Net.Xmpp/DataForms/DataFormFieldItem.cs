// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.DataForms
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    /// <summary>
    /// XMPP Data Forms
    /// </summary>
    /// <remarks>
    /// XEP-0004: Data Forms
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "jabber:x:data")]
    [XmlRootAttribute("item", Namespace = "jabber:x:data", IsNullable = false)]
    public partial class DataFormFieldItem
    {
        [XmlElementAttribute("field")]
        public List<DataFormField> Fields
        {
            get;
            set;
        }

        public DataFormFieldItem()
        {
            this.Fields = new List<DataFormField>();
        }
    }
}

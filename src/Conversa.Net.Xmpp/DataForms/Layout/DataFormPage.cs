// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.DataForms.Layout
{
    using Conversa.Net.Xmpp.Shared;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    /// <summary>
    /// Data Forms Layout
    /// </summary>
    /// <remarks>
    /// XEP-0141: Data Forms Layout
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://jabber.org/protocol/xdata-layout")]
    [XmlRootAttribute("page", Namespace = "http://jabber.org/protocol/xdata-layout", IsNullable = false)]
    public partial class DataFormPage
    {
        [XmlElementAttribute("fieldref", typeof(DataFormFieldRef))]
        [XmlElementAttribute("reportedref", typeof(Empty))]
        [XmlElementAttribute("section", typeof(DataFormSection))]
        [XmlElementAttribute("text", typeof(string))]
        public List<object> Items
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

        public DataFormPage()
        {
            this.Items = new List<object>();
        }
    }
}

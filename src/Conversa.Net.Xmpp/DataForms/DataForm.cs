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
    [XmlRootAttribute("x", Namespace = "jabber:x:data", IsNullable = false)]
    public partial class DataForm
    {
        [XmlElementAttribute("instructions")]
        public List<string> Instructions
        {
            get;
            set;
        }

        [XmlElementAttribute("title")]
        public string Title
        {
            get;
            set;
        }

        [XmlElementAttribute("field")]
        public List<DataFormField> Fields
        {
            get;
            set;
        }

        [XmlArrayItemAttribute("field", IsNullable = false)]
        public List<DataFormField> ReportedFields
        {
            get;
            set;
        }

        [XmlArrayItemAttribute("field", typeof(DataFormField), IsNullable = false)]
        public List<DataFormField> Items
        {
            get;
            set;
        }

        [XmlAttribute("type")]
        public DataFormType Type
        {
            get;
            set;
        }

        public DataForm()
        {
            this.Items          = new List<DataFormField>();
            this.ReportedFields = new List<DataFormField>();
            this.Fields         = new List<DataFormField>();
        }
    }
}

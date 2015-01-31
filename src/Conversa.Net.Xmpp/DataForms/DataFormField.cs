// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.DataForms
{
    using Conversa.Net.Xmpp.Shared;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Xml.Serialization;

    /// <summary>
    /// XMPP Data Forms
    /// </summary>
    /// <remarks>
    /// XEP-0004: Data Forms
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "jabber:x:data")]
    [XmlRootAttribute("field", Namespace = "jabber:x:data", IsNullable = false)]
    public partial class DataFormField
    {
        [XmlElementAttribute("desc")]
        public string Description
        {
            get;
            set;
        }

        [XmlElementAttribute("value")]
        public List<string> Value
        {
            get;
            set;
        }

        [XmlElementAttribute("option")]
        public List<DataFormFieldOption> Options
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

        [XmlAttribute("type")]
        [DefaultValueAttribute(DataFormFieldType.TextSingle)]
        public DataFormFieldType Type
        {
            get;
            set;
        }

        [XmlAttribute("var")]
        public string Var
        {
            get;
            set;
        }

        [XmlAttribute("required")]
        public Empty Required
        {
            get;
            set;
        }

        [XmlIgnore]
        public bool RequiredSpecified
        {
            get;
            set;
        }

        public DataFormField()
        {
            this.Options = new List<DataFormFieldOption>();
            this.Type    = DataFormFieldType.TextSingle;
        }
    }
}

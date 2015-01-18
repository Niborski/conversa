// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.DataForms.Validation
{
    using Conversa.Net.Xmpp.Shared;
    using System.ComponentModel;
    using System.Xml.Serialization;

    /// <summary>
    /// Data Forms Validation
    /// </summary>
    /// <remarks>
    /// XEP-0122: Data Forms Validation
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://jabber.org/protocol/xdata-validate")]
    [XmlRootAttribute("validate", Namespace = "http://jabber.org/protocol/xdata-validate", IsNullable = false)]
    public partial class DataFormValidation
    {
        [XmlElementAttribute("basic", typeof(Empty), Namespace = "http://jabber.org/protocol/xdata-validate")]
        [XmlElementAttribute("open", typeof(Empty), Namespace = "http://jabber.org/protocol/xdata-validate")]
        [XmlElementAttribute("range", typeof(DataFormValidationRange))]
        [XmlElementAttribute("regex", typeof(string))]
        [XmlChoiceIdentifierAttribute("Type")]
        public object Item
        {
            get;
            set;
        }

        [XmlIgnore]
        public DataFormValidationType Type
        {
            get;
            set;
        }

        [XmlElementAttribute("list-range")]
        public DataFormValidationRangeList Ranges
        {
            get;
            set;
        }

        [XmlAttribute("datatype")]
        [DefaultValueAttribute("xs:string")]
        public string DataType
        {
            get;
            set;
        }

        public DataFormValidation()
        {
            this.Ranges = new DataFormValidationRangeList();
            this.DataType = "xs:string";
        }
    }
}

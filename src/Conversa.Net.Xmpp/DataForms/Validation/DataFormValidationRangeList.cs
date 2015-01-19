// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.DataForms.Validation
{
    using Conversa.Net.Xmpp.Shared;
    using System.Xml.Serialization;

    /// <summary>
    /// Data Forms Validation
    /// </summary>
    /// <remarks>
    /// XEP-0122: Data Forms Validation
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://jabber.org/protocol/xdata-validate")]
    [XmlRootAttribute("list-range", Namespace = "http://jabber.org/protocol/xdata-validate", IsNullable = false)]
    public partial class DataFormValidationRangeList
    {
        [XmlTextAttribute]
        public Empty Value
        {
            get;
            set;
        }

        [XmlAttribute("min")]
        public uint Min
        {
            get;
            set;
        }

        [XmlIgnore]
        public bool MinSpecified
        {
            get;
            set;
        }

        [XmlAttribute("max")]
        public uint Max
        {
            get;
            set;
        }

        [XmlIgnore]
        public bool MaxSpecified
        {
            get;
            set;
        }

        public DataFormValidationRangeList()
        {
        }
    }
}

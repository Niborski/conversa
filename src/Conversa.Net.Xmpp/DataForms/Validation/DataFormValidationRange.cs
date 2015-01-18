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
    [XmlRootAttribute("range", Namespace = "http://jabber.org/protocol/xdata-validate", IsNullable = false)]
    public partial class DataFormValidationRange
    {
        [XmlAttribute("min")]
        public string Min
        {
            get;
            set;
        }

        [XmlAttribute("max")]
        public string Max
        {
            get;
            set;
        }

        [XmlTextAttribute]
        public Empty Value
        {
            get;
            set;
        }

        public DataFormValidationRange()
        {
        }
    }
}

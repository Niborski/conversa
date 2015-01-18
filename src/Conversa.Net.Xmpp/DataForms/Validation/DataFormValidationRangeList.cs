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
        private uint? min;
        private uint? max;

        [XmlTextAttribute]
        public Empty Value
        {
            get;
            set;
        }

        [XmlAttribute("min")]
        public uint Min
        {
            get
            {
                if (this.min.HasValue)
                {
                    return this.min.Value;
                }
                else
                {
                    return default(uint);
                }
            }
            set { this.min = value; }
        }

        [XmlIgnore]
        public bool MinSpecified
        {
            get { return this.min.HasValue; }
            set
            {
                if (!value)
                {
                    this.min = null;
                }
            }
        }

        [XmlAttribute("max")]
        public uint Max
        {
            get
            {
                if (this.max.HasValue)
                {
                    return this.max.Value;
                }
                else
                {
                    return default(uint);
                }
            }
            set { this.max = value; }
        }

        [XmlIgnore]
        public bool MaxSpecified
        {
            get { return this.max.HasValue; }
            set
            {
                if (!value)
                {
                    this.max = null;
                }
            }
        }

        public DataFormValidationRangeList()
        {
        }
    }
}

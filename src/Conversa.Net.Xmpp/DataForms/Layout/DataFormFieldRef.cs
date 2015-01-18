// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.DataForms.Layout
{
    using Conversa.Net.Xmpp.Shared;
    using System.Xml.Serialization;

    /// <summary>
    /// Data Forms Layout
    /// </summary>
    /// <remarks>
    /// XEP-0141: Data Forms Layout
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://jabber.org/protocol/xdata-layout")]
    [XmlRootAttribute("fieldref", Namespace = "http://jabber.org/protocol/xdata-layout", IsNullable = false)]
    public partial class DataFormFieldRef
    {
        [XmlAttribute("var")]
        public string Var
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
    }
}

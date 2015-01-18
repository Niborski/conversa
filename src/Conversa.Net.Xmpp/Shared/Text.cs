// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Shared
{
    using System.Xml.Schema;
    using System.Xml.Serialization;

    [XmlTypeAttribute(AnonymousType = true)]
    public partial class Text
    {
        [XmlAttributeAttribute("lang", Form = XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/XML/1998/namespace")]
        public string Language
        {
            get;
            set;
        }

        [XmlTextAttribute]
        public string Value
        {
            get;
            set;
        }

        public Text()
        {
        }
    }
}

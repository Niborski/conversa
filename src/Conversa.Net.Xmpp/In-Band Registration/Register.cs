// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.InBandRegistration
{
    using Conversa.Net.Xmpp.DataForms;
    using System.Xml.Serialization;

    /// <summary>
    /// In-Band Registration
    /// </summary>
    /// <remarks>
    /// XEP-0077 In-Band Registration
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "jabber:iq:register")]
    [XmlRootAttribute("query", Namespace = "jabber:iq:register", IsNullable = false)]
    public partial class Register
    {
        /// <remarks/>
        [XmlElementAttribute("username")]
        public string UserName
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("password")]
        public string Password
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("remove")]
        public string Remove
        {
            get;
            set;
        }

        [XmlElementAttribute(Namespace = "jabber:x:data")]
        public DataForm Form
        {
            get;
            set;
        }

        [XmlElementAttribute("x", Namespace = "jabber:x:oob")]
        public OutOfBandAddress External
        {
            get;
            set;
        }

        //[XmlElementAttribute("address", typeof(string))]
        //[XmlElementAttribute("city", typeof(string))]
        //[XmlElementAttribute("date", typeof(string))]
        //[XmlElementAttribute("email", typeof(string))]
        //[XmlElementAttribute("first", typeof(string))]
        //[XmlElementAttribute("instructions", typeof(string))]
        //[XmlElementAttribute("key", typeof(string))]
        //[XmlElementAttribute("last", typeof(string))]
        //[XmlElementAttribute("misc", typeof(string))]
        //[XmlElementAttribute("name", typeof(string))]
        //[XmlElementAttribute("nick", typeof(string))]
        //[XmlElementAttribute("password", typeof(string))]
        //[XmlElementAttribute("phone", typeof(string))]
        //[XmlElementAttribute("registered", typeof(Empty))]
        //[XmlElementAttribute("remove", typeof(Empty))]
        //[XmlElementAttribute("state", typeof(string))]
        //[XmlElementAttribute("text", typeof(string))]
        //[XmlElementAttribute("url", typeof(string))]
        //[XmlElementAttribute("username", typeof(string))]
        //[XmlElementAttribute("zip", typeof(string))]
        //[XmlChoiceIdentifierAttribute("ItemsElementName")]
        //public object[] Items
        //{
        //    get;
        //    set;
        //}

        //[XmlElementAttribute("ItemsElementName")]
        //[XmlIgnore]
        //public RegisterType[] ItemsElementName
        //{
        //    get;
        //    set;
        //}

        public Register()
        {
            this.External = new OutOfBandAddress();
            this.Form     = new DataForm();
        }
    }
}

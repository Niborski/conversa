// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.AdHocCommands
{
    using System.Collections.Generic;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    /// <summary>
    /// Ad-Hoc Commands
    /// </summary>
    /// <remarks>
    /// XEP-0050: Ad-Hoc Commands
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://jabber.org/protocol/commands")]
    [XmlRootAttribute("command", Namespace = "http://jabber.org/protocol/commands", IsNullable = false)]
    public partial class Command
    {
        [XmlAnyElementAttribute]
        [XmlElementAttribute("actions", typeof(CommandActions))]
        [XmlElementAttribute("note", typeof(CommandNote))]
        public List<object> Items
        {
            get;
            set;
        }

        [XmlAttribute("node")]
        public string Node
        {
            get;
            set;
        }

        [XmlAttribute("sessionid")]
        public string SessionId
        {
            get;
            set;
        }

        [XmlAttributeAttribute("lang", Form = XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/XML/1998/namespace")]
        public string Lang
        {
            get;
            set;
        }

        [XmlAttribute("action")]
        public CommandActionType Action
        {
            get;
            set;
        }

        [XmlIgnore]
        public bool ActionSpecified
        {
            get;
            set;
        }

        [XmlAttribute("status")]
        public CommandStatus Status
        {
            get;
            set;
        }

        [XmlIgnore]
        public bool StatusSpecified
        {
            get;
            set;
        }

        public Command()
        {
            this.Items = new List<object>();
        }
    }
}

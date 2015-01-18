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
        private CommandActionType? action;
        private CommandStatus?     status;

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
            get
            {
                if (this.action.HasValue)
                {
                    return this.action.Value;
                }
                else
                {
                    return default(CommandActionType);
                }
            }
            set { this.action = value; }
        }

        [XmlIgnore]
        public bool ActionSpecified
        {
            get { return this.action.HasValue; }
            set
            {
                if (!value)
                {
                    this.action = null;
                }
            }
        }

        [XmlAttribute("Status")]
        public CommandStatus Status
        {
            get
            {
                if (this.status.HasValue)
                {
                    return this.status.Value;
                }
                else
                {
                    return default(CommandStatus);
                }
            }
            set { this.status = value; }
        }

        [XmlIgnore]
        public bool StatusSpecified
        {
            get { return this.status.HasValue; }
            set
            {
                if (!value)
                {
                    this.status = null;
                }
            }
        }

        public Command()
        {
            this.Items = new List<object>();
        }
    }
}

// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.AdHocCommands
{
    using Conversa.Net.Xmpp.Shared;
    using System.Xml.Serialization;

    /// <summary>
    /// Ad-Hoc Commands
    /// </summary>
    /// <remarks>
    /// XEP-0050: Ad-Hoc Commands
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://jabber.org/protocol/commands")]
    [XmlRootAttribute("actions", Namespace = "http://jabber.org/protocol/commands", IsNullable = false)]
    public partial class CommandActions
    {
        [XmlAttribute("type")]
        public CommandActionsType Type
        {
            get;
            set;
        }

        [XmlElementAttribute("prev", Namespace = "http://jabber.org/protocol/commands")]
        public Empty Prev
        {
            get;
            set;
        }

        [XmlIgnore]
        public bool PrevSpecified
        {
            get;
            set;
        }

        [XmlElementAttribute("next", Namespace = "http://jabber.org/protocol/commands")]
        public Empty Next
        {
            get;
            set;
        }

        [XmlIgnore]
        public bool NextSpecified
        {
            get;
            set;
        }

        [XmlElementAttribute("complete", Namespace = "http://jabber.org/protocol/commands")]
        public Empty Complete
        {
            get;
            set;
        }

        [XmlIgnore]
        public bool CompleteSpecified
        {
            get;
            set;
        }
    }
}

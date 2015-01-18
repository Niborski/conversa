// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.AdHocCommands
{
    using System.Xml.Serialization;

    /// <summary>
    /// Ad-Hoc Commands
    /// </summary>
    /// <remarks>
    /// XEP-0050: Ad-Hoc Commands
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://jabber.org/protocol/commands")]
    [XmlRootAttribute("note", Namespace = "http://jabber.org/protocol/commands", IsNullable = false)]
    public partial class CommandNote
    {
        [XmlAttribute("type")]
        public CommandNoteType Type
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

        public CommandNote()
        {
        }
    }
}

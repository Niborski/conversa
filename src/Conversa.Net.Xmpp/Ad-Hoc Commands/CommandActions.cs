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
        private Empty? prev;
        private Empty? next;
        private Empty? complete;

        [XmlAttribute("type")]
        public CommandActionsType Type
        {
            get;
            set;
        }

        [XmlElementAttribute("prev", Namespace = "http://jabber.org/protocol/commands")]
        public Empty Prev
        {
            get
            {
                if (this.prev.HasValue)
                {
                    return this.prev.Value;
                }
                else
                {
                    return default(Empty);
                }
            }
            set { this.prev = value; }
        }

        [XmlIgnore]
        public bool PrevSpecified
        {
            get { return this.prev.HasValue; }
            set
            {
                if (!value)
                {
                    this.prev = null;
                }
            }
        }

        [XmlElementAttribute("next", Namespace = "http://jabber.org/protocol/commands")]
        public Empty Next
        {
            get
            {
                if (this.next.HasValue)
                {
                    return this.next.Value;
                }
                else
                {
                    return default(Empty);
                }
            }
            set { this.next = value; }
        }

        [XmlIgnore]
        public bool NextSpecified
        {
            get { return this.next.HasValue; }
            set
            {
                if (!value)
                {
                    this.next = null;
                }
            }
        }

        [XmlElementAttribute("complete", Namespace = "http://jabber.org/protocol/commands")]
        public Empty Complete
        {
            get
            {
                if (this.complete.HasValue)
                {
                    return this.complete.Value;
                }
                else
                {
                    return default(Empty);
                }
            }
            set { this.complete = value; }
        }

        [XmlIgnore]
        public bool CompleteSpecified
        {
            get { return this.complete.HasValue; }
            set
            {
                if (!value)
                {
                    this.complete = null;
                }
            }
        }
    }
}

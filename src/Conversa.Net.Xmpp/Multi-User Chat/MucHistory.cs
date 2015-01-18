// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.MultiUserChat
{
    using Conversa.Net.Xmpp.Shared;
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// Multi-User Chat
    /// </summary>
    /// <remarks>
    /// XEP-0045: Multi-User Chat
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://jabber.org/protocol/muc")]
    [XmlRootAttribute("history", Namespace = "http://jabber.org/protocol/muc", IsNullable = false)]
    public partial class MucHistory
    {
        [XmlAttribute("maxchars")]
        public int MaxChars
        {
            get;
            set;
        }

        [XmlIgnore]
        public bool MaxCharsSpecified
        {
            get;
            set;
        }

        [XmlAttribute("maxstanzas")]
        public int MaxStanzas
        {
            get;
            set;
        }

        [XmlIgnore]
        public bool MaxStanzasSpecified
        {
            get;
            set;
        }

        [XmlAttribute("seconds")]
        public int Seconds
        {
            get;
            set;
        }

        [XmlIgnore]
        public bool SecondsSpecified
        {
            get;
            set;
        }

        [XmlAttribute("since")]
        public DateTime since
        {
            get;
            set;
        }

        [XmlIgnore]
        public bool SinceSpecified
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

        public MucHistory()
        {
        }
    }
}

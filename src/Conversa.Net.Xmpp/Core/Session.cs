// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Core
{
    using Conversa.Net.Xmpp.Shared;
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// IM Session Establishment
    /// </summary>
    /// <remarks>
    /// RFC 6121: Instant Messaging and Presence
    /// </remarks>
    [XmlTypeAttribute(Namespace = "urn:ietf:params:xml:ns:xmpp-session")]
    [XmlRootAttribute("session", Namespace = "urn:ietf:params:xml:ns:xmpp-session")]
    public partial class Session
    {
        /// <remarks/>
        [XmlElementAttribute("optional", typeof(Empty))]
        [XmlElementAttribute("required", typeof(Empty))]
        [XmlChoiceIdentifierAttribute("SessionChoiceType")]
        public Empty Item
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlIgnoreAttribute]
        public SessionChoiceType SessionChoiceType
        {
            get;
            set;
        }

        public Session()
        {
        }
    }
}

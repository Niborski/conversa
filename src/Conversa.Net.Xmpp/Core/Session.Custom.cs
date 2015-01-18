// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Core
{
    using System.Xml.Serialization;

    /// <summary>
    /// IM Session Establishment
    /// </summary>
    /// <remarks>
    /// RFC 6121: Instant Messaging and Presence
    /// </remarks>
    public partial class Session
    {
        /// <remarks/>
        [XmlIgnoreAttribute]
        public bool IsOptional
        {
            get { return this.SessionChoiceType == SessionChoiceType.Optional; }
        }

        /// <remarks/>
        [XmlIgnoreAttribute]
        public bool IsRequired
        {
            get { return this.SessionChoiceType == SessionChoiceType.Required; }
        }
    }
}

// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using System.Xml.Serialization;

namespace Conversa.Net.Xmpp.Core
{
    /// <summary>
    /// Message Stanza
    /// </summary>
    /// <remarks>
    /// RFC 6120: XMPP Core
    /// </remarks>
    public partial class Message
        : IStanza
    {
        /// <summary>
        /// Gets a value indicating wheter the current instance is a error message
        /// </summary>
        [XmlIgnoreAttribute]
        public bool IsError
        {
            get { return this.Type == MessageType.Error; }
        }

        /// <summary>
        /// Gets a value indicating wheter the current instance is a chat message
        /// </summary>
        [XmlIgnoreAttribute]
        public bool IsChat
        {
            get { return this.Type == MessageType.Chat; }
        }

        /// <summary>
        /// Gets a value indicating wheter the current instance is a group chat message
        /// </summary>
        [XmlIgnoreAttribute]
        public bool IsGroupChat
        {
            get { return this.Type == MessageType.GroupChat; }
        }

        /// <summary>
        /// Gets a value indicating wheter the current instance is a headline messsage
        /// </summary>
        [XmlIgnoreAttribute]
        public bool IsHeadline
        {
            get { return this.Type == MessageType.Headline; }
        }

        /// <summary>
        /// Gets a value indicating wheter the current instance is a normal messsage
        /// </summary>
        [XmlIgnoreAttribute]
        public bool IsNormal
        {
            get { return this.Type == MessageType.Normal; }
        }
    }
}

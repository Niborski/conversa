// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using System.Xml.Serialization;

namespace Conversa.Net.Xmpp.Core
{
    /// <summary>
    /// Presence Stanza
    /// </summary>
    /// <remarks>
    /// RFC 6121:  Instant Messaging and Presence
    /// </remarks>
    public partial class Presence
        : IStanza
    {
        /// <summary>
        /// Gets a value indicating wheter the current instance is a presence error.
        /// </summary>
        [XmlIgnoreAttribute]
        public bool IsError
        {
            get { return this.TypeSpecified && this.Type == PresenceType.Error; }
        }

        /// <summary>
        /// Gets a value indicating wheter the current instance is a presence probe.
        /// </summary>
        [XmlIgnoreAttribute]
        public bool IsProbe
        {
            get { return this.TypeSpecified && this.Type == PresenceType.Probe; }
        }

        /// <summary>
        /// Gets a value indicating wheter the current instance is a subscribe presence.
        /// </summary>
        [XmlIgnoreAttribute]
        public bool IsSubscribe
        {
            get { return this.TypeSpecified && this.Type == PresenceType.Subscribed; }
        }

        /// <summary>
        /// Gets a value indicating wheter the current instance is a subscribed presence.
        /// </summary>
        [XmlIgnoreAttribute]
        public bool IsSubscribed
        {
            get { return this.TypeSpecified && this.Type == PresenceType.Subscribed; }
        }

        /// <summary>
        /// Gets a value indicating wheter the current instance is a unavailable presence.
        /// </summary>
        [XmlIgnoreAttribute]
        public bool IsUnavailable
        {
            get { return this.TypeSpecified && this.Type == PresenceType.Unavailable; }
        }

        /// <summary>
        /// Gets a value indicating wheter the current instance is a unsubscribe presence.
        /// </summary>
        [XmlIgnoreAttribute]
        public bool IsUnsubscribe
        {
            get { return this.TypeSpecified && this.Type == PresenceType.Unsubscribe; }
        }

        /// <summary>
        /// Gets a value indicating wheter the current instance is a unsubscribed presence.
        /// </summary>
        [XmlIgnoreAttribute]
        public bool IsUnsubscribed
        {
            get { return this.TypeSpecified && this.Type == PresenceType.Unsubscribed; }
        }

        /// <summary>
        /// Configures the presence status as unavailable/offline
        /// </summary>
        /// <returns></returns>
        public Presence AsUnavailable()
        {
            this.Type          = PresenceType.Unavailable;
            this.TypeSpecified = true;
            this.Show          = ShowType.Offline;
            this.ShowSpecified = true;

            return this;
        }
    }
}

// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using System;

namespace Conversa.Net.Xmpp.Capabilities
{
    /// <summary>
    /// Entity capabilities info
    /// </summary>
    public interface IEntityCapabilitiesInfo
    {
        /// <summary>
        /// Gets the caps changed stream.
        /// </summary>
        /// <value>
        /// The caps changed stream.
        /// </value>
        IObservable<EntityCapabilities> CapsChangedStream
        {
            get;
        }

        /// <summary>
        /// Gets a value that indicates whether user tunes are supported
        /// </summary>
        bool SupportsUserTune
        {
            get;
        }

        /// <summary>
        /// Gets a value that indicates whether user moods are supported
        /// </summary>
        bool SupportsUserMood
        {
            get;
        }

        /// <summary>
        /// Gets a value that indicates whether simple communications blocking is supported
        /// </summary>
        bool SupportsBlocking
        {
            get;
        }

        /// <summary>
        /// Gets a value that indicates if the contact supports MUC
        /// </summary>
        bool SupportsConference
        {
            get;
        }

        /// <summary>
        /// Gets a value that indicates if the contact supports chat state notifications
        /// </summary>
        bool SupportsChatStateNotifications
        {
            get;
        }
    }
}

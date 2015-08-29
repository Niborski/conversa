// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conversa.Net.Xmpp.InstantMessaging
{
    /// <summary>
    /// Represents the Rich Communication Services (RCS) chat capabilities of a phone number.
    /// </summary>
    public sealed class ChatCapabilities
    {
        /// <summary>
        /// Gets a Boolean value indicating if a phone number supports Rich Communication Services (RCS) chat.
        /// </summary>
        public bool IsChatCapable
        {
            get;
        }
        
        /// <summary>
        /// Gets a Boolean value indicating if a phone number supports Rich Communication Services (RCS) file transfer.
        /// </summary>
        public bool IsFileTransferCapable
        {
            get;
        }
        
        /// <summary>
        /// Gets a Boolean value indicating if a phone number is capable of pushing Rich Communication Services (RCS) geolocation.
        /// </summary>
        public bool IsGeoLocationPushCapable
        {
            get;
        }
        
        /// <summary>
        /// Gets a Boolean value indicating if a phone number supports Rich Communication Services (RCS) integrated messaging.
        /// </summary>
        public bool IsIntegratedMessagingCapable
        {
            get;
        }

        /// <summary>
        /// Gets a Boolean value indicating if an Rich Communication Services (RCS) capable phone number is online.
        /// </summary>
        public bool IsOnline
        {
            get;
        }

        public ChatCapabilities()
        {
        }
    }
}

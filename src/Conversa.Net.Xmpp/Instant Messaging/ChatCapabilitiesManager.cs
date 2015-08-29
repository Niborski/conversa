// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.Core;
using System;
using Windows.Foundation;

namespace Conversa.Net.Xmpp.InstantMessaging
{
    /// <summary>
    /// Provides functionality for getting chat capabilities.
    /// </summary>
    public static class ChatCapabilitiesManager
    {
        /// <summary>
        /// Asynchronously gets the locally cached Rich Communication Services (RCS) chat capabilities for the specified XMPP address.
        /// </summary>
        /// <param name="address">The XMPP address for which to get the RCS chat capabilities.</param>
        /// <returns>The locally cached RCS chat capabilities.</returns>
        public static IAsyncOperation<ChatCapabilities> GetCachedCapabilitiesAsync(string address)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchronously gets the Rich Communication Services (RCS) chat capabilities for the specified XMPP address.
        /// </summary>
        /// <param name="address">The XMPP address for which to get the RCS chat capabilities.</param>
        /// <returns>The RCS chat capabilities from the service provider.</returns>
        public static IAsyncOperation<ChatCapabilities> GetCapabilitiesFromNetworkAsync(XmppAddress address)
        {
            throw new NotImplementedException();
        }
    }
}

// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using System;
using Windows.Foundation;

namespace Conversa.Net.Xmpp.InstantMessaging
{
    /// <summary>
    /// Provides functionality for blocking messages.
    /// </summary>
    public static class ChatMessageBlocking
    {
        /// <summary>
        /// Asynchronously marks a message as blocked or unblocked.
        /// </summary>
        /// <param name="localChatMessageId">The ID of the message to block.</param>
        /// <param name="blocked">True if the message should be blocked, false if it should be unblocked.</param>
        /// <returns>An async action indicating that the operation has completed.</returns>
        public static IAsyncAction MarkMessageAsBlockedAsync(string localChatMessageId,  bool blocked)
        {
            throw new NotImplementedException();
        }
    }
}

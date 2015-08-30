// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using System;
using System.Linq.Expressions;

namespace Conversa.Net.Xmpp.InstantMessaging
{
    /// <summary>
    /// Represents the criteria for finding chat messages.
    /// </summary>
    public sealed class ChatQueryOptions
    {
        /// <summary>
        /// Gets or sets the expression to search for the in ChatMessageStore.
        /// </summary>
        public Expression<Func<ChatMessage, bool>> SearchExpression
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatQueryOptions"/> class.
        /// </summary>
        public ChatQueryOptions()
        {
        }
    }
}

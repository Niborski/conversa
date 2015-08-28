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
        public Expression<Func<ChatMessage, bool>> Predicate
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of the ChatQueryOptions class.
        /// </summary>
        public ChatQueryOptions()
        {
        }
    }
}

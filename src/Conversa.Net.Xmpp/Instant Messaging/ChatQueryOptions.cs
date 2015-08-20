namespace Conversa.Net.Xmpp.InstantMessaging
{
    /// <summary>
    /// Represents the criteria for finding chat messages.
    /// </summary>
    public sealed class ChatQueryOptions
    {
        /// <summary>
        /// Gets or sets the string to search for the in ChatMessageStore.
        /// </summary>
        public string SearchString
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

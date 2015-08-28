using Conversa.Net.Xmpp.Core;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace Conversa.Net.Xmpp.InstantMessaging
{
    /// <summary>
    /// Provides threading info for a ChatConversation.
    /// </summary>
    public sealed class ChatConversationThreadingInfo
    {
        [PrimaryKey]
        public string Id
        {
            get;
            set;
        }

        [ForeignKey(typeof(ChatMessage))]
        public string ChatMessageId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Contact.Id for the remote participant.
        /// </summary>
        public string ContactId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or puts the ID of the ChatConversation.
        /// </summary>
        public string ConversationId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or puts a string where you can store your own custom threading info.
        /// </summary>
        public string Custom
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or puts a value that indicates the type of threading info, such as participant, contact ID, conversation ID, and so on.
        /// </summary>
        public ChatConversationThreadingKind Kind
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the list of participants in the ChatConversation.
        /// </summary>
        public IList<string> Participants
        {
            get;
        }

        /// <summary>
        /// Initializes a new instance of the ChatConversationThreadingInfo class.
        /// </summary>
        public ChatConversationThreadingInfo()
        {
            this.Participants = new List<string>();
        }
    }
}

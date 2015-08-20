using Conversa.Net.Xmpp.Core;
using System.Collections.Generic;

namespace Conversa.Net.Xmpp.InstantMessaging
{
    /// <summary>
    /// Provides threading info for a ChatConversation.
    /// </summary>
    public sealed class ChatConversationThreadingInfo
    {
        /// <summary>
        /// Gets or sets the Contact.Id for the remote participant.
        /// </summary>
        public XmppAddress ContactId
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
        public IList<XmppAddress> Participants
        {
            get;
        }

        /// <summary>
        /// Initializes a new instance of the ChatConversationThreadingInfo class.
        /// </summary>
        public ChatConversationThreadingInfo()
        {
        }

        /// <summary>
        /// Converts the current conversation threading info to XMPP format.
        /// </summary>
        /// <returns>The current conversation threading info as an XMPP message thread.</returns>
        public MessageThread ToXmpp()
        {
            return null;
        }
    }
}

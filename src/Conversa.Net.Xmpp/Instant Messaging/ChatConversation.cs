using Conversa.Net.Xmpp.Client;
using Conversa.Net.Xmpp.Core;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;

namespace Conversa.Net.Xmpp.InstantMessaging
{
    /// <summary>
    /// Represents a conversation in a chat client.
    /// </summary>
    public sealed class ChatConversation
    {
        public event TypedEventHandler<ChatConversation, RemoteParticipantComposingChangedEventArgs> RemoteParticipantComposingChanged;

        private Contact          owner;
        private ChatMessageStore store;
        
        /// <summary>
        /// Gets a Boolean value indicating if there are unread messages in the ChatConversation.
        /// </summary>
        public bool HasUnreadMessages
        {
            get;
        } = false;

        /// <summary>
        /// Gets the unique identifier for the ChatConversation.
        /// </summary>
        public string Id
        {
            get { return this.owner.Address; }
        }
        
        /// <summary>
        /// Gets or puts a Boolean value indicating if the ChatConversation is muted.
        /// </summary>
        public bool IsConversationMuted
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the ID of the most recent message in the conversation.
        /// </summary>
        public string MostRecentMessageId
        {
            get;
        }

        /// <summary>
        /// Gets a list of all the participants in the conversation.
        /// </summary>
        public IList<XmppAddress> Participants
        {
            get;
        }

        /// <summary>
        /// Read/write	Gets or puts the subject of a group conversation.
        /// </summary>
        public string Subject
        {
            get;
            set;
        }
         
        /// <summary>
        /// Gets the threading info for the ChatConversation.
        /// </summary>
        public ChatConversationThreadingInfo ThreadingInfo
        {
            get;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatConversation"/> class.
        /// </summary>
        /// <param name="owner">The conversation owner.</param>
        internal ChatConversation(Contact owner)
        {
            this.owner = owner;
            this.store = XmppTransportManager.RequestStore();;
        }      

        /// <summary>
        /// 
        /// </summary>
        /// <returns>An asynchronous operation that returns a ChatMessageStore on successful completion.</returns>
        public IAsyncOperation<ChatMessageStore> RequestStoreAsync()
        {
            return AsyncInfo.Run(_ => Task.Run<ChatMessageStore>(() => { return store; }));
        }

        /// <summary>
        /// Asynchronously deletes all of the messages in the ChatConversation and the conversation itself.
        /// </summary>
        /// <returns>An async action indicating that the operation has completed.</returns>
        public IAsyncAction DeleteAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the ChatMessageReader for this ChatConversation.
        /// </summary>
        /// <returns>The ChatMessageReader for this ChatConversation.</returns>
        public ChatMessageReader GetMessageReader()
        {
            return this.store.GetMessageReader();
        }
        
        /// <summary>
        /// Asynchronously marks all the messages in the conversation as read.
        /// </summary>
        /// <returns>An async action indicating that the operation has completed.</returns>
        public IAsyncAction MarkMessagesAsReadAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchronously marks all the messages in the conversation before the specified DateTime as read.
        /// </summary>
        /// <param name="value">Mark all messages before this DateTime as read.</param>
        /// <returns>An async action indicating that the operation has completed.</returns>
        public IAsyncAction MarkMessagesAsReadAsync(DateTimeOffset value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Call this to indicate that the local participant has started or has completed typing.
        /// </summary>
        /// <param name="transportId">Specifies the ChatMessageTransport to use.</param>
        /// <param name="participantAddress">The address of the remote participant.</param>
        /// <param name="isComposing">True if the local participant is typing, otherwise false.</param>
        public void NotifyLocalParticipantComposing(string transportId, XmppAddress participantAddress, bool isComposing)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Locally triggers the event that indicates that a remote participant is typing.
        /// </summary>
        /// <param name="transportId">Specifies the ChatMessageTransport to use.</param>
        /// <param name="participantAddress">The address of the remote participant.</param>
        /// <param name="isComposing">True if the remote participant is typing, otherwise false.</param>
        public void NotifyRemoteParticipantComposing(string transportId, XmppAddress participantAddress, bool isComposing)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchronously saves the ChatConversation.
        /// </summary>
        /// <returns>An async action indicating that the operation has completed.</returns>
        public IAsyncAction SaveAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The chat message to be sent.
        /// </summary>
        /// <param name="chatMessage"></param>
        /// <returns></returns>
        public IAsyncAction SendMessageAsync(ChatMessage chatMessage)
        {
            return AsyncInfo.Run(_ => Task.Run(async () => {  await this.store.SendMessageAsync(chatMessage); }));
        }
    }
}

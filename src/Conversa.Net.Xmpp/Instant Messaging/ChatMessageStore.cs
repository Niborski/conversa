using Conversa.Net.Xmpp.Client;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;

namespace Conversa.Net.Xmpp.InstantMessaging
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ChatMessageStore
    {
        // public event TypedEventHandler<ChatMessageStore, ChatMessageChangedEventArgs> MessageChanged;

        /// <summary>
        /// Gets the chat message change tracker for the store.
        /// </summary>
        public ChatMessageChangeTracker ChangeTracker
        {
            get;
        }

        /// <summary>
        /// Initializes a new instance with the given <see cref="Contact">owner</see>.
        /// </summary>
        /// <param name="owner">The chat message store owner.</param>
        internal ChatMessageStore()
        {
        }

        /// <summary>
        /// The local ID of the message to be deleted.
        /// </summary>
        /// <param name="localMessageId"></param>
        /// <returns>An asynchronous action.</returns>
        public IAsyncAction DeleteMessageAsync(string localMessageId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="localChatMessageId">The local ID of the message to be downloaded.</param>
        /// <returns></returns>
        public IAsyncAction DownloadMessageAsync(string localChatMessageId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="localChatMessageId"></param>
        /// <returns></returns>
        public IAsyncOperation<ChatMessage> GetMessageAsync(string localChatMessageId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>The chat message reader.</returns>
        public ChatMessageReader GetMessageReader()
        {
            throw new NotImplementedException();
        }

        public ChatMessageReader GetMessageReader(TimeSpan recentTimeLimit)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="localChatMessageId"></param>
        /// <returns></returns>
        public IAsyncAction MarkMessageReadAsync(string localChatMessageId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="localChatMessageId">The local ID of the message to be retried.</param>
        /// <returns></returns>
        public IAsyncAction RetrySendMessageAsync(string localChatMessageId)
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
            return AsyncInfo.Run(_ => Task.Run(async () => {
                var status    = this.ValidateMessage(chatMessage);

                if (status.Status == ChatMessageValidationStatus.Valid
                 || status.Status == ChatMessageValidationStatus.ValidWithLargeMessage)
                {
                    var xmppMessage = chatMessage.ToXmpp();
                    var transport   = XmppTransportManager.GetTransport();

                    chatMessage.RemoteId = xmppMessage.Id;
                    chatMessage.Status   = ChatMessageStatus.Sending;

                    await transport.SendAsync(xmppMessage).ConfigureAwait(false);
                }
            }));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chatMessage">The chat message to validate.</param>
        /// <returns>The validation result.</returns>
        public ChatMessageValidationResult ValidateMessage(ChatMessage chatMessage)
        {
            var transport = XmppTransportManager.GetTransport();
            var status    = ChatMessageValidationStatus.Valid;

            if (transport == null)
            {
                status = ChatMessageValidationStatus.TransportNotFound;
            }
            else if (transport.State != XmppTransportState.Open)
            {
                status = ChatMessageValidationStatus.TransportInactive;
            }
            else if (chatMessage == null || chatMessage.ThreadingInfo == null)
            {
                status = ChatMessageValidationStatus.InvalidData;
            }
            else if (String.IsNullOrEmpty(chatMessage.Body))
            {
                status = ChatMessageValidationStatus.InvalidBody;
            }
            else if (String.IsNullOrEmpty(chatMessage.ThreadingInfo.ContactId))
            {
                status = ChatMessageValidationStatus.NoRecipients;
            }
            else if (transport.Contacts[chatMessage.ThreadingInfo.ContactId] == null)
            {
                status = ChatMessageValidationStatus.InvalidRecipients;
            }

            return new ChatMessageValidationResult(status);
        }
    }
}
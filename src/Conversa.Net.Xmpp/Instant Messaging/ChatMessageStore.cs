using Conversa.Net.Xmpp.Client;
using Conversa.Net.Xmpp.Core;
using Conversa.Net.Xmpp.DataStore;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Windows.Foundation;

namespace Conversa.Net.Xmpp.InstantMessaging
{
    /// <summary>
    /// Provides the methods and properties to read, manage and send messages.
    /// </summary>
    public sealed class ChatMessageStore
    {
        private Subject<ChatMessage> messageChangedStream;

        static ChatMessageStore()
        {   
            DataSource<ChatMessageAttachment>.CreateTable();
            DataSource<ChatConversationThreadingInfo>.CreateTable();         
            DataSource<ChatRecipientDeliveryInfo>.CreateTable();
            DataSource<ChatMessage>.CreateTable();
        }

        public IObservable<ChatMessage> MessageChangedStream
        {
            get { return this.messageChangedStream.AsObservable(); }
        }

        /// <summary>
        /// Gets the chat message change tracker for the store.
        /// </summary>
        public ChatMessageChangeTracker ChangeTracker
        {
            get;
            private set;
        }

        internal ChatMessageStore()
        {
            var transport = XmppTransportManager.GetTransport();

            this.messageChangedStream = new Subject<ChatMessage>();
            this.ChangeTracker        = new ChatMessageChangeTracker();
        }

        /// <summary>
        /// Deletes a message from the chat message store.
        /// </summary>
        /// <param name="localMessageId">The local ID of the message to be deleted.</param>
        public async Task DeleteMessageAsync(string localMessageId)
        {
            ChatMessage chatMessage = await this.GetMessageAsync(localMessageId).ConfigureAwait(false);

            if (chatMessage != null)
            {
                chatMessage.Status = ChatMessageStatus.Deleted;

                await this.SaveMessageAsync(chatMessage).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Downloads a message specified by the identifier to the message store.
        /// </summary>
        /// <param name="localChatMessageId">The local ID of the message to be downloaded.</param>
        /// <returns></returns>
        public async Task<ChatMessage> DownloadMessageAsync(string localChatMessageId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieves a message specified by an identifier from the message store.
        /// </summary>
        /// <param name="localChatMessageId"></param>
        /// <returns></returns>
        public async Task<ChatMessage> GetMessageAsync(string localChatMessageId)
        {
            return await GetMessageReader().GetMessageAsync(localChatMessageId).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a ChatMessageReader class object which provides a message collection from the message store.
        /// </summary>
        /// <returns>The chat message reader.</returns>
        public ChatMessageReader GetMessageReader()
        {
            return new ChatMessageReader();
        }

        /// <summary>
        /// Marks a specified message in the store as already read.
        /// </summary>
        /// <param name="localChatMessageId"></param>
        /// <returns></returns>
        public async Task MarkMessageReadAsync(string localChatMessageId)
        {
            ChatMessage chatMessage = await this.GetMessageAsync(localChatMessageId).ConfigureAwait(false);

            chatMessage.IsRead = true;

            await this.SaveMessageAsync(chatMessage).ConfigureAwait(false);
        }

        /// <summary>
        /// Attempts a retry of sending a specified message from the message store.
        /// </summary>
        /// <param name="localChatMessageId">The local ID of the message to be retried.</param>
        /// <returns></returns>
        public IAsyncAction RetrySendMessageAsync(string localChatMessageId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Attempts to send a chat message. The message is saved to the message store as part of the send operation.
        /// </summary>
        /// <param name="chatMessage">The chat message to be sent.</param>
        public async Task SendMessageAsync(ChatMessage chatMessage)
        {
            var status = this.ValidateMessage(chatMessage);

            if (!chatMessage.IsIncoming)
            {
                if (status.Status == ChatMessageValidationStatus.Valid
                 || status.Status == ChatMessageValidationStatus.ValidWithLargeMessage)
                {
                    var xmppMessage = chatMessage.ToXmpp();
                    var transport   = XmppTransportManager.GetTransport();

                    chatMessage.RemoteId                = xmppMessage.Id;
                    chatMessage.RecipientsDeliveryInfos = new List<ChatRecipientDeliveryInfo>();

                    if (chatMessage.Status == ChatMessageStatus.Draft)
                    {
                        chatMessage.Status = ChatMessageStatus.Sending;
                    }
                    else
                    {
                        chatMessage.Status = ChatMessageStatus.SendRetryNeeded;
                    }

                    foreach (var recipient in chatMessage.Recipients)
                    {
                        var deliveryInfo = new ChatRecipientDeliveryInfo
                        {
                             Id                            = IdentifierGenerator.Generate()
                           , DeliveryTime                  = DateTimeOffset.UtcNow
                           , IsErrorPermanent              = false
                           , Status                        = chatMessage.Status
                           , TransportAddress              = recipient
                           , TransportErrorCode            = 0
                           , TransportErrorCodeCategory    = XmppTransportErrorCodeCategory.None
                           , TransportInterpretedErrorCode = XmppTransportInterpretedErrorCode.None
                        };

                        chatMessage.RecipientsDeliveryInfos.Add(deliveryInfo);
                    }
                   
                    await this.SaveMessageAsync(chatMessage).ConfigureAwait(false);

                    await transport.SendAsync(xmppMessage
                                            , async message => await OnMessageSent(message).ConfigureAwait(false)
                                            , async message => await OnMessageError(message).ConfigureAwait(false))
                                   .ConfigureAwait(false);
                }
                else
                {
                    chatMessage.Status = ChatMessageStatus.SendRetryNeeded;
                }
            }

            await this.SaveMessageAsync(chatMessage).ConfigureAwait(false);
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

        private async Task SaveMessageAsync(ChatMessage chatMessage)
        {
            await DataSource<ChatMessage>.AddOrUpdateAsync(chatMessage).ConfigureAwait(false);
        }

        private async Task OnMessageSent(Message message)
        {
            ChatMessage chatMessage = await DataSource<ChatMessage>
                .Query()
                .Where(m => m.RemoteId == message.Id)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            if (chatMessage != null)
            {
                chatMessage.Status = ChatMessageStatus.Sent;

                foreach (var deliveryInfo in chatMessage.RecipientsDeliveryInfos)
                {
                    deliveryInfo.Status = chatMessage.Status;
                }

                await this.SaveMessageAsync(chatMessage).ConfigureAwait(false);
            }
        }

        private async Task OnMessageError(Message message)
        {
            ChatMessage chatMessage = await DataSource<ChatMessage>
                .Query()
                .Where(m => m.RemoteId == message.Id)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            if (chatMessage != null)
            {
                chatMessage.Status = ChatMessageStatus.SendFailed;

                foreach (var deliveryInfo in chatMessage.RecipientsDeliveryInfos)
                {
                    deliveryInfo.ReadTime                      = DateTimeOffset.UtcNow;
                    deliveryInfo.Status                        = chatMessage.Status;
                    deliveryInfo.IsErrorPermanent              = false;
                    deliveryInfo.TransportErrorCode            = message.Error.Code;

                    // TODO: Test code needs to be changed
                    deliveryInfo.IsErrorPermanent              = true;
                    deliveryInfo.TransportErrorCodeCategory    = XmppTransportErrorCodeCategory.None;
                    deliveryInfo.TransportInterpretedErrorCode = XmppTransportInterpretedErrorCode.InvalidRecipientAddress;
                }

                await this.SaveMessageAsync(chatMessage).ConfigureAwait(false);
            }
        }
    }
}
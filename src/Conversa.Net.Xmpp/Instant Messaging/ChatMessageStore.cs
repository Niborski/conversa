// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.Client;
using Conversa.Net.Xmpp.Core;
using Conversa.Net.Xmpp.DataStore;
using System;
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
        private Subject<ChatMessage>                      messageChangedStream;
        private Subject<ChatMessageStoreChangedEventData> storeChangedStream;
        private ChatMessageReader                         messageReader;

        /// <summary>
        /// An event that occurs when a message in the message store is changed.
        /// </summary>
        public IObservable<ChatMessage> MessageChangedStream
        {
            get { return this.messageChangedStream.AsObservable(); }
        }

        /// <summary>
        /// Occurs when something in the <see cref="ChatMessageStore"/> has changed.
        /// </summary>
        public IObservable<ChatMessageStoreChangedEventData> StoreChangedStream
        {
            get  { return this.storeChangedStream.AsObservable(); }
        }

        /// <summary>
        /// Gets the chat message change tracker for the store.
        /// </summary>
        public ChatMessageChangeTracker ChangeTracker
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatMessageStore"/> class.
        /// </summary>
        internal ChatMessageStore()
        {
            var transport = XmppTransportManager.GetTransport();

            this.messageChangedStream = new Subject<ChatMessage>();
            this.ChangeTracker        = new ChatMessageChangeTracker();
            this.messageReader        = new ChatMessageReader();
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
        /// Asynchronously gets a ChatConversation by ID.
        /// </summary>
        /// <param name="conversationId">The ID of the conversation to retrieve.</param>
        /// <returns>The ChatConversation specified by the conversationId parameter.</returns>
        public async Task<ChatConversation> GetConversationAsync(string conversationId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchronously gets a conversation based on a threading info object.
        /// </summary>
        /// <param name="threadingInfo">The threading info that identifies the conversation.</param>
        /// <returns>The conversation identified by the threadingInfo parameter.</returns>
        public async Task<ChatConversation> GetConversationFromThreadingInfoAsync(ChatConversationThreadingInfo threadingInfo)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ChatConversationReader GetConversationReader()
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
            return await DataSource<ChatMessage>.FirstOrDefaultAsync(m => m.Id == localChatMessageId).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Retrieves a message specified by its remote ID from the message store.
        /// </summary>
        /// <param name="remoteId">The <see cref="T:ChatMessage.RemoteId"/> of the <see cref="ChatMessage"/> to retrieve.</param>
        /// <returns>The message.</returns>
        public async Task<ChatMessage> GetMessageByRemoteIdAsync(string remoteId)
        {
            return await DataSource<ChatMessage>.FirstOrDefaultAsync(m => m.RemoteId == remoteId).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a ChatMessageReader class object which provides a message collection from the message store.
        /// </summary>
        /// <returns>The chat message reader.</returns>
        public ChatMessageReader GetMessageReader()
        {
            return this.messageReader;
        }

        /// <summary>
        /// Gets a new or existing ChatSearchReader to be used to search for messages.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ChatSearchReader GetSearchReader(ChatQueryOptions value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchronously gets the number of unread chat messages.
        /// </summary>
        /// <returns>The number of unread chat messages.</returns>
        public async Task<Int32> GetUnseenCountAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchronously marks all transport messages as seen.
        /// </summary>
        /// <returns>An async action indicating that the operation has finished.</returns>
        public async Task MarkAsSeenAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Marks a specified message in the store as already read.
        /// </summary>
        /// <param name="localChatMessageId"></param>
        /// <returns></returns>
        public async Task MarkMessageReadAsync(string localChatMessageId)
        {
            ChatMessage chatMessage = await this.GetMessageAsync(localChatMessageId).ConfigureAwait(false);

            if (chatMessage != null)
            {
                chatMessage.IsRead = true;

                await this.SaveMessageAsync(chatMessage).ConfigureAwait(false);
            }
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
        /// Asynchronously saves a message to the <see cref="ChatMessageStore"/>.
        /// </summary>
        /// <param name="chatMessage">The message to save.</param>
        /// <returns>An async action indicating that the operation has finished.</returns>
        public async Task SaveMessageAsync(ChatMessage chatMessage)
        {
            await DataSource<ChatMessage>.AddOrUpdateAsync(chatMessage).ConfigureAwait(false);
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

                    chatMessage.RemoteId = xmppMessage.Id;                    

                    if (chatMessage.Status == ChatMessageStatus.Draft)
                    {
                        chatMessage.Status = ChatMessageStatus.Sending;
                    }
                    else
                    {
                        chatMessage.Status = ChatMessageStatus.SendRetryNeeded;
                    }

                    foreach (var deliveryInfo in chatMessage.RecipientsDeliveryInfos)
                    {
                        deliveryInfo.Status = chatMessage.Status;
                    }

                    await this.SaveMessageAsync(chatMessage).ConfigureAwait(false);

                    await transport.SendAsync(xmppMessage
                                            , null
                                            , async message => await OnMessageError(message).ConfigureAwait(false))
                                   .ConfigureAwait(false);

                    chatMessage.Status = ChatMessageStatus.Sent;

                    foreach (var deliveryInfo in chatMessage.RecipientsDeliveryInfos)
                    {
                        deliveryInfo.Status = chatMessage.Status;
                    }

                    await this.SaveMessageAsync(chatMessage).ConfigureAwait(false);
                }
                else
                {
                    chatMessage.Status = ChatMessageStatus.SendRetryNeeded;
                }
            }
            else
            {
                await this.SaveMessageAsync(chatMessage).ConfigureAwait(false);
            }
        }        

        /// <summary>
        /// Asynchronously attempts to cancel downloading the specified message.
        /// </summary>
        /// <param name="localChatMessageId">The ID of the message to stop downloading.</param>
        /// <returns>An async action indicating that the operation has completed.</returns>
        public async Task<bool> TryCancelDownloadMessageAsync(string localChatMessageId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchronously attempts to cancel sending the specified message.
        /// </summary>
        /// <param name="localChatMessageId">The ID of the message to stop sending.</param>
        /// <returns>An async action indicating that the operation has completed.</returns>
        public async Task<bool> TryCancelSendMessageAsync(string localChatMessageId)
        {
            throw new NotImplementedException();
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

        private async Task OnMessageError(Message message)
        {
            var chatMessage = await this.GetMessageByRemoteIdAsync(message.Id).ConfigureAwait(false);

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
// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.ChatStates;
using Conversa.Net.Xmpp.Client;
using Conversa.Net.Xmpp.Core;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Velox.DB;
using Windows.Foundation;

namespace Conversa.Net.Xmpp.InstantMessaging
{
    /// <summary>
    /// Represents a conversation in a chat client.
    /// </summary>
    public sealed class ChatConversation
        : BindableBase
    {
        public static ChatConversation Create(Contact contact)
        {
            return new ChatConversation(contact);
        }

        private Subject<RemoteParticipantComposingChangedEventData> remoteParticipantComposingChangedStream;
        private Subject<ChatMessage> incomingChatMessageStream;
        private ChatMessageStore     store;

        /// <summary>
        /// Occurs when the remote user has started or finished typing.
        /// </summary>
        [Column.Ignore]
        public IObservable<RemoteParticipantComposingChangedEventData> RemoteParticipantComposingChangedStream
        {
            get { return this.remoteParticipantComposingChangedStream.AsObservable(); }
        }

        /// <summary>
        /// Occurs when an incoming chat message has been received.
        /// </summary>
        [Column.Ignore]
        public IObservable<ChatMessage> IncomingChatMessageStream
        {
            get { return this.incomingChatMessageStream.AsObservable(); }
        }

        /// <summary>
        /// Gets a Boolean value indicating if there are unread messages in the ChatConversation.
        /// </summary>
        public bool HasUnreadMessages
        {
            get { return GetProperty(() => HasUnreadMessages); }
            private set { SetProperty(() => HasUnreadMessages, value); }
        }

        /// <summary>
        /// Gets the unique identifier for the ChatConversation.
        /// </summary>
        [Column.PrimaryKey, Column.Name("ConversationId")]
        public string Id
        {
            get { return GetProperty(() => Id); }
            set { SetProperty(() => Id, value); }
        }
        
        /// <summary>
        /// Gets or puts a Boolean value indicating if the ChatConversation is muted.
        /// </summary>
        public bool IsConversationMuted
        {
            get { return GetProperty(() => IsConversationMuted); }
            set { SetProperty(() => IsConversationMuted, value); }
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
        [Column.Ignore]
        public IEnumerable<string> Participants
        {
            get
            {
                foreach (string participant in this.ThreadingInfo.Participants)
                {
                    yield return participant;
                }
            }
        }

        /// <summary>
        /// Gets or puts the subject of a group conversation.
        /// </summary>
        public string Subject
        {
            get { return GetProperty(() => Subject); }
            set { SetProperty(() => Subject, value); }
        }

        /// <summary>
        /// Gets the threading info for the ChatConversation.
        /// </summary>
        [Column.Ignore]
        public ChatConversationThreadingInfo ThreadingInfo
        {
            get { return GetProperty(() => ThreadingInfo); }
            private set { SetProperty(() => ThreadingInfo, value); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatConversation"/> class.
        /// </summary>
        /// <param name="contact">The conversation contact.</param>
        internal ChatConversation(Contact contact)
        {
            var transport = XmppTransportManager.GetTransport();

            this.remoteParticipantComposingChangedStream = new Subject<RemoteParticipantComposingChangedEventData>();
            this.incomingChatMessageStream               = new Subject<ChatMessage>();

            this.ThreadingInfo = new ChatConversationThreadingInfo
            {
                Id              = IdentifierGenerator.Generate()
              , ContactId       = contact.Address
              , ConversationId  = this.Id
              , Custom          = null
              , Kind            = ChatConversationThreadingKind.ContactId
            };

            this.ThreadingInfo.Participants.Add(transport.UserAddress);
            this.ThreadingInfo.Participants.Add(contact.Address);

            this.store = XmppTransportManager.RequestStore(contact.Address.BareAddress);
            this.store.ChangeTracker.Enable();

            transport.MessageStream
                     .Where(message => message.IsChat && message.FromAddress.BareAddress == contact.Address)
                     .Subscribe(async message => await OnMessageReceived(message).ConfigureAwait(false));            
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
        /// <param name="participantAddress">The address of the remote participant.</param>
        /// <param name="isComposing">True if the local participant is typing, otherwise false.</param>
        public async Task NotifyLocalParticipantComposing(XmppAddress participantAddress, bool isComposing)
        {
            var transport = XmppTransportManager.GetTransport();
            var message   = new Message
            {
                Id   = IdentifierGenerator.Generate()
              , Type = MessageType.Chat
              , From = transport.UserAddress
              , To   = this.ThreadingInfo.ContactId
            };

            if (isComposing)
            {
                message.Items.Add(ChatStateType.Composing);
            }
            else
            {
                message.Items.Add(ChatStateType.Inactive);
            }

            await transport.SendAsync(message).ConfigureAwait(false);
        }

        /// <summary>
        /// Locally triggers the event that indicates that a remote participant is typing.
        /// </summary>
        /// <param name="participantAddress">The address of the remote participant.</param>
        /// <param name="isComposing">True if the remote participant is typing, otherwise false.</param>
        public async Task NotifyRemoteParticipantComposingAsync(XmppAddress participantAddress, bool isComposing)
        {
            await Task.Run(() => {
                var data = new RemoteParticipantComposingChangedEventData(participantAddress, isComposing);
                this.remoteParticipantComposingChangedStream.OnNext(data);
            });
        }

        /// <summary>
        /// The chat message to be sent.
        /// </summary>
        /// <param name="chatMessage"></param>
        /// <returns></returns>
        public async Task SendMessageAsync(ChatMessage chatMessage)
        {
            chatMessage.Id                      = IdentifierGenerator.Generate();
            chatMessage.From                    = XmppTransportManager.GetTransport().UserAddress;
            chatMessage.Subject                 = this.Subject;
            chatMessage.Status                  = ChatMessageStatus.Draft;
            chatMessage.RecipientsDeliveryInfos = new List<ChatRecipientDeliveryInfo>();
            chatMessage.LocalTimestamp          = DateTimeOffset.UtcNow;
            chatMessage.ThreadingInfo           = new ChatConversationThreadingInfo
            {
                Id             = chatMessage.Id
              , ContactId      = this.ThreadingInfo.ContactId
              , ConversationId = this.ThreadingInfo.ConversationId
              , Custom         = this.ThreadingInfo.Custom
              , Kind           = this.ThreadingInfo.Kind
            };
            
            foreach (var participant in this.Participants)
            {
                var deliveryInfo = new ChatRecipientDeliveryInfo
                {
                      Id                            = IdentifierGenerator.Generate()
                    , DeliveryTime                  = DateTimeOffset.UtcNow
                    , IsErrorPermanent              = false
                    , Status                        = chatMessage.Status
                    , TransportAddress              = participant
                    , TransportErrorCode            = 0
                    , TransportErrorCodeCategory    = XmppTransportErrorCodeCategory.None
                    , TransportInterpretedErrorCode = XmppTransportInterpretedErrorCode.None
                };

                chatMessage.RecipientsDeliveryInfos.Add(deliveryInfo);
            }

            await this.store.SendMessageAsync(chatMessage).ConfigureAwait(false);
        }

        private async Task OnMessageReceived(Message message)
        {
            var chatStates = message.Items.OfType<IChatState>().ToList();

            if (chatStates.Count > 0)
            {
                bool isComposing = (chatStates.OfType<ComposingChatState>().Count() > 0);
                await this.NotifyLocalParticipantComposing(message.From, isComposing).ConfigureAwait(false);
            }

            if (message.Body != null)
            {
                var chatMessage = ChatMessage.Create(message);

                this.incomingChatMessageStream.OnNext(chatMessage);
                this.incomingChatMessageStream.OnCompleted();

                await this.store.SendMessageAsync(chatMessage).ConfigureAwait(false);

                chatMessage.Status = ChatMessageStatus.Received;
            }

            if (message.From != this.ThreadingInfo.ContactId)
            {
                this.ThreadingInfo.ContactId = message.FromAddress;
            }
        }
    }
}

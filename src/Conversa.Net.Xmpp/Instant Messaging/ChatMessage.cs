// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.Client;
using Conversa.Net.Xmpp.Core;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Velox.DB;

namespace Conversa.Net.Xmpp.InstantMessaging
{
    /// <summary>
    /// Represents a chat message.
    /// </summary>
    public sealed class ChatMessage
        : BindableBase, IEquatable<ChatMessage>
    {
        internal static ChatMessage Create(Message message)
        {
            var chatMessage = new ChatMessage
            {
                  Id                         = IdentifierGenerator.Generate()
                , Body                       = message.Body?.Value
                , EstimatedDownloadSize      = 0
                , From                       = message.From
                , IsAutoReply                = false
                , IsForwardingDisabled       = false
                , IsIncoming                 = true
                , IsRead                     = false
                , IsReceivedDuringQuietHours = false
                , IsReplyDisabled            = false
                , IsSeen                     = false
                , LocalTimestamp             = DateTimeOffset.Now
                , MessageKind                = ChatMessageKind.Standard
                , NetworkTimestamp           = DateTimeOffset.Now
                , RecipientsDeliveryInfos    = new List<ChatRecipientDeliveryInfo>
                  {
                      new ChatRecipientDeliveryInfo
                      {
                           Id                            = IdentifierGenerator.Generate()
                         , DeliveryTime                  = null
                         , IsErrorPermanent              = false
                         , ReadTime                      = DateTimeOffset.UtcNow
                         , Status                        = ChatMessageStatus.Received
                         , TransportAddress              = message.To
                         , TransportErrorCode            = 0
                         , TransportErrorCodeCategory    = XmppTransportErrorCodeCategory .None
                         , TransportInterpretedErrorCode = XmppTransportInterpretedErrorCode.None
                      }
                  }
                , RemoteId                   = message.Id
                , ShouldSuppressNotification = false
                , Status                     = ChatMessageStatus.Received
                , Subject                    = message.Subject?.Value
                , ThreadingInfo              = new ChatConversationThreadingInfo
                  {
                       Id             = IdentifierGenerator.Generate()
                     , ContactId      = message.To
                     , ConversationId = message.Thread?.Value
                     , Custom         = null
                     , Kind           = ChatConversationThreadingKind.ContactId
                  }
            };

            chatMessage.ThreadingInfo.Participants.Add(message.To);
            chatMessage.ThreadingInfo.Participants.Add(message.From);

            return chatMessage;
        }

        /// <summary>
        /// Gets a list of chat message attachments.
        /// </summary>
        [Relation.OneToMany(ForeignKey = "ChatMessageId")]
        public List<ChatMessageAttachment> Attachments
        {
            get { return GetProperty(() => Attachments); }
            internal set { SetProperty(() => Attachments, value); }
        }

        [Column.Ignore]
        public bool HasAttachments
        {
            get { return (this.Attachments?.Count > 0); }
        }
        
        /// <summary>
        /// Gets or sets the body of the chat message.
        /// </summary>
        public string Body
        {
            get { return GetProperty(() => Body); }
            set { SetProperty(() => Body, value); }
        }

        /// <summary>
        /// Gets or sets the estimated size of a file to be sent or recieved.
        /// </summary>
        public long EstimatedDownloadSize
        {
            get { return GetProperty(() => EstimatedDownloadSize); }
            set { SetProperty(() => EstimatedDownloadSize, value); }
        }

        /// <summary>
        /// Gets the sender of the message.
        /// </summary>
        public string From
        {
            get { return GetProperty(() => From); }
            set {
                SetProperty(() => From, value);
            }
        }

        [Column.Ignore]
        public Contact Sender
        {
            get { return XmppTransportManager.GetTransport().Contacts[this.From]; }
        }

        /// <summary>
        /// Gets the ID of the message.
        /// </summary>
        [Column.PrimaryKey, Column.Name("ChatMessageId")]
        public string Id
        {
            get { return GetProperty(() => Id); }
            internal set { SetProperty(() => Id, value); }
        }

        /// <summary>
        /// Gets or sets a Boolean value indicating if the message is an auto-reply.
        /// </summary>
        public bool IsAutoReply
        {
            get { return GetProperty(() => IsAutoReply); }
            set { SetProperty(() => IsAutoReply, value); }
        }
                
        /// <summary>
        /// Gets a value indicating if forwarding is disabled.
        /// </summary>
        public bool IsForwardingDisabled
        {
            get { return GetProperty(() => IsForwardingDisabled); }
            private set { SetProperty(() => IsForwardingDisabled, value); }
        }
        
        /// <summary>
        /// Gets a value indicating if the message is incoming.
        /// </summary>
        public bool IsIncoming
        {
            get { return GetProperty(() => IsIncoming); }
            private set { SetProperty(() => IsIncoming, value); }
        }

        /// <summary>
        /// Gets a value indicating if the message has been read.
        /// </summary>
        public bool IsRead
        {
            get { return GetProperty(() => IsRead); }
            internal set { SetProperty(() => IsRead, value); }
        }

        /// <summary>
        /// Gets or sets a Boolean value indicating if the message was received during user specified quiet hours.
        /// </summary>
        public bool IsReceivedDuringQuietHours
        {
            get { return GetProperty(() => IsReceivedDuringQuietHours); }
            set { SetProperty(() => IsReceivedDuringQuietHours, value); }
        }

        /// <summary>
        /// Gets a Boolean value indicating if reply is disabled on the ChatMessage.
        /// </summary>
        public bool IsReplyDisabled
        {
            get { return GetProperty(() => IsReplyDisabled); }
            internal set { SetProperty(() => IsReplyDisabled, value); }
        }

        /// <summary>
        /// Gets or sets a Boolean value indicating if the message has been seen.
        /// </summary>
        public bool IsSeen
        {
            get { return GetProperty(() => IsSeen); }
            set { SetProperty(() => IsSeen, value); }
        }

        /// <summary>
        /// Gets the local timestamp of the message.
        /// </summary>
        public DateTimeOffset LocalTimestamp
        {
            get { return GetProperty(() => LocalTimestamp); }
            set { SetProperty(() => LocalTimestamp, value); }
        }

        /// <summary>
        /// Gets or puts the type of the ChatMessage.
        /// </summary>
        public ChatMessageKind MessageKind
        {
            get { return GetProperty(() => MessageKind); }
            set { SetProperty(() => MessageKind, value); }
        }

        /// <summary>
        /// Gets the network timestamp of the message.
        /// </summary>
        public DateTimeOffset NetworkTimestamp
        {
            get { return GetProperty(() => NetworkTimestamp); }
            private set { SetProperty(() => NetworkTimestamp, value); }
        }

        /// <summary>
        /// Gets the list of recipients of the message.
        /// </summary>
        [Column.Ignore]
        public IEnumerable<string> Recipients
        {
            get
            {
                // First participant is always the current connected user
                foreach (var participant in this.ThreadingInfo.Participants.Skip(1))
                {
                    yield return participant;
                }
            }
        }

        /// <summary>
        /// Gets the delivery info for the recipient of the ChatMessage.
        /// </summary>
        [Relation.OneToMany(ForeignKey = "ChatMessageId")]
        public List<ChatRecipientDeliveryInfo> RecipientsDeliveryInfos
        {
            get { return GetProperty(() => RecipientsDeliveryInfos); }
            internal set { SetProperty(() => RecipientsDeliveryInfos, value); }
        }

        /// <summary>
        /// Gets the list of send statuses for the message.
        /// </summary>
        [Column.Ignore]
        public IReadOnlyDictionary<String, ChatMessageStatus> RecipientSendStatuses
        {
            get
            {
                return this.RecipientsDeliveryInfos?.ToDictionary(x => x.TransportAddress, x => x.Status);
            }
        }
        
        /// <summary>
        /// Gets or sets the remote ID for the ChatMessage.
        /// </summary>
        public string RemoteId
        {
            get { return GetProperty(() => RemoteId); }
            internal set { SetProperty(() => RemoteId, value); }
        }
        
        /// <summary>
        /// Gets or sets a Boolean value indicating if notification of receiving the ChatMessage should be suppressed.
        /// </summary>
        public bool ShouldSuppressNotification
        {
            get { return GetProperty(() => ShouldSuppressNotification); }
            set { SetProperty(() => ShouldSuppressNotification, value); }
        }

        /// <summary>
        /// Gets the status of the message.
        /// </summary>
        public ChatMessageStatus Status
        {
            get { return GetProperty(() => Status); }
            internal set { SetProperty(() => Status, value); }
        }

        /// <summary>
        /// Gets the subject of the message.
        /// </summary>
        public string Subject
        {
            get { return GetProperty(() => Subject); }
            internal set { SetProperty(() => Subject, value); }
        }

        /// <summary>
        /// Gets or sets the conversation threading info for the ChatMessage.
        /// </summary>
        [Relation.OneToOne]
        public ChatConversationThreadingInfo ThreadingInfo
        {
            get { return GetProperty(() => ThreadingInfo); }
            set { SetProperty(() => ThreadingInfo, value); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatMessage"/> class.
        /// </summary>
        public ChatMessage()
        {
            this.Attachments = new List<ChatMessageAttachment>();
            this.Status      = ChatMessageStatus.Draft;
        }

        /// <summary>
        /// Converts the current chat message to the XMPP format.
        /// </summary>
        /// <returns>The current chat message to the XMPP format.</returns>
        internal Message ToXmpp()
        {
            return new Message
            {
                Subject = new MessageSubject
                {
                    Value    = this.Subject
                  , Language = null
                }
              , Body = new MessageBody
                {
                    Value    = this.Body
                  , Language = null
                }
              , Delay  = null
              , From   = this.From
              , Thread = new MessageThread { Value = this.ThreadingInfo?.ConversationId }
              , To     = this.ThreadingInfo.ContactId
              , Type   = MessageType.Chat
              , Lang   = null
            };
        }

        public bool Equals(ChatMessage other)
        {
            if (other == null)
            {
                return false;
            }

            return (this.Id.Equals(other.Id));
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals(obj as ChatMessage);
        }

        public override int GetHashCode()
        {
            return (13 * 397) ^ this.Id.GetHashCode();
        }
    }
}

using Conversa.Net.Xmpp.Client;
using Conversa.Net.Xmpp.Core;
using System;
using System.Collections.Generic;

namespace Conversa.Net.Xmpp.InstantMessaging
{
    /// <summary>
    /// Represents a chat message.
    /// </summary>
    public sealed class ChatMessage
    {
        internal static ChatMessage Create(Message message)
        {
            return new ChatMessage
            {
                  // Attachments           = null
                  Body                       = message.Body?.Value
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
                , Recipients                 = new List<string> { message.To }
                , RecipientsDeliveryInfos    = null
                , RemoteId                   = message.Id
                , ShouldSuppressNotification = false
                , Status                     = ChatMessageStatus.Received
                , Subject                    = message.Subject?.Value
                , ThreadingInfo              = new ChatConversationThreadingInfo()
            };
        }

        /// <summary>
        /// Gets a list of chat message attachments.
        /// </summary>
        public IList<ChatMessageAttachment> Attachments
        {
            get;
            private set;
        }
        
        /// <summary>
        /// Gets or sets the body of the chat message.
        /// </summary>
        public string Body
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the estimated size of a file to be sent or recieved.
        /// </summary>
        public ulong EstimatedDownloadSize
        {
            get;
            set;
        }
 
        /// <summary>
        /// Gets the sender of the message.
        /// </summary>
        public string From
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the ID of the message.
        /// </summary>
        public string Id
        {
            get;
        } = Guid.NewGuid().ToString();

        /// <summary>
        /// Gets or sets a Boolean value indicating if the message is an auto-reply.
        /// </summary>
        public bool IsAutoReply
        {
            get;
            set;
        }
                
        /// <summary>
        /// Gets a value indicating if forwarding is disabled.
        /// </summary>
        public bool IsForwardingDisabled
        {
            get;
            private set;
        }
        
        /// <summary>
        /// Gets a value indicating if the message is incoming.
        /// </summary>
        public bool IsIncoming
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating if the message has been read.
        /// </summary>
        public bool IsRead
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets or sets a Boolean value indicating if the message was received during user specified quiet hours.
        /// </summary>
        public bool IsReceivedDuringQuietHours
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a Boolean value indicating if reply is disabled on the ChatMessage.
        /// </summary>
        public bool IsReplyDisabled
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets a Boolean value indicating if the message has been seen.
        /// </summary>
        public bool IsSeen
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the local timestamp of the message.
        /// </summary>
        public DateTimeOffset LocalTimestamp
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or puts the type of the ChatMessage.
        /// </summary>
        public ChatMessageKind MessageKind
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the network timestamp of the message.
        /// </summary>
        public DateTimeOffset NetworkTimestamp
        {
            get;
            private set;
        }
        
        /// <summary>
        /// Gets the list of recipients of the message.
        /// </summary>
        public IList<string> Recipients
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the delivery info for the recipient of the ChatMessage.
        /// </summary>
        public IList<ChatRecipientDeliveryInfo> RecipientsDeliveryInfos
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the list of send statuses for the message.
        /// </summary>
        public IReadOnlyDictionary<String, ChatMessageStatus> RecipientSendStatuses
        {
            get;
            private set;
        }
        
        /// <summary>
        /// Gets or sets the remote ID for the ChatMessage.
        /// </summary>
        public string RemoteId
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets or sets a Boolean value indicating if notification of receiving the ChatMessage should be suppressed.
        /// </summary>
        public bool ShouldSuppressNotification
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the status of the message.
        /// </summary>
        public ChatMessageStatus Status
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the subject of the message.
        /// </summary>
        public string Subject
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets or sets the conversation threading info for the ChatMessage.
        /// </summary>
        public ChatConversationThreadingInfo ThreadingInfo
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the transport friendly name of the message.
        /// </summary>
        public string TransportFriendlyName
        {
            get;            
            private set;
        }

        /// <summary>
        /// Gets or sets the transport ID of the message.
        /// </summary>
        public string TransportId
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatMessage"/> class.
        /// </summary>
        public ChatMessage()
        {
            var transport = XmppTransportManager.GetTransport();

            this.Attachments           = new List<ChatMessageAttachment>();
            this.TransportId           = transport.TransportId;
            this.TransportFriendlyName = TransportFriendlyName;
            this.Status                = ChatMessageStatus.Draft;
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
              , Thread = this.ThreadingInfo?.ToXmpp()
              , Delay = null
              , From  = this.From
              , Id    = Guid.NewGuid().ToString()
              , To    = this.ThreadingInfo?.ContactId
              , Type  = MessageType.Chat
              , Lang  = null
            };
        }
    }
}

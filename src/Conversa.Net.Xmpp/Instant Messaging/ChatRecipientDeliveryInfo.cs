using System;

namespace Conversa.Net.Xmpp.InstantMessaging
{
    /// <summary>
    /// Represent the delivery info about a chat recipient.
    /// </summary>
    public sealed class ChatRecipientDeliveryInfo
    {
        /// <summary>
        /// Gets or sets the time the message was sent to the recipient.
        /// </summary>
        public Nullable<DateTimeOffset> DeliveryTime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a Boolean value indicating whether the error for the message that was sent to the recipient is permanent.
        /// </summary>
        public bool IsErrorPermanent
        {
            get;
        }

        /// <summary>
        /// Gets or sets the time the recipient read the message.
        /// </summary>
        public Nullable<DateTimeOffset> ReadTime
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
        }
        
        /// <summary>
        /// Gets or sets the transport address of the recipient.
        /// </summary>
        public string TransportAddress
        {
            get;
            set;
        }
        
        /// <summary>
        /// Get the transport error code.
        /// </summary>
        public int TransportErrorCode
        {
            get;
        }

        /// <summary>
        /// Gets the category for the TransportErrorCode.
        /// </summary>
        public XmppTransportErrorCodeCategory TransportErrorCodeCategory
        {
            get;
        }

        /// <summary>
        /// Gets the interpreted error code for the transport.        
        /// </summary>
        public XmppTransportInterpretedErrorCode TransportInterpretedErrorCode
        {
            get;
        }

        /// <summary>
        /// Initializes a new instance of the ChatRecipientDeliveryInfo class.
        /// </summary>
        public ChatRecipientDeliveryInfo()
        {
        }
    }
}

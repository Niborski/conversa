// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Velox.DB;

namespace Conversa.Net.Xmpp.InstantMessaging
{
    /// <summary>
    /// Provides threading info for a ChatConversation.
    /// </summary>
    public sealed class ChatConversationThreadingInfo
    {
        [Column.PrimaryKey, Column.Name("ThreadingInfoId")]
        public string Id
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

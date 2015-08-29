// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using System;

namespace Conversa.Net.Xmpp.InstantMessaging
{
    /// <summary>
    /// Chat message validation result
    /// </summary>
    public sealed class ChatMessageValidationResult
    {
        /// <summary>
        /// Gets the maximum number of parts.
        /// </summary>
        public Nullable<uint> MaxPartCount
        {
            get;
        }

        /// <summary>
        /// Gets the number of parts.
        /// </summary>
        public Nullable<uint> PartCount
        {
            get;
        }

        /// <summary>
        /// The remaining characters in the part.
        /// </summary>
        public Nullable<uint> RemainingCharacterCountInPart
        {
            get;
        }

        /// <summary>
        /// Gets the status of the validation.
        /// </summary>
        public ChatMessageValidationStatus Status
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatMessageValidationResult"/> class.
        /// </summary>
        /// <param name="status">The status of the validation.</param>
        internal ChatMessageValidationResult(ChatMessageValidationStatus status)
        {
            this.Status = status;
        }
    }
}

// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Velox.DB;
using Windows.Storage.Streams;

namespace Conversa.Net.Xmpp.InstantMessaging
{
    /// <summary>
    /// Represents an attachment of a chat message.
    /// </summary>
    public sealed class ChatMessageAttachment
    {
        [Column.PrimaryKey, Column.Name("AttachmentId")]
        public string Id
        {
            get;
            set;
        }

        [Column.Name("MessageId")]
        public string ChatMessageId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the data stream for the attachment.
        /// </summary>
        [Column.Ignore]
        public IRandomAccessStreamReference DataStreamReference
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the group ID of the attachment.
        /// </summary>
        public uint GroupId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the MIME type of the attachment.
        /// </summary>
        public string MimeType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the original file name of the attachment.
        /// </summary>
        public string OriginalFileName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the text of the attachment.
        /// </summary>
        public string Text
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the thumbnail image for the attachment.
        /// </summary>
        [Column.Ignore]
        public IRandomAccessStreamReference Thumbnail
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the progress of transferring the attachment.
        /// </summary>
        public double TransferProgress
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatMessageAttachment"/> class.
        /// </summary>
        /// <param name="mimeType">The MIME type of the attachment.</param>
        /// <param name="dataStreamReference">A stream containing the attachment data.</param>
        public ChatMessageAttachment(string mimeType, IRandomAccessStreamReference dataStreamReference)
        {
            this.MimeType            = mimeType;
            this.DataStreamReference = dataStreamReference;
        }
    }
}

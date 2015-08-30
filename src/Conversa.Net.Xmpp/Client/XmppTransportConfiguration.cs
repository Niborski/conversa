// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Windows.Media.MediaProperties;
using Windows.Security.Cryptography.Certificates;

namespace Conversa.Net.Xmpp.Client
{
    /// <summary>
    /// Represents data about the XMPP message transport.
    /// </summary>
    public sealed class XmppTransportConfiguration
    {
        /// <summary>
        /// Get a vector of SSL server errors to ignore when making an secure connection.
        /// </summary>
        /// <returns>A vector of SSL server errors to ignore.</returns>
        public IList<ChainValidationResult> IgnorableServerCertificateErrors
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the extended properties of the transport.
        /// </summary>
        public IReadOnlyDictionary<String, Object> ExtendedProperties
        {
            get;
        }

        /// <summary>
        /// Gets the maximum attachment limit for a message on the transport.
        /// </summary>
        public int MaxAttachmentCount
        {
            get;
        } = -1;

        /// <summary>
        /// Gets the maximum size of an attachment for the transport.
        /// </summary>
        public int MaxMessageSizeInKilobytes
        {
            get;
        } = -1;

        /// <summary>
        /// Gets the maximum number of recipients for the transport.
        /// </summary>
        public int MaxRecipientCount
        {
            get;
            internal set;
        } = -1;

        /// <summary>
        /// Gets the supported video encoding format for the transport.
        /// </summary>
        public MediaEncodingProfile SupportedVideoFormat
        {
            get;
        }
    
        /// <summary>
        /// Initializes a new instance of the <see cref="XmppTransportConfiguration"/> class.
        /// </summary>
        internal XmppTransportConfiguration()
        {
        }
    }
}

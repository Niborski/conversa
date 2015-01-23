// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using System;

namespace Conversa.Net.Xmpp.Client
{
    /// <summary>
    /// Speficies feature flags supported bythe XMPP server.
    /// </summary>
    [Flags]
    internal enum XmppStreamFeatures
    {
        /// <summary>
        /// No features detected
        /// </summary>
        None                = 0,
        /// <summary>
        /// TLS Connections.
        /// </summary>
        SecureConnection	= 1,
        /// <summary>
        /// SASL Plain authentication mechanism.
        /// </summary>
        SaslPlain			= 2,
        /// <summary>
        /// SASL Digest authentication mechanism.
        /// </summary>
        SaslDigestMD5		= 4,
        /// <summary>
        /// SASL SCRAM-SHA-1 authentication mechanism.
        /// </summary>
        SaslScramSha1       = 8,
        /// <summary>
        /// Google SASL X-OAUTH-2 authentication mechanism.
        /// </summary>
        SaslGoogleXOAuth2   = 16,
        /// <summary>
        /// Resource binding.
        /// </summary>
        ResourceBinding		= 32,
        /// <summary>
        /// Session Binding
        /// </summary>
        Sessions			= 64,
        /// <summary>
        /// In-Band registration of users.
        /// </summary>
        InBandRegistration	= 128
    }
}

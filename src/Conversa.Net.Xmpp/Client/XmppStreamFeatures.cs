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
        /// SASL X-OAUTH-2
        /// </summary>
        SaslXOAuth2         = 2,
        /// <summary>
        /// SASL Digest Authentication Mechanism.
        /// </summary>
        SaslDigestMD5		= 4,
        /// <summary>
        /// SASL Plaint Authentication Mechanism.
        /// </summary>
        SaslPlain			= 8,
        /// <summary>
        /// Resource binding.
        /// </summary>
        ResourceBinding		= 16,
        /// <summary>
        /// Session Binding
        /// </summary>
        Sessions			= 32,
        /// <summary>
        /// In-Band registration of users.
        /// </summary>
        InBandRegistration	= 64
    }
}

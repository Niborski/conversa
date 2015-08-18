// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Core
{
    /// <summary>
    /// Internal constants
    /// </summary>
    internal static class XmppCodes
    {
        /// <summary>
        /// XMPP Stream initialization format
        /// </summary>
        internal const string StreamFormat = "<?xml version='1.0' encoding='UTF-8' ?><stream:stream xmlns='jabber:client' xmlns:stream='http://etherx.jabber.org/streams' to='{0}' version='1.0'>";

        /// <summary>
        /// XMPP Stream XML open node tag
        /// </summary>
        internal static readonly string StartStream = "<stream:stream ";

        /// <summary>
        /// XMPP Stream close format
        /// </summary>
        internal const string EndStream = "</stream:stream>";

        /// <summary>
        /// Code for the SASL PLAIN authentication mechanism
        /// </summary>
        internal const string SaslPlainMechanism = "PLAIN";

        /// <summary>
        /// Code for the SASL Digest authentication mechanism
        /// </summary>
        internal const string SaslDigestMD5Mechanism = "DIGEST-MD5";

        /// <summary>
        /// Code for the SCRAM-SHA1 authentication mechanism
        /// </summary>
        internal const string SaslScramSha1Mechanism = "SCRAM-SHA-1";

        /// <summary>
        /// Code for the Google SASL X-OAUTH-2 authentication mechanism
        /// </summary>
        internal const string SaslGoogleXOAuth2Authenticator = "X-OAUTH2";

        /// <summary>
        /// XMPP DNS SRV Record prefix
        /// </summary>
        internal const string XmppSrvRecordPrefix = "_xmpp-client._tcp";
    }
}

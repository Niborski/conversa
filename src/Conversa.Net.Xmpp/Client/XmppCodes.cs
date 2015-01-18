// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Client
{
    /// <summary>
    /// Internal constants
    /// </summary>
    internal static class XmppCodes
    {
        /// <summary>
        /// Namespace of the XMPP stream
        /// </summary>
        internal const string StreamNamespace = "jabber:client";

        /// <summary>
        /// URI of the XMPP stream
        /// </summary>
        internal const string StreamURI = "http://etherx.jabber.org/streams";

        /// <summary>
        /// Version of the XMPP stream
        /// </summary>
        internal const string StreamVersion = "1.0";

        /// <summary>
        /// XMPP Stream initialization format
        /// </summary>
        internal const string StreamFormat = "<?xml version='1.0' encoding='UTF-8' ?><stream:stream xmlns='{0}' xmlns:stream='{1}' to='{2}' version='{3}'>";

        /// <summary>
        /// XMPP Stream XML open node tag
        /// </summary>
        internal static readonly string StartStream = "<stream:stream ";

        /// <summary>
        /// XMPP Stream close format
        /// </summary>
        internal const string EndStream = "</stream:stream>";

        /// <summary>
        /// Code for the SASL Digest authentication mechanism
        /// </summary>
        internal const string SaslDigestMD5Mechanism = "DIGEST-MD5";

        /// <summary>
        /// Code for the SASL PLAIN authentication mechanism
        /// </summary>
        internal const string SaslPlainMechanism = "PLAIN";

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

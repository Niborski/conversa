// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Authentication
{
    /// <summary>
    /// Base class for authentication mechanims implementations.
    /// </summary>
    internal interface IXmppAuthenticator
    {
        object StartSaslNegotiation();
        object ContinueNegotiationWith(object message);
    }
}

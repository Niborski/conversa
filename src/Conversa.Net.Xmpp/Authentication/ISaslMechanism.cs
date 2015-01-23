// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.Core;

namespace Conversa.Net.Xmpp.Authentication
{
    /// <summary>
    /// Base class for SASL mechanims implementations.
    /// </summary>
    internal interface ISaslMechanism
    {
        SaslAuth StartSaslNegotiation();
        SaslResponse ProcessChallenge(SaslChallenge challenge);
        SaslResponse ProcessResponse(SaslResponse response);
        bool ProcessSuccess(SaslSuccess success);
    }
}

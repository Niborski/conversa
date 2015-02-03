// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.Core;

namespace Conversa.Net.Xmpp.Client.Authentication
{
    /// <summary>
    /// Base class for SASL mechanims implementations.
    /// </summary>
    internal interface ISaslMechanism
    {
        /// <summary>
        /// Starts the SASL negotiation process.
        /// </summary>
        /// <returns>A SASL auth instance.</returns>
        SaslAuth StartSaslNegotiation();

        /// <summary>
        /// Process the SASL challenge message.
        /// </summary>
        /// <param name="challenge">The server challenge.</param>
        /// <returns>The challenge response.</returns>
        SaslResponse ProcessChallenge(SaslChallenge challenge);

        /// <summary>
        /// Process the SASL reponse message.
        /// </summary>
        /// <param name="response">The server reponse</param>
        /// <returns>The client response.</returns>
        SaslResponse ProcessResponse(SaslResponse response);

        /// <summary>
        /// Verifies the SASL success message if needed.
        /// </summary>
        /// <param name="success">The server success response</param>
        /// <returns><b>true</b> if the reponse has been verified; otherwise <b>false</b></returns>
        bool ProcessSuccess(SaslSuccess success);
    }
}

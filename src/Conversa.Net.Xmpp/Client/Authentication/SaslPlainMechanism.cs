// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.Core;
using System;
using System.Text;

namespace Conversa.Net.Xmpp.Client.Authentication
{
    /// <summary>
    /// SASL Plain authentication mechanism.
    /// </summary>
    /// <remarks>
    /// References:
    ///     http://www.ietf.org/rfc/rfc4616.txt
    /// </remarks>
    internal sealed class SaslPlainMechanism
        : ISaslMechanism
    {
        private XmppConnectionString connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SaslPlainMechanism"/> class.
        /// </summary>
        public SaslPlainMechanism(XmppConnectionString connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Starts the SASL negotiation process.
        /// </summary>
        /// <returns>
        /// A SASL auth instance.
        /// </returns>
        public SaslAuth StartSaslNegotiation()
        {
            var address = this.connectionString.UserAddress;
            var message = "\0" + this.connectionString.UserAddress.UserName
                        + "\0" + this.connectionString.UserPassword;

            return new SaslAuth
            {
                Mechanism = XmppCodes.SaslPlainMechanism
              , Value     = Encoding.UTF8.GetBytes(message).ToBase64String()
            };
        }

        /// <summary>
        /// Process the SASL challenge message.
        /// </summary>
        /// <param name="challenge">The server challenge.</param>
        /// <returns>
        /// The challenge response.
        /// </returns>
        public SaslResponse ProcessChallenge(SaslChallenge challenge)
        {
            return null;
        }

        /// <summary>
        /// Process the SASL reponse message.
        /// </summary>
        /// <param name="response">The server reponse</param>
        /// <returns>
        /// The client response.
        /// </returns>
        public SaslResponse ProcessResponse(SaslResponse response)
        {
            return null;
        }

        /// <summary>
        /// Verifies the SASL success message if needed.
        /// </summary>
        /// <param name="success">The server success response</param>
        /// <returns>
        ///   <b>true</b> if the reponse has been verified; otherwise <b>false</b>
        /// </returns>
        public bool ProcessSuccess(SaslSuccess success)
        {
            return true;
        }
    }
}

// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.Client;
using Conversa.Net.Xmpp.Core;
using System;
using System.Text;

namespace Conversa.Net.Xmpp.Authentication
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

        public SaslResponse ProcessChallenge(SaslChallenge challenge)
        {
            return null;
        }

        public SaslResponse ProcessResponse(SaslResponse response)
        {
            return null;
        }
    }
}

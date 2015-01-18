// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.Client;
using Conversa.Net.Xmpp.Core;
using System;
using System.Text;

namespace Conversa.Net.Xmpp.Authentication
{
    /// <summary>
    /// <see cref="XmppAuthenticator" /> implementation for the SASL Plain Authentication mechanism.
    /// </summary>
    /// <remarks>
    /// References:
    ///     http://www.ietf.org/rfc/rfc4616.txt
    /// </remarks>
    internal sealed class XmppSaslPlainAuthenticator
        : IXmppAuthenticator
    {
        private XmppConnectionString connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:XmppSaslPlainAuthenticator"/> class.
        /// </summary>
        public XmppSaslPlainAuthenticator(XmppConnectionString connectionString)
        {
            this.connectionString = connectionString;
        }

        public object StartSaslNegotiation()
        {
            // Send authentication mechanism
            var auth = new SaslAuth
            {
                Mechanism = XmppCodes.SaslPlainMechanism
              , Value     = this.BuildMessage()
            };

            return auth;
        }

        public object ContinueNegotiationWith(object message)
        {
            return null;
        }

        private string BuildMessage()
        {
            var address = this.connectionString.UserAddress;
            var message = String.Format("\0{0}\0{1}", address.UserName, this.connectionString.UserPassword);

            return Encoding.UTF8.GetBytes(message).ToBase64String();
        }
    }
}

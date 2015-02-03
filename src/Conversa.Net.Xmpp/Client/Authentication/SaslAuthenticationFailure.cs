// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Client.Authentication
{
    /// <summary>
    /// SASL Authentication failure
    /// </summary>
    public sealed class SaslAuthenticationFailure
    {
        /// <summary>
        /// Gets the authentication failiure message.
        /// </summary>
        /// <value>The message.</value>
        public string Message
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SaslAuthenticationFailure"/> class.
        /// </summary>
        /// <param name="message">The authentication failiure message.</param>
        internal SaslAuthenticationFailure(string message)
        {
            this.Message = message;
        }
    }
}

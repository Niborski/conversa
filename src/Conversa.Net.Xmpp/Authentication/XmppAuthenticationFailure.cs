// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.


namespace Conversa.Net.Xmpp.Authentication
{
    /// <summary>
    /// EventArgs for the <see cref="XmppClient.AuthenticationFailiure"/> event.
    /// </summary>
    public sealed class XmppAuthenticationFailure
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
        /// Initializes a new instance of the <see cref="XmppAuthenticationFailure"/> class.
        /// </summary>
        /// <param name="message">The authentication failiure message.</param>
        internal XmppAuthenticationFailure(string message)
        {
            this.Message = message;
        }
    }
}

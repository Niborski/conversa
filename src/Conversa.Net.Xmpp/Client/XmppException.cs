// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.Core;
using System;

namespace Conversa.Net.Xmpp.Client
{
    /// <summary>
    /// Exception for XMPP related errors.
    /// </summary>
    public sealed class XmppException
        : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:XmppException"/> class.
        /// </summary>
        public XmppException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:XmppException"/> class with the given class
        /// </summary>
        /// <param name="message">The XMPP Stream error.</param>
        public XmppException(StreamError error)
            : base(error.GetErrorMessage())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:XmppException"/> class with the given class
        /// </summary>
        /// <param name="message">The message.</param>
        public XmppException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:XmppException"/> class with the
        /// given message and inner exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public XmppException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}

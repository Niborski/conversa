// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.InstantMessaging;

namespace Conversa.Net.Xmpp.PersonalEventing
{
    /// <summary>
    /// XMPP User event activity ( tunes, moods, ... )
    /// </summary>
    public abstract class XmppUserEvent
        : XmppEvent
    {
        private XmppContact user;

        /// <summary>
        /// Gets the user data
        /// </summary>
        public XmppContact User
        {
            get { return this.user; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmppUserEvent"/> class.
        /// </summary>
        /// <param name="user"></param>
        protected XmppUserEvent()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmppUserEvent"/> class.
        /// </summary>
        /// <param name="user"></param>
        protected XmppUserEvent(XmppContact user)
        {
            this.user = user;
        }
    }
}

// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.InstantMessaging;

namespace Conversa.Net.Xmpp.Eventing
{
    /// <summary>
    /// XMPP User event activity ( tunes, moods, ... )
    /// </summary>
    public abstract class UserEvent
        : Event
    {
        private Contact user;

        /// <summary>
        /// Gets the user data
        /// </summary>
        public Contact User
        {
            get { return this.user; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserEvent"/> class.
        /// </summary>
        /// <param name="user"></param>
        protected UserEvent()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserEvent"/> class.
        /// </summary>
        /// <param name="user"></param>
        protected UserEvent(Contact user)
        {
            this.user = user;
        }
    }
}

// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.Core;
using System.Threading.Tasks;

namespace Conversa.Net.Xmpp.Client
{
    /// <summary>
    /// XMPP Client Presence
    /// </summary>
    public sealed class XmppTransportPresence
    {
        private Presence presence;

        public bool IsOffline
        {
            get { return this.presence == null || this.presence.IsUnavailable; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmppTransportPresence"/> class.
        /// </summary>
        /// <param name="transport"></param>
        public XmppTransportPresence()
        {
        }

        /// <summary>
        /// Set the presence as Online
        /// </summary>
        public async Task SetOnlineAsync()
        {
            await this.SetPresenceAsync(ShowType.Online).ConfigureAwait(false);
        }

        /// <summary>
        /// Sets the presence as Unavailable
        /// </summary>
        public async Task SetUnavailableAsync()
        {
            var transport = XmppTransportManager.GetTransport();

            await transport.SendAsync(new Presence().AsUnavailable()).ConfigureAwait(false);
        }

        /// <summary>
        /// Sets the presense state.
        /// </summary>
        /// <param name="presenceState"></param>
        public async Task SetPresenceAsync(ShowType presenceState)
        {
            await this.SetPresenceAsync(presenceState, null).ConfigureAwait(false);
        }

        /// <summary>
        /// Sets the presence state with the given state and status message
        /// </summary>
        /// <param name="showAs"></param>
        /// <param name="statusMessage"></param>
        public async Task SetPresenceAsync(ShowType showAs, string statusMessage)
        {
            await this.SetPresenceAsync(showAs, statusMessage, 0).ConfigureAwait(false);
        }

        /// <summary>
        /// Sets the presence state with the given state, status message and priority
        /// </summary>
        /// <param name="showAs"></param>
        /// <param name="statusMessage"></param>
        /// <param name="priority"></param>
        public async Task SetPresenceAsync(ShowType showAs, string statusMessage, int priority)
        {
            var transport = XmppTransportManager.GetTransport();
            this.presence = new Presence
            {
                From              = transport.UserAddress
              , Show              = ShowType.Online
              , ShowSpecified     = true
              , Status            = new Status { Value = statusMessage }
              , Priority          = (sbyte)priority
              , PrioritySpecified = true
            };

            await transport.SendAsync(this.presence).ConfigureAwait(false);
        }

        /// <summary>
        /// Sets the initial presence state
        /// </summary>
        /// <returns></returns>
        internal async Task SetInitialPresenceAsync()
        {
            await this.SetPresenceAsync(ShowType.Online).ConfigureAwait(false);
        }
    }
}

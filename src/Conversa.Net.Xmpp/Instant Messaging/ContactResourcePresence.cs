// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.Client;
using Conversa.Net.Xmpp.Core;
using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace Conversa.Net.Xmpp.InstantMessaging
{
    /// <summary>
    /// Contact resource presence handling
    /// </summary>
    public sealed class ContactResourcePresence
        : StanzaHub
    {
        private Subject<ContactResource> presenceStream;
        private ContactResource          resource;
        private ShowType                 showAs;
        private int                      priority;
        private string                   statusMessage;

        /// <summary>
        /// Occurs when the contact presence is updated
        /// </summary>
        public IObservable<ContactResource> PresenceChanged
        {
            get { return this.presenceStream.AsObservable(); }
        }

        /// <summary>
        /// Gets or sets the presence priority.
        /// </summary>
        /// <value>The priority.</value>
        public int Priority
        {
            get { return this.priority; }
            set { this.priority = value; }
        }

        /// <summary>
        /// Gets or sets the presence status.
        /// </summary>
        /// <value>The presence status.</value>
        public ShowType ShowAs
        {
            get { return this.showAs; }
            set { this.showAs = value; }
        }

        /// <summary>
        /// Gets or sets the presence status message.
        /// </summary>
        /// <value>The presence status message.</value>
        public string StatusMessage
        {
            get { return this.statusMessage; }
            set { this.statusMessage = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactResourcePresence"/> class using
        /// the given session.
        /// </summary>
        /// <param name="session"></param>
        internal ContactResourcePresence(XmppClient client, ContactResource resource)
            : base(client)
        {
            this.resource       = resource;
            this.showAs         = ShowType.Offline;
            this.presenceStream = new Subject<ContactResource>();
        }

        /// <summary>
        /// Gets the presence of the given user.
        /// </summary>
        /// <param name="address">User address</param>
        public async Task GetPresenceAsync()
        {
            var presence = new Presence
            {
                From          = this.Client.UserAddress
              , To            = this.resource.Address
              , Type          = PresenceType.Probe
              , TypeSpecified = true
            };

            await this.SendAsync(presence).ConfigureAwait(false);
        }

        /// <summary>
        /// Set the presence as <see cref="XmppPresenceState.Available"/>
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
            await this.SendAsync(new Presence().AsUnavailable()).ConfigureAwait(false);
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
            var presence = new Presence
            {
                Show              = ShowType.Online
              , ShowSpecified     = true
              , Status            = new Status { Value = statusMessage }
              , Priority          = (sbyte)priority
              , PrioritySpecified = true
            };

            await this.SendAsync(presence).ConfigureAwait(false);
        }

        /// <summary>
        /// Request subscription to the given user
        /// </summary>
        public async Task RequestSubscriptionAsync()
        {
            var presence = new Presence
            {
                To            = this.resource.Address
              , Type          = PresenceType.Subscribe
              , TypeSpecified = true
            };

            await this.SendAsync(presence).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribes to presence updates of the current user
        /// </summary>
        public async Task SubscribedAsync()
        {
            var presence = new Presence
            {
                To            = this.resource.Address
              , Type          = PresenceType.Subscribed
              , TypeSpecified = true
            };

            await this.SendAsync(presence).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribes to presence updates of the current user
        /// </summary>
        public async Task UnsuscribeAsync()
        {
            var presence = new Presence
            {
                To            = this.resource.Address
              , Type          = PresenceType.Unsubscribe
              , TypeSpecified = true
            };

            await this.SendAsync(presence).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribes to presence updates of the current user
        /// </summary>
        public async Task UnsuscribedAsync()
        {
            var presence = new Presence
            {
                To            = this.resource.Address
              , Type          = PresenceType.Unsubscribed
              , TypeSpecified = true
            };

            await this.SendAsync(presence).ConfigureAwait(false);
        }

        protected override void OnDisconnected()
        {
            this.resource = null;
            this.presenceStream.Dispose();
        }

        internal void Update(Presence presence)
        {
            this.ShowAs = ShowType.Online;

            if (presence.TypeSpecified && presence.Type == PresenceType.Unavailable)
            {
                this.ShowAs = ShowType.Offline;
            }
            else if (presence.ShowSpecified)
            {
                this.ShowAs = presence.Show;
            }

            if (presence.PrioritySpecified)
            {
                this.Priority = presence.Priority;
            }

            this.StatusMessage = ((presence.Status == null) ? String.Empty : presence.Status.Value);

            this.presenceStream.OnNext(this.resource);
        }
    }
}

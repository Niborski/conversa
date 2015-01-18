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
    /// Presence handling
    /// </summary>
    public sealed class XmppPresence
        : XmppMessageProcessor
    {
        private static int DefaultPresencePriorityValue = -200;

        private Subject<XmppContactResource> presenceStream;
        private XmppContactResource          resource;
        private ShowType                     showAs;
        private int                          priority;
        private string                       statusMessage;

        /// <summary>
        /// Occurs when the contact presence is updated
        /// </summary>
        public IObservable<XmppContactResource> PresenceChanged
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

        public bool IsOnline
        {
            get { return this.ShowAs == ShowType.Online; }
        }

        public bool IsAway
        {
            get { return this.ShowAs == ShowType.Away; }
        }

        public bool IsBusy
        {
            get { return this.ShowAs == ShowType.Busy; }
        }

        public bool IsExtendedAway
        {
            get { return this.ShowAs == ShowType.ExtendedAway; }
        }

        public bool IsOffline
        {
            get { return this.ShowAs == ShowType.Offline; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmppPresence"/> class using
        /// the given session.
        /// </summary>
        /// <param name="session"></param>
        internal XmppPresence(XmppClient client, XmppContactResource resource)
            : base(client)
        {
            this.resource       = resource;
            this.showAs         = ShowType.Offline;
            this.presenceStream = new Subject<XmppContactResource>();
        }

        /// <summary>
        /// Gets the presence of the given user.
        /// </summary>
        /// <param name="address">User address</param>
        public async Task GetPresenceAsync()
        {
            var presence = Presence.Create()
                                   .AsProbe()
                                   .FromAddress(this.Client.UserAddress)
                                   .ToAddress(this.resource.Address);

            await this.Client.SendAsync(presence);
        }

        /// <summary>
        /// Sets the initial presence against the given user.
        /// </summary>
        /// <param name="target">XMPP Address of the target user.</param>
        public async Task SetDefaultPresenceAsync()
        {
            var presence = Presence.Create()
                                   .ToAddress(this.resource.Address.BareAddress)
                                   .ShowAs(ShowType.Online)
                                   .WithPriority(DefaultPresencePriorityValue);

            await this.Client.SendAsync(presence);
        }

        /// <summary>
        /// Set the presence as <see cref="XmppPresenceState.Available"/>
        /// </summary>
        public async Task SetOnlineAsync()
        {
            await this.SetPresenceAsync(ShowType.Online);
        }

        /// <summary>
        /// Sets the presence as Unavailable
        /// </summary>
        public async Task SetUnavailableAsync()
        {
            await this.Client.SendAsync(Presence.Create().AsUnavailable());
        }

        /// <summary>
        /// Sets the presense state.
        /// </summary>
        /// <param name="presenceState"></param>
        public async Task SetPresenceAsync(ShowType presenceState)
        {
            await this.SetPresenceAsync(presenceState, null);
        }

        /// <summary>
        /// Sets the presence state with the given state and status message
        /// </summary>
        /// <param name="showAs"></param>
        /// <param name="statusMessage"></param>
        public async Task SetPresenceAsync(ShowType showAs, string statusMessage)
        {
            await this.SetPresenceAsync(showAs, statusMessage, 0);
        }

        /// <summary>
        /// Sets the presence state with the given state, status message and priority
        /// </summary>
        /// <param name="showAs"></param>
        /// <param name="statusMessage"></param>
        /// <param name="priority"></param>
        public async Task SetPresenceAsync(ShowType showAs, string statusMessage, int priority)
        {
            var presence = Presence.Create()
                                   .ShowAs(showAs)
                                   .WithStatus(statusMessage)
                                   .WithPriority(priority);

            await this.Client.SendAsync(presence);
        }

        /// <summary>
        /// Request subscription to the given user
        /// </summary>
        public async Task RequestSubscriptionAsync()
        {
            var presence = Presence.Create().AsSubscribe().ToAddress(this.resource.Address);

            await this.Client.SendAsync(presence);
        }

        /// <summary>
        /// Subscribes to presence updates of the current user
        /// </summary>
        public async Task SubscribedAsync()
        {
            var presence = Presence.Create().AsSubscribed().ToAddress(this.resource.Address);

            await this.Client.SendAsync(presence);
        }

        /// <summary>
        /// Subscribes to presence updates of the current user
        /// </summary>
        public async Task UnsuscribedAsync()
        {
            var presence = Presence.Create().AsUnsubscribed().ToAddress(this.resource.Address);

            await this.Client.SendAsync(presence);
        }

        protected override void OnClientDisconnected()
        {
            this.resource = null;
            this.presenceStream.Dispose();
        }

        internal void Update(Presence presence)
        {
            if (presence.TypeSpecified && presence.Type == PresenceType.Unavailable)
            {
                this.ShowAs = ShowType.Offline;
            }
            else
            {
                this.ShowAs = ShowType.Online;
            }

            this.Priority      = presence.Priority;
            this.ShowAs        = presence.Show;
            this.StatusMessage = ((presence.Status == null) ? String.Empty : presence.Status.Value);

            this.presenceStream.OnNext(this.resource);
        }
    }
}

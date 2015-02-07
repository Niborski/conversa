// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.Blocking;
using Conversa.Net.Xmpp.Client;
using Conversa.Net.Xmpp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace Conversa.Net.Xmpp.InstantMessaging
{
    /// <summary>
    /// Represents a <see cref="ContactList"/> contact.
    /// </summary>
    public sealed class Contact
        : StanzaHub
    {
        private Subject<ContactBlockingAction> blockingStream;
        private Subject<ContactResource>       newResourceStream;
        private Subject<ContactResource>       removedResourceStream;

        private string                 name;
        private string                 displayName;
        private XmppAddress            address;
        private RosterSubscriptionType subscription;
        private List<ContactResource>  resources;
        private List<string>           groups;

        /// <summary>
        /// Occurs when the contact blocking status changes.
        /// </summary>
        public IObservable<ContactBlockingAction> BlockingStream
        {
            get { return this.blockingStream.AsObservable(); }
        }

        /// <summary>
        /// Occurs when a new contact resource is added to the contact resource list.
        /// </summary>
        public IObservable<ContactResource> NewResourceStream
        {
            get { return this.newResourceStream.AsObservable(); }
        }

        /// <summary>
        /// Occurs when a new contact resource is removed from the contact resource list.
        /// </summary>
        public IObservable<ContactResource> RemovedResourceStream
        {
            get { return this.removedResourceStream.AsObservable(); }
        }

        /// <summary>
        /// Gets the contact address.
        /// </summary>
        /// <value>The contact address.</value>
        public XmppAddress Address
        {
            get { return this.address; }
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        /// <summary>
        /// Gets the contact Display Name
        /// </summary>
        public string DisplayName
        {
            get { return this.displayName; }
            set { this.displayName = value; }
        }

        /// <summary>
        /// Gets the contact groups.
        /// </summary>
        /// <value>The groups.</value>
        public IEnumerable<string> Groups
        {
            get { return this.groups.AsEnumerable(); }
        }

        /// <summary>
        /// Gets the list available resources.
        /// </summary>
        /// <value>The resources.</value>
        public IEnumerable<ContactResource> Resources
        {
            get { return this.resources.AsEnumerable(); }
        }

        /// <summary>
        /// Gets or sets the subscription.
        /// </summary>
        /// <value>The subscription.</value>
        public RosterSubscriptionType Subscription
        {
            get { return this.subscription; }
            set { this.subscription = value; }
        }

        /// <summary>
        /// Gets a value that indicates if the contact supports File Transfer
        /// </summary>
        public bool SupportsFileTransfer
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:XmppContact"/> class.
        /// </summary>
        /// <param name="client">The XMPP client instance.</param>
        /// <param name="address">The contact address.</param>
        /// <param name="name">The contact name.</param>
        /// <param name="subscription">The contact subscription mode.</param>
        /// <param name="groups">The contact groups.</param>
        internal Contact(XmppClient             client
                       , XmppAddress            address
                       , string                 name
                       , RosterSubscriptionType subscription
                       , IEnumerable<string>    groups)
            : base(client)
        {
            this.address               = address.BareAddress;
            this.groups                = new List<string>();
            this.resources             = new List<ContactResource>();
            this.blockingStream        = new Subject<ContactBlockingAction>();
            this.newResourceStream     = new Subject<ContactResource>();
            this.removedResourceStream = new Subject<ContactResource>();

            this.Update(name, subscription, groups);

            this.SubscribeToPresenceChanges();
        }

        /// <summary>
        /// Block contact
        /// </summary>
        public async Task BlockAsync()
        {
            if (!this.Client.ServiceDiscovery.SupportsBlocking)
            {
                return;
            }

            var iq = new InfoQuery
            {
                From  = this.Client.UserAddress
              , Type  = InfoQueryType.Set
              , Block = new Block(this.Address)
            };

            await this.SendAsync(iq).ConfigureAwait(false);
        }

        /// <summary>
        /// Unblock contact.
        /// </summary>
        public async Task UnBlockAsync()
        {
            if (!this.Client.ServiceDiscovery.SupportsBlocking)
            {
                return;
            }

            var iq = new InfoQuery
            {
                From    = this.Client.UserAddress
              , Type    = InfoQueryType.Set
              , Unblock = new Unblock(this.Address)
            };

            await this.SendAsync(iq).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribes to presence updates of the current user
        /// </summary>
        public async Task AcceptSubscriptionAsync()
        {
            var presence = new Presence
            {
                To            = this.Address
              , Type          = PresenceType.Subscribed
              , TypeSpecified = true
            };

            await this.SendAsync(presence).ConfigureAwait(false);
        }

        /// <summary>
        /// Unsubscribes from presence updates of the current user
        /// </summary>
        public async Task UnsuscribeAsync()
        {
            var presence = new Presence
            {
                To            = this.Address
              , Type          = PresenceType.Unsubscribe
              , TypeSpecified = true
            };

            await this.SendAsync(presence).ConfigureAwait(false);
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </returns>
        public override string ToString()
        {
            return this.address.ToString();
        }

        internal void Update(string name, RosterSubscriptionType subscription, IEnumerable<string> groups)
        {
            this.name         = ((name == null) ? String.Empty : name);
            this.displayName  = (!String.IsNullOrEmpty(this.name) ? this.name : this.address.UserName);
            this.subscription = subscription;

            if (groups != null && groups.Count() > 0)
            {
                this.groups.AddRange(groups);
            }
            else
            {
                this.groups.Add("Contacts");
            }
        }

        private void SubscribeToPresenceChanges()
        {
            this.AddSubscription(this.Client
                                     .PresenceStream
                                     .Where(message => ((XmppAddress)message.From).BareAddress == this.Address
                                                    && !message.IsError)
                                     .Subscribe(message => this.OnPresenceChanged(message)));
        }

        private async void OnPresenceChanged(Presence message)
        {
            var resource = this.resources.SingleOrDefault(contactResource => contactResource.Address == message.From);

            if (resource == null)
            {
                resource = new ContactResource(this.Client, message.From, message);

                this.resources.Add(resource);
                this.newResourceStream.OnNext(resource);

                await resource.Capabilities.DiscoverAsync().ConfigureAwait(false);
            }
            else
            {
                if (message.TypeSpecified)
                {
                    switch (message.Type)
                    {
                        case PresenceType.Probe:
                            break;

                        case PresenceType.Subscribe:
                            // auto-accept subscription requests
                            await this.AcceptSubscriptionAsync().ConfigureAwait(false);
                            break;

                        case PresenceType.Unavailable:
                            await this.UpdateResourceAsync(resource, message).ConfigureAwait(false);
                            break;

                        case PresenceType.Unsubscribe:
                            await this.UnsuscribedAsync().ConfigureAwait(false);
                            break;
                    }
                }
                else
                {
                    await this.UpdateResourceAsync(resource, message).ConfigureAwait(false);
                }
            }
        }

        private async Task UpdateResourceAsync(ContactResource resource, Presence message)
        {
            await resource.UpdateAsync(message).ConfigureAwait(false);

            // Remove the resource information if the contact has gone offline
            if (resource.IsOffline)
            {
                this.resources.Remove(resource);
                this.removedResourceStream.OnNext(resource);
            }
        }

        private async Task UnsuscribedAsync()
        {
            var presence = new Presence
            {
                To            = this.Address
              , Type          = PresenceType.Unsubscribed
              , TypeSpecified = true
            };

            await this.SendAsync(presence).ConfigureAwait(false);
        }
    }
}

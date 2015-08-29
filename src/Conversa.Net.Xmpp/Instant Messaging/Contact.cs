// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.Blocking;
using Conversa.Net.Xmpp.Client;
using Conversa.Net.Xmpp.Core;
using DevExpress.Mvvm;
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
        : BindableBase
    {
        private Subject<ContactBlockingAction> blockingStream;
        private Subject<ContactResource>       newResourceStream;
        private Subject<ContactResource>       removedResourceStream;

        private XmppAddress            address;
        private List<ContactResource>  resources;
        private List<string>           groups;
        private IDisposable            lastActivitySequence;

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
            get { return GetProperty(() => Name); }
            set { SetProperty(() => Name, value); }
        }

        /// <summary>
        /// Gets the contact Display Name
        /// </summary>
        public string DisplayName
        {
            get { return GetProperty(() => DisplayName); }
            set { SetProperty(() => DisplayName, value); }
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
        /// <value>The contact resources.</value>
        public IEnumerable<ContactResource> Resources
        {
            get { return this.resources.AsEnumerable(); }
        }

        /// <summary>
        /// Gets the contact high priority resource.
        /// </summary>
        public ContactResource HighPriorityResource
        {
            get { return this.resources.OrderByDescending(x => x.Presence.Priority).FirstOrDefault(); }
        }

        /// <summary>
        /// Gets or sets the subscription.
        /// </summary>
        /// <value>The subscription.</value>
        public RosterSubscriptionType Subscription
        {
            get { return GetProperty(() => Subscription); }
            set { SetProperty(() => Subscription, value); }
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
        /// <param name="address">The contact address.</param>
        /// <param name="name">The contact name.</param>
        /// <param name="subscription">The contact subscription mode.</param>
        /// <param name="groups">The contact groups.</param>
        internal Contact(XmppAddress            address
                       , string                 name
                       , RosterSubscriptionType subscription
                       , IEnumerable<string>    groups)
        {
            this.address               = address.BareAddress;
            this.groups                = new List<string>();
            this.resources             = new List<ContactResource>();
            this.blockingStream        = new Subject<ContactBlockingAction>();
            this.newResourceStream     = new Subject<ContactResource>();
            this.removedResourceStream = new Subject<ContactResource>();
            
            this.Update(name, subscription, groups);
            this.AddDefaultResource();

            this.SubscribeToPresenceChanges();
        }

        /// <summary>
        /// Subscribes to presence updates of the current user
        /// </summary>
        public async Task AcceptSubscriptionAsync()
        {
            var transport = XmppTransportManager.GetTransport();
            var presence  = new Presence
            {
                To            = this.Address
              , Type          = PresenceType.Subscribed
              , TypeSpecified = true
            };

            await transport.SendAsync(presence).ConfigureAwait(false);
        }

        /// <summary>
        /// Unsubscribes from presence updates of the current user
        /// </summary>
        public async Task UnsuscribeAsync()
        {
            var transport = XmppTransportManager.GetTransport();
            var presence = new Presence
            {
                To            = this.Address
              , Type          = PresenceType.Unsubscribe
              , TypeSpecified = true
            };

            await transport.SendAsync(presence).ConfigureAwait(false);
        }

        /// <summary>
        /// Block contact
        /// </summary>
        public async Task BlockAsync()
        {
            var transport = XmppTransportManager.GetTransport();

            if (!transport.ServerCapabilities.SupportsBlocking)
            {
                return;
            }

            var iq = new InfoQuery
            {
                From  = transport.UserAddress
              , Type  = InfoQueryType.Set
              , Block = new Block(this.Address.BareAddress)
            };

            await transport.SendAsync(iq, r => this.OnContactBlocked(r), e => this.OnBlockingError(e))
                           .ConfigureAwait(false);
        }

        /// <summary>
        /// Unblock contact.
        /// </summary>
        public async Task UnBlockAsync()
        {
            var transport = XmppTransportManager.GetTransport();

            if (!transport.ServerCapabilities.SupportsBlocking)
            {
                return;
            }

            var iq = new InfoQuery
            {
                From    = transport.UserAddress
              , Type    = InfoQueryType.Set
              , Unblock = new Unblock(this.Address.BareAddress)
            };

            await transport.SendAsync(iq, r => this.OnContactUnBlocked(r), e => this.OnBlockingError(e))
                           .ConfigureAwait(false);
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
            this.Name         = ((name == null) ? String.Empty : name);
            this.DisplayName  = (!String.IsNullOrEmpty(this.Name) ? this.Name : this.address.UserName);
            this.Subscription = subscription;

            if (groups != null && groups.Count() > 0)
            {
                this.groups.AddRange(groups);
            }
        }

        private void AddDefaultResource()
        {
            var defaultResource = new XmppAddress(this.address.UserName
                                                , this.Address.DomainName
                                                , IdentifierGenerator.Generate());

            var defaultPresence = new Presence
            {
                Show              = ShowType.Offline
              , ShowSpecified     = true
              , Priority          = -127
              , PrioritySpecified = true
            };

            this.resources.Add(new ContactResource(defaultResource, defaultPresence, true));
        }

        private void SubscribeToPresenceChanges()
        {
            var transport = XmppTransportManager.GetTransport();

            transport.PresenceStream
                     .Where(message => message.FromAddress.BareAddress == this.Address && !message.IsError)
                     .Subscribe(async message => await this.OnPresenceChangedAsync(message).ConfigureAwait(false));

            transport.StateChanged
                     .Where(state => state == XmppTransportState.Closed)
                     .Subscribe(state => OnDisconnected());
        }

        private void OnDisconnected()
        {
            if (this.lastActivitySequence != null)
            {
                this.lastActivitySequence.Dispose();
                this.lastActivitySequence = null;
            }
        }

        private async Task OnPresenceChangedAsync(Presence message)
        {
            var transport = XmppTransportManager.GetTransport();
            var resource  = this.resources.SingleOrDefault(contactResource => contactResource.Address == message.From);

            if (resource == null)
            {
                resource = new ContactResource(message.From, message);
                 
                this.resources.Add(resource);
                this.newResourceStream.OnNext(resource);

                if (transport.ServerCapabilities != null && resource.SupportsEntityCapabilities)
                {
                    await resource.DiscoverCapabilitiesAsync().ConfigureAwait(false);
                }
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
                            this.UpdateResource(resource, message);
                            break;

                        case PresenceType.Unsubscribe:
                            await this.UnsuscribedAsync().ConfigureAwait(false);
                            break;
                    }
                }
                else
                {
                    this.UpdateResource(resource, message);
                }
            }

            this.RaisePropertyChanged(() => Resources);
            this.RaisePropertyChanged(() => HighPriorityResource);
        }

        private void CapabilitiesChanged()
        {
            //if (res)
            //_lastPresenceChange = Observable.Interval(TimeSpan.FromSeconds(10))
            //                                .Subscribe(buffer => this.);
        }

        private void UpdateResource(ContactResource resource, Presence message)
        {
            resource.Update(message);

            // Remove the resource information if the contact has gone offline
            if (resource.IsOffline)
            {
                this.resources.Remove(resource);
                this.removedResourceStream.OnNext(resource);
            }

            this.RaisePropertyChanged(() => Resources);
            this.RaisePropertyChanged(() => HighPriorityResource);
        }

        private async Task UnsuscribedAsync()
        {
            var transport = XmppTransportManager.GetTransport();
            var presence  = new Presence
            {
                To            = this.Address
              , Type          = PresenceType.Unsubscribed
              , TypeSpecified = true
            };

            await transport.SendAsync(presence).ConfigureAwait(false);
        }

        private void OnContactBlocked(InfoQuery response)
        {
            this.blockingStream.OnNext(ContactBlockingAction.Blocked);
        }

        private void OnContactUnBlocked(InfoQuery response)
        {
            this.blockingStream.OnNext(ContactBlockingAction.Unblocked);
        }

        private void OnBlockingError(InfoQuery error)
        {
        }
    }
}

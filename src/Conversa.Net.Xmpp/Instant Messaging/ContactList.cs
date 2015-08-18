// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.Blocking;
using Conversa.Net.Xmpp.Client;
using Conversa.Net.Xmpp.Core;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace Conversa.Net.Xmpp.InstantMessaging
{
    /// <summary>
    /// Contact's Roster
    /// </summary>
    public sealed class ContactList
        : Hub, IEnumerable<Contact>
    {
        // Messaging Subjects
        private Subject<Tuple<string, ContactListChangedAction>> contactListChangedStream;

        // Private members
        private ConcurrentBag<Contact> contacts;

        /// <summary>
        /// Gets the contact with the given bare address
        /// </summary>
        /// <param name="address">The contact bare address</param>
        /// <returns></returns>
        public Contact this[string address]
        {
            get { return this.contacts.SingleOrDefault(contact => contact.Address.BareAddress == address); }
        }

        /// <summary>
        /// Occurs when a change happens in the contact list.
        /// </summary>
        public IObservable<Tuple<string, ContactListChangedAction>> ContactListChangedStream
        {
            get { return this.contactListChangedStream.AsObservable(); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactList"/> class
        /// </summary>
        internal ContactList(XmppClient client)
            : base(client)
        {
            this.contacts                 = new ConcurrentBag<Contact>();
            this.contactListChangedStream = new Subject<Tuple<string,ContactListChangedAction>>();
        }

        /// <summary>
        /// Adds the given contact to the roster
        /// </summary>
        /// <param name="address">Contact address</param>
        /// <param name="name">Contact name</param>
        public async Task AddContactAsync(XmppAddress address, string name)
        {
            var contact = this[address];

            if (contact != null)
            {
                throw new ArgumentException("The given address is already in the contact list");
            }

            var iq = new InfoQuery()
            {
                Type   = InfoQueryType.Set
              , From   = this.Client.UserAddress.BareAddress
              , To     = this.Client.UserAddress.BareAddress
              , Roster = new Roster
                {
                    Items =
                    {
                        new RosterItem
                        {
                            Subscription = RosterSubscriptionType.None
                          , Jid          = address.BareAddress
                          , Name         = name
                        }
                    }
                }
            };

            await this.SendAsync(iq, r => this.OnAddContactResponse(r), e => this.OnAddContactError(e))
                      .ConfigureAwait(false);
        }

        /// <summary>
        /// Updates the given contact information
        /// </summary>
        public async Task UpdateContactAsync(XmppAddress address)
        {
            var contact = this[address];

            if (contact == null)
            {
                throw new ArgumentException("The given address is not in the contact list");
            }

            var iq = new InfoQuery
            {
                Type   = InfoQueryType.Set
              , From   = this.Client.UserAddress.BareAddress
              , To     = this.Client.UserAddress.BareAddress
              , Roster = new Roster(new RosterItem(contact.Address.BareAddress
                                                 , contact.Name
                                                 , contact.Subscription
                                                 , contact.Groups))
            };

            await this.SendAsync(iq).ConfigureAwait(false);
        }

        /// <summary>
        /// Removes a user from the roster
        /// </summary>
        public async Task RemoveContactAsync(XmppAddress address)
        {
            var contact = this[address];

            if (contact == null)
            {
                return;
            }

            var iq = new InfoQuery
            {
                Type   = InfoQueryType.Set
              , From   = this.Client.UserAddress.BareAddress
              , To     = this.Client.UserAddress.BareAddress
              , Roster = new Roster(new RosterItem { Jid          = address.BareAddress
                                                   , Subscription = RosterSubscriptionType.Remove })
            };

            await this.SendAsync(iq, r => this.OnRemoveContactResponse(r), e => this.OnRemoveContactError(e))
                      .ConfigureAwait(false);
        }

        /// <summary>
        /// Adds to group.
        /// </summary>
        /// <param name="groupName">Name of the group.</param>
        public async Task AddContactToGroupAsync(XmppAddress address, string groupName)
        {
            var contact = this[address];

            if (contact == null)
            {
                throw new ArgumentException("The given address is not in the contact list");
            }

            if (contact.Groups.Contains(groupName))
            {
                return;
            }

            var iq = new InfoQuery
            {
                Type   = InfoQueryType.Set
              , Roster = new Roster(new RosterItem(contact.Address, contact.Name, contact.Subscription, groupName))
            };

            await this.SendAsync(iq).ConfigureAwait(false);
        }

        /// <summary>
        /// Request Roster list to the XMPP Server
        /// </summary>
        public async Task RequestRosterAsync()
        {
            var iq = new InfoQuery
            {
                Type   = InfoQueryType.Get
              , From   = this.Client.UserAddress
              , Roster = new Roster()
            };

            await this.SendAsync(iq, r => this.OnUpdateRoster(r), e => this.OnRosterError(e))
                      .ConfigureAwait(false);
        }

        /// <summary>
        /// Refreshes the blocked contacts list
        /// </summary>
        /// <returns></returns>
        public async Task RefreshBlockedContactsAsync()
        {
#warning TODO: Change to use entity caps
            //if (!this.Client.ServiceDiscovery.SupportsBlocking)
            //{
            //    return;
            //}

            var iq = new InfoQuery
            {
                Type      = InfoQueryType.Get
              , BlockList = new BlockList()
            };

            await this.SendAsync(iq, r => this.OnBlockedContactsResponse(r), e => this.OnBlockedContactsError(e))
                      .ConfigureAwait(false);
        }

        /// <summary>
        /// Unblocks all blocked contacts
        /// </summary>
        public async Task UnBlockAllAsync()
        {
#warning TODO: Change to use entity caps
            //if (!this.Client.ServiceDiscovery.SupportsBlocking)
            //{
            //    return;
            //}

            var iq = new InfoQuery
            {
                Type    = InfoQueryType.Set
              , From    = this.Client.UserAddress
              , Unblock = new Unblock()
            };

            await this.SendAsync(iq, r => this.OnUnBlockAllResponse(r), e => this.OnUnBlockAllError(e))
                      .ConfigureAwait(false);
        }

        IEnumerator<Contact> IEnumerable<Contact>.GetEnumerator()
        {
            return this.contacts.GetEnumerator();
        }

        public IEnumerator GetEnumerator()
        {
            return this.contacts.GetEnumerator();
        }

        protected override async void OnConnected()
        {
            await this.RequestRosterAsync().ConfigureAwait(false);
            
            base.OnConnected();
        }

        protected override void OnDisconnected()
        {
            this.contactListChangedStream.Dispose();
            this.contacts.Clear();

            base.OnDisconnected();
        }

        private void SubscribeToRosterPush()
        {
            this.AddSubscription(this.Client
                                     .InfoQueryStream
                                     .Where(message => message.To == this.Client.UserAddress
                                                    && message.Roster != null
                                                    && message.IsUpdate)
                                     .Subscribe(message => this.OnRosterPush(message)));
        }

        private async void OnRosterPush(InfoQuery rosterPush)
        {
            await this.SendAsync(rosterPush.AsResponse()).ConfigureAwait(false);
        }

        private void OnAddContactResponse(InfoQuery response)
        {
            this.contactListChangedStream.OnNext(new Tuple<string, ContactListChangedAction>(null, ContactListChangedAction.Add));
        }

        private void OnAddContactError(InfoQuery error)
        {
        }

        private void OnRemoveContactResponse(InfoQuery response)
        {
            this.contactListChangedStream.OnNext(new Tuple<string, ContactListChangedAction>(null, ContactListChangedAction.Remove));
        }

        private void OnRemoveContactError(InfoQuery error)
        {
        }

        private async void OnUpdateRoster(InfoQuery response)
        {
            var roster = response.Roster;

            // It's a roster management related message
            foreach (RosterItem item in roster.Items)
            {
                var contact = this.contacts.FirstOrDefault(c => c.Address.BareAddress == item.Jid);

                if (contact == null)
                {
                    // Create the new contact
                    contact = new Contact(this.Client, item.Jid, item.Name, item.Subscription, item.Groups);

                    // Add the contact to the roster
                    this.contacts.Add(contact);
                }

                switch (item.Subscription)
                {
                    case RosterSubscriptionType.Remove:
                        this.contacts.TryTake(out contact);
                        break;

                    case RosterSubscriptionType.None:
                        // auto-accept pending subscription requests
                        if (item.IsPendingOut)
                        {
                            await contact.AcceptSubscriptionAsync().ConfigureAwait(false);
                        }
                        break;

                    default:
                        // Update contact data
                        contact.Update(item.Name, item.Subscription, item.Groups);
                        break;
                }
            }

            this.contactListChangedStream.OnNext(new Tuple<string, ContactListChangedAction>(null, ContactListChangedAction.Reset));
        }

        private void OnRosterError(InfoQuery error)
        {
        }

        private void OnBlockedContactsResponse(InfoQuery response)
        {
        }

        private void OnBlockedContactsError(InfoQuery response)
        {
        }

        private void OnUnBlockAllResponse(InfoQuery response)
        {
        }

        private void OnUnBlockAllError(InfoQuery response)
        {
        }
    }
}

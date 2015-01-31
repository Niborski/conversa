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
        : StanzaHub, IEnumerable<Contact>
    {
        private Subject<ContactList>   rosterStream;
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
        /// Occurs when the roster is updated
        /// </summary>
        public IObservable<ContactList> RosterUpdated
        {
            get { return this.rosterStream.AsObservable(); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactList"/> class
        /// </summary>
        internal ContactList(XmppClient client)
            : base(client)
        {
            this.contacts     = new ConcurrentBag<Contact>();
            this.rosterStream = new Subject<ContactList>();
        }

        /// <summary>
        /// Adds the given contact to the roster
        /// </summary>
        /// <param name="address">Contact address</param>
        /// <param name="name">Contact name</param>
        public async Task AddContactAsync(string address, string name)
        {
            var iq = new InfoQuery()
            {
                Type   = InfoQueryType.Set
              , From   = this.Client.UserAddress
              , Roster = new Roster
                {
                    Items =
                    {
                        new RosterItem
                        {
                            Subscription = RosterSubscriptionType.None
                          , Jid          = address
                          , Name         = name
                        }
                    }
                }
            };

            await this.SendAsync(iq, r => this.OnAddContactResponse(r), e => this.OnAddContactError(e))
                      .ConfigureAwait(false);
        }

        /// <summary>
        /// Deletes a user from the roster list
        /// </summary>
        public async Task RemoveContactAsync(XmppAddress address)
        {
            var iq = new InfoQuery
            {
                Type   = InfoQueryType.Set
              , From   = this.Client.UserAddress
              , Roster = new Roster()
            };
            var item = new RosterItem
            {
                Jid          = address.BareAddress
              , Subscription = RosterSubscriptionType.Remove
            };

            iq.Roster.Items.Add(item);

            await this.SendAsync(iq, r => this.OnRemoveContactError(r), e => this.OnRemoveContactError(e))
                      .ConfigureAwait(false);
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
#warning TODO: Check if blocked contact list should be stored in a separated collection or the information should be injected into XmppContact class
            if (!this.Client.ServiceDiscovery.SupportsBlocking)
            {
                return;
            }

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
            if (!this.Client.ServiceDiscovery.SupportsBlocking)
            {
                return;
            }

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
            await this.RequestRosterAsync();

            base.OnConnected();
        }

        protected override void OnDisconnected()
        {
            this.rosterStream.Dispose();
            this.contacts.Clear();

            base.OnDisconnected();
        }

        private void OnAddContactResponse(InfoQuery response)
        {
        }

        private void OnAddContactError(InfoQuery error)
        {
        }

        private void OnRemoveContactResponse(InfoQuery response)
        {
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

            this.rosterStream.OnNext(this);
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

// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.Blocking;
using Conversa.Net.Xmpp.Client;
using Conversa.Net.Xmpp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Conversa.Net.Xmpp.InstantMessaging
{
    /// <summary>
    /// Represents a <see cref="XmppRoster"/> contact.
    /// </summary>
    public sealed class XmppContact
        : XmppMessageProcessor
    {
        private string                    name;
        private string                    displayName;
        private XmppAddress               address;
        private RosterSubscriptionType    subscription;
        private List<XmppContactResource> resources;
        private List<string>              groups;

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
        public IEnumerable<XmppContactResource> Resources
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
        internal XmppContact(XmppClient             client
                           , XmppAddress            address
                           , string                 name
                           , RosterSubscriptionType subscription
                           , IEnumerable<string>    groups)
            : base(client)
        {
            this.address   = address.BareAddress;
            this.groups    = new List<string>();
            this.resources = new List<XmppContactResource>();

            this.RefreshData(name, subscription, groups);

            if (!String.IsNullOrEmpty(address.ResourceName))
            {
                this.resources.Add(new XmppContactResource(this.Client, this, address));
            }

            this.Subscribe();
        }

        /// <summary>
        /// Adds to group.
        /// </summary>
        /// <param name="groupName">Name of the group.</param>
        public async Task AddToGroupAsync(string groupName)
        {
            if (this.groups.Contains(groupName))
            {
                return;
            }

            var iq = new InfoQuery
            {
                Type   = InfoQueryType.Set
              , Roster = new Roster(new RosterItem(this.Address, this.Name, this.Subscription, groupName))
            };

            this.groups.Add(groupName);

            await this.Client.SendAsync(iq);
        }

        /// <summary>
        /// Updates the contact data.
        /// </summary>
        public async Task UpdateAsync()
        {
            var iq = new InfoQuery
            {
                Type   = InfoQueryType.Set
              , Roster = new Roster(new RosterItem(this.Address, this.Name, this.Subscription, this.groups))
            };

            await this.Client.SendAsync(iq);
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

            await this.Client.SendAsync(iq);
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

            await this.Client.SendAsync(iq);
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

        private void Subscribe()
        {
            this.AddSubscription(this.Client
                                     .PresenceStream
                                     .Where(message => ((XmppAddress)message.From).BareAddress == this.Address)
                                     .Subscribe(message => this.OnPresenceMessage(message)));
        }

        private async void OnPresenceMessage(Presence message)
        {
            var resource = this.resources.SingleOrDefault(contactResource => contactResource.Address == message.From);

            if (resource == null)
            {
                resource = new XmppContactResource(this.Client, this, address);
                this.resources.Add(resource);
            }

            if (resource.Address.BareAddress == this.Client.UserAddress.BareAddress)
            {
#warning TODO: See how to handle our own presence changes from other resources
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
                            await resource.Presence.SubscribedAsync().ConfigureAwait(false);
                            break;

                        case PresenceType.Unavailable:
                            await this.UpdateResourceAsync(resource, message).ConfigureAwait(false);
                            break;

                        case PresenceType.Unsubscribe:
                            await resource.Presence.UnsuscribedAsync().ConfigureAwait(false);
                            break;
                    }
                }
                else
                {
                    await this.UpdateResourceAsync(resource, message).ConfigureAwait(false);
                }
            }
        }

        private async Task UpdateResourceAsync(XmppContactResource resource, Presence message)
        {
            await resource.UpdateAsync(message).ConfigureAwait(false);

            // Remove the resource information if the contact has gone offline
            if (resource.Presence.ShowAs == ShowType.Offline)
            {
                this.resources.Remove(resource);
            }
        }

        internal void RefreshData(string name, RosterSubscriptionType subscription, IEnumerable<string> groups)
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
    }
}

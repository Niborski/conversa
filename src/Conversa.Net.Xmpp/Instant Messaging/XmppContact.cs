﻿// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
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
        private string                       name;
        private string                       displayName;
        private XmppAddress                  address;
        private RosterSubscriptionType       subscription;
        private List<XmppContactResource>    resources;
        private List<string>                 groups;

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
        /// Gets the contact groups.
        /// </summary>
        /// <value>The groups.</value>
        public IEnumerable<string> Groups
        {
            get { return this.groups.AsEnumerable(); }
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
        /// Gets the list available resources.
        /// </summary>
        /// <value>The resources.</value>
        public IEnumerable<XmppContactResource> Resources
        {
            get
            {
                foreach (XmppContactResource resource in this.resources)
                {
                    yield return resource;
                }
            }
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
        /// <param name="name">The name.</param>
        /// <param name="subscription">The subscription.</param>
        /// <param name="groups">The groups.</param>
        internal XmppContact(XmppClient             client
                           , XmppAddress            address
                           , string                 name
                           , RosterSubscriptionType subscription
                           , IList<string>          groups)
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
            var roster = new Roster();
            var item   = new RosterItem 
            { 
                Jid          = this.Address.BareAddress
              , Name         = this.Name
              , Subscription = this.Subscription 
            };

            item.Groups.Add(groupName);

            roster.Items.Add(item);

            var iq = InfoQuery.Create()
                              .ForUpdate();

            iq.Roster = roster;

            if (!this.groups.Contains(groupName))
            {
                this.groups.Add(groupName);
            }

            await this.Client.SendAsync(iq);
        }

        /// <summary>
        /// Updates the contact data.
        /// </summary>
        public async Task UpdateAsync()
        {
            var roster = new Roster();
            var item   = new RosterItem
            {
                Jid          = this.Address.BareAddress
              , Name         = this.Name
              , Subscription = this.Subscription
            };

            item.Groups.AddRange(this.groups);

            roster.Items.Add(item);

            var iq = InfoQuery.Create()
                              .ForUpdate();

            iq.Roster = roster;

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

            var block = new Block();

            block.Items.Add(new BlockItem { Jid = this.Address.BareAddress });

            var iq = InfoQuery.Create()
                              .ForUpdate()
                              .FromAddress(this.Client.UserAddress);

            iq.Block = block;

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

            var unblock = new Unblock();

            unblock.Items.Add(new BlockItem { Jid = this.Address.BareAddress });

            var iq = InfoQuery.Create()
                              .ForUpdate()
                              .FromAddress(this.Client.UserAddress);

            iq.Unblock = unblock;

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

        private void OnPresenceMessage(Presence message)
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
                            resource.Presence.SubscribedAsync();
                            break;

                        case PresenceType.Unavailable:
                            this.UpdateResource(resource, message);
                            break;

                        case PresenceType.Unsubscribe:
                            resource.Presence.UnsuscribedAsync();
                            break;
                    }
                }
                else
                {
                    this.UpdateResource(resource, message);
                }
            }
        }
        
        private void UpdateResource(XmppContactResource resource, Presence message)
        {
            resource.Update(message);

            // Remove the resource information if the contact has gone offline
            if (resource.Presence.ShowAs == ShowType.Offline)
            {
                this.resources.Remove(resource);
            }
        }

        internal void RefreshData(string name, RosterSubscriptionType subscription, IList<string> groups)
        {
            this.Name = ((name == null) ? String.Empty : name);

            if (!String.IsNullOrEmpty(this.Name))
            {
                this.displayName = this.name;
            }
            else
            {
                this.displayName = this.address.UserName;
            }

            this.Subscription   = subscription;
            
            if (groups != null && groups.Count > 0)
            {
                this.groups.AddRange(groups);
            }
            else
            {
                this.groups.Add("Contacts");
            }

            //this.NotifyPropertyChanged(() => DisplayName);
            //this.NotifyPropertyChanged(() => Groups);
        }
    }
}
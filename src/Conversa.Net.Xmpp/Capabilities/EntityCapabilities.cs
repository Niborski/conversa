// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.Client;
using Conversa.Net.Xmpp.Core;
using Conversa.Net.Xmpp.Discovery;
using Conversa.Net.Xmpp.Registry;
using System;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace Conversa.Net.Xmpp.Capabilities
{
    /// <summary>
    /// Entity Capabilities
    /// </summary>
    /// <remarks>
    /// XEP-0115: Entity Capabilities
    /// </remarks>
    public sealed class EntityCapabilities
        : IEntityCapabilitiesInfo
    {
        private XmppTransport                  client;
        private Subject<EntityCapabilities> capsChangedStream;
        private XmppAddress                 address;
        private ServiceInfo                 info;

        // private XmppCapabilitiesStorage capsStorage;

        /// <summary>
        /// Gets the caps changed stream.
        /// </summary>
        /// <value>
        /// The caps changed stream.
        /// </value>
        public IObservable<EntityCapabilities> CapsChangedStream
        {
            get { return this.capsChangedStream.AsObservable(); }
        }

        /// <summary>
        /// Gets the entity address
        /// </summary>
        public XmppAddress Address
        {
            get { return this.address; }
            set
            { 
                this.address = value; 
                this.Clear();
            }
        }

        public string ServiceDiscoveryNode
        {
            get { return this.info.Node; }
            set 
            { 
                this.info = new ServiceInfo { Node = value };
                this.Clear();
            }
        }

        /// <summary>
        /// Gets a value that indicates whether user tunes are supported
        /// </summary>
        public bool SupportsUserTune
        {
            get { return this.SupportsFeature(XmppFeatures.UserTune); }
        }

        /// <summary>
        /// Gets a value that indicates whether user moods are supported
        /// </summary>
        public bool SupportsUserMood
        {
            get { return this.SupportsFeature(XmppFeatures.UserMood); }
        }

        /// <summary>
        /// Gets a value that indicates whether simple communications blocking is supported
        /// </summary>
        public bool SupportsBlocking
        {
            get { return this.SupportsFeature(XmppFeatures.Blocking); }
        }

        /// <summary>
        /// Gets a value that indicates if the contact supports MUC
        /// </summary>
        public bool SupportsConference
        {
            get { return this.SupportsFeature(XmppFeatures.MultiUserChat); }
        }

        /// <summary>
        /// Gets a value that indicates if the contact supports chat state notifications
        /// </summary>
        public bool SupportsChatStateNotifications
        {
            get { return this.SupportsFeature(XmppFeatures.ChatStateNotifications); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityCapabilities"/> class.
        /// </summary>
        internal EntityCapabilities(XmppTransport client)
            : this(client, null, null)
        {
            this.client = client;
            this.info   = new ServiceInfo();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityCapabilities"/> class.
        /// </summary>
        public EntityCapabilities(XmppTransport client, XmppAddress address, string node)
        {
            this.client            = client;
            this.address           = address;
            this.info              = new ServiceInfo { Node = node };
            this.capsChangedStream = new Subject<EntityCapabilities>();
        }

        public async Task DiscoverAsync()
        {
#warning TODO: Grab Capabilities from storage or send the discovery request
            var iq = new InfoQuery
            {
                From        = this.client.UserAddress
              , To          = this.Address
              , Type        = InfoQueryType.Get
              , ServiceInfo = new ServiceInfo { Node = this.info.Node }
            };

            await this.client
                      .SendAsync(iq, r => this.OnDiscoverResponse(r), r => this.OnDiscoverError(r))
                      .ConfigureAwait(false);
        }

        public void Clear()
        {
            this.info.Features.Clear();
            this.info.Identities.Clear();
        }

        public void AddIdentity(string category, string name, string type)
        {
            if (this.info.Identities.Count(x => x.Category == category && x.Name == name && x.Type == type) == 0)
            {
                this.info.Identities.Add(new ServiceIdentity { Category = category, Name = name, Type = type });
            }
        }

        public void AddFeature(string name)
        {
            if (this.info.Features.Count(x => x.Name == name) == 0)
            {
                this.info.Features.Add(new ServiceFeature { Name = name });
            }
        }

        public bool SupportsFeature(string featureName)
        {
            return (this.info.Features.Count(f => f.Name == featureName) > 0);
        }

        private void OnDiscoverResponse(InfoQuery response)
        {
            this.info = response.ServiceInfo;
            this.capsChangedStream.OnNext(this);

#warning TODO: Update caps storage
        }

        private void OnDiscoverError(InfoQuery error)
        {
        }

//        private async Task UpdateCapabilitiesAsync(EntityCapabilities caps)
//        {
//            // Request capabilities only if they aren't cached yet for this resource
//            // or the verfiication string differs from the one that is cached
//            if (this.Capabilities == null || this.Capabilities.VerificationString != caps.VerificationString)
//            {
//                this.Capabilities.Update(caps);

//                // Check if we have the capabilities in the storage
//                if (!await this.capsStorage.IsEmptyAsync().ConfigureAwait(false))
//                {
//                    this.Capabilities = await this.capsStorage.LoadAsync().ConfigureAwait(false);
//                }
//                else if ((this.contact.Subscription == RosterSubscriptionType.Both
//                       || this.contact.Subscription == RosterSubscriptionType.To))
//#warning TODO: Review
//                        // && (!this.Presence.TypeSpecified || presence.Type == PresenceType.Unavailable)
//                {
//                    // Discover Entity Capabilities Extension Features
//                    await this.capabilities.DiscoverAsync().ConfigureAwait(false);
//                }
//            }
//        }
    }
}

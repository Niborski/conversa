// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.Client;
using Conversa.Net.Xmpp.Core;
using Conversa.Net.Xmpp.ServiceDiscovery;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conversa.Net.Xmpp.Caps
{
    /// <summary>
    /// Entity Capabilities
    /// </summary>
    /// <remarks>
    /// XEP-0115: Entity Capabilities
    /// </remarks>
    public class XmppEntityCapabilities
        : XmppMessageProcessor
    {
        private XmppAddress        address;
        private EntityCapabilities caps;
        private ServiceInfo        info;

        // private XmppCapabilitiesStorage capsStorage;

        /// <summary>
        /// Gets the entity address
        /// </summary>
        public XmppAddress Address
        {
            get { return this.address; }
        }

        /// <summary>
        /// Gets or sets the identity.
        /// </summary>
        /// <value>The identity.</value>
        public IEnumerable<ServiceIdentity> Identities
        {
            get { return this.info.Identities.AsEnumerable(); }
        }

        /// <summary>
        /// Gets the list of features
        /// </summary>
        public IEnumerable<ServiceFeature> Features
        {
            get { return this.Features.AsEnumerable(); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmppEntityCapabilities"/> class.
        /// </summary>
        public XmppEntityCapabilities(XmppClient client, XmppAddress address)
            : base(client)
        {
            this.address = address;
            this.caps    = new EntityCapabilities();
            this.info    = new ServiceInfo { Node = this.caps.Node + "#" + this.caps.VerificationString };
        }

        public void Clear()
        {
            this.info.Features.Clear();
            this.info.Identities.Clear();
        }

        public void AddFeature(string name)
        {
            if (this.info.Features.Count(x => x.Name == name) == 0)
            {
                this.info.Features.Add(new ServiceFeature { Name = name });
            }
        }

        public void AddIdentity(string category, string name, string type)
        {
            if (this.info.Identities.Count(x => x.Category == category && x.Name == name && x.Type == type) == 0)
            {
                this.info.Identities.Add(new ServiceIdentity { Category = category, Name = name, Type = type });
            }
        }

        public bool SupportsFeature(string featureName)
        {
            return (this.Features.Count(f => f.Name == featureName) > 0);
        }

        public async Task DiscoverAsync()
        {
            var iq = new InfoQuery
            {
                From        = this.Client.UserAddress
              , To          = this.Address
              , Type        = InfoQueryType.Get
              , ServiceInfo = new ServiceInfo { Node = this.caps.Node }
            };

            await this.SendAsync(iq).ConfigureAwait(false);
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

        protected override void OnResponseMessage(InfoQuery response)
        {
            this.info = response.ServiceInfo;

#warning TODO: Update caps storage

            base.OnResponseMessage(response);
        }
    }
}

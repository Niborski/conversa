// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.Client;
using Conversa.Net.Xmpp.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conversa.Net.Xmpp.ServiceDiscovery
{
    public abstract class XmppServiceDiscoveryObject
        : XmppMessageProcessor
    {
        private XmppAddress               address;
        private List<XmppServiceIdentity> identities;
        private List<XmppServiceFeature>  features;
        private List<XmppServiceItem>     items;

        /// <summary>
        /// Gets the XMPP Address (JID)
        /// </summary>
        public XmppAddress Address
        {
            get { return this.address; }
        }

        /// <summary>
        /// Gets the object identity.
        /// </summary>
        public IEnumerable<XmppServiceIdentity> Identities
        {
            get { return this.identities.AsEnumerable(); }
        }

        /// <summary>
        /// Gets the list of features supported by the XMPP Service
        /// </summary>
        public IEnumerable<XmppServiceFeature> Features
        {
            get { return this.features.AsEnumerable(); }
        }

        /// <summary>
        /// Gets the list of items for the XMPP Service
        /// </summary>
        public IEnumerable<XmppServiceItem> Items
        {
            get { return this.items.AsEnumerable(); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmppServiceDiscoveryObject"/> class.
        /// </summary>
        protected XmppServiceDiscoveryObject(XmppClient client, string address)
            : base(client)
        {
            this.address    = address;
            this.identities = new List<XmppServiceIdentity>();
            this.features   = new List<XmppServiceFeature>();
            this.items      = new List<XmppServiceItem>();

            this.RequestServiceInfoAsync();
            this.RequestServiceFeaturesAsync();
        }

        /// <summary>
        /// Discover item features
        /// </summary>
        public virtual async Task RequestServiceInfoAsync()
        {
            this.features.Clear();

            // Get Service Info
            var iq = InfoQuery.Create()
                              .ForRequest()
                              .FromAddress(this.Client.UserAddress)
                              .ToAddress(this.Address);

            iq.ServiceInfo = new ServiceInfo();

            await this.SendAsync(iq);
        }

        /// <summary>
        /// Discover item items.
        /// </summary>
        public virtual async Task RequestServiceFeaturesAsync()
        {
            this.items.Clear();

            // Get Service Details
            var iq = InfoQuery.Create()
                              .ForRequest()
                              .FromAddress(this.Client.UserAddress)
                              .ToAddress(this.Address);

            iq.ServiceItem = new ServiceItem();

            await this.SendAsync(iq);
        }

        /// <summary>
        /// Check if the item is on the given service category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public bool IsOnCategory(XmppServiceCategory category)
        {
            return (this.identities.Where(s => s.Category == XmppServiceCategory.Conference).Count() > 0);
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </returns>
        public override string ToString()
        {
            return this.Address;
        }

        protected override void OnResponseMessage(InfoQuery response)
        {
            if (response.ServiceItem != null)
            {
                this.OnServiceItem(response.ServiceItem);
            }
            else if (response.ServiceInfo != null)
            {
                this.OnServiceInfo(response.ServiceInfo);
            }

            base.OnResponseMessage(response);
        }

        private void OnServiceItem(ServiceItem serviceItem)
        {
            foreach (var itemDetails in serviceItem.Items)
            {
                this.items.Add(new XmppServiceItem(this.Client, itemDetails.Jid));
            }
        }

        private void OnServiceInfo(ServiceInfo service)
        {
            this.identities.Clear();

            foreach (var identity in service.Identities)
            {
                this.identities.Add(new XmppServiceIdentity(identity.Name, identity.Category, identity.Type));
            }

            foreach (var feature in service.Features)
            {
                this.features.Add(new XmppServiceFeature(feature.Name));
            }
        }
    }
}

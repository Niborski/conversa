// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.Client;
using Conversa.Net.Xmpp.Core;
using Conversa.Net.Xmpp.Registry;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conversa.Net.Xmpp.ServiceDiscovery
{
    /// <summary>
    /// Service Discovery
    /// </summary>
    /// <remarks>
    /// XEP-0030: Service Discovery
    /// </remarks>
    public sealed class XmppServiceDiscovery
        : XmppMessageProcessor
    {
        private string                node;
        private List<ServiceIdentity> identities;
        private List<ServiceFeature>  features;
        private List<XmppService>     services;

        /// <summary>
        /// Gets the service discover node
        /// </summary>
        public string Node
        {
            get { return this.node; }
            set { this.node = value; }
        }

        /// <summary>
        /// Gets the object identity.
        /// </summary>
        public IEnumerable<ServiceIdentity> Identities
        {
            get { return this.identities.AsEnumerable(); }
        }

        /// <summary>
        /// Gets the list of features supported by the XMPP Service
        /// </summary>
        public IEnumerable<ServiceFeature> Features
        {
            get { return this.features.AsEnumerable(); }
        }

        /// <summary>
        /// Gets the collection of discovered services
        /// </summary>
        public IEnumerable<XmppService> Services
        {
            get { return this.services.AsEnumerable(); }
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
        /// Initializes a new instance of the <see cref="XmppServiceDiscovery"/> class.
        /// </summary>
        public XmppServiceDiscovery(XmppClient client)
            : this(client, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmppServiceDiscovery"/> class.
        /// </summary>
        public XmppServiceDiscovery(XmppClient client, string node)
            : base(client)
        {
            this.node       = node;
            this.identities = new List<ServiceIdentity>();
            this.features   = new List<ServiceFeature>();
            this.services   = new List<XmppService>();
        }

        public ServiceFeature AddFeature(string featureName)
        {
            var feature = new ServiceFeature { Name = featureName };

            this.features.Add(feature);

            return feature;
        }

        public ServiceIdentity AddIdentity(string category, string name, string type)
        {
            var identity = new ServiceIdentity { Category = category, Name = name, Type = type };

            this.identities.Add(identity);

            return identity;
        }

        public async Task SendAsnwerTo(string messageId, XmppAddress to)
        {
            var iq = new InfoQuery
            {
                Id          = messageId
              , Type        = InfoQueryType.Result
              , To          = to
              , ServiceInfo = new ServiceInfo
                {
                    Node = this.node
                }
            };

            foreach (var identity in this.Identities)
            {
                iq.ServiceInfo.Identities.Add(identity);
            }

            foreach (var feature in this.Features)
            {
                iq.ServiceInfo.Features.Add(feature);
            }

            await this.Client.SendAsync(iq).ConfigureAwait(false);
        }

        /// <summary>
        /// Discover item features
        /// </summary>
        public async Task DiscoverServicesAsync()
        {
            // Get Service Info
            var iq = new InfoQuery
            {
                Type        = InfoQueryType.Get
              , From        = this.Client.UserAddress
              , To          = this.Node
              , ServiceInfo = new ServiceInfo()
            };

            await this.SendAsync(iq).ConfigureAwait(false);
        }

        /// <summary>
        /// Discover item items.
        /// </summary>
        public async Task DiscoverFeaturesAsync()
        {
            // Get Service Details
            var iq = new InfoQuery
            {
                Type        = InfoQueryType.Get
              , From        = this.Client.UserAddress
              , To          = this.Node
              , ServiceItem = new ServiceItem()
            };

            await this.SendAsync(iq).ConfigureAwait(false);
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </returns>
        public override string ToString()
        {
            return this.Node;
        }

        protected override void OnResponseMessage(InfoQuery response)
        {
            if (response.ServiceItem != null)
            {
                this.OnServiceItem(response.ServiceItem);
            }
            if (response.ServiceInfo != null)
            {
                this.OnServiceInfo(response.ServiceInfo);
            }

            base.OnResponseMessage(response);
        }

        private void OnServiceInfo(ServiceInfo service)
        {
            this.features.Clear();
            this.identities.Clear();

            foreach (var identity in service.Identities)
            {
                this.AddIdentity(identity.Category, identity.Name, identity.Type);
            }

            foreach (var feature in service.Features)
            {
                this.AddFeature(feature.Name);
            }
        }

        private void OnServiceItem(ServiceItem serviceItem)
        {
            this.services.Clear();

            foreach (var itemDetail in serviceItem.Items)
            {
                this.services.Add(new XmppService(this.Client, itemDetail.Jid));
            }
        }

        private bool SupportsFeature(string featureName)
        {
#warning TODO: Implement
            return false;
            //var q = from service in this.Services
            //        where service.Features.Where(f => f.Name == featureName).Count() > 0
            //        select service;

            //return (q.Count() > 0);
        }
    }
}

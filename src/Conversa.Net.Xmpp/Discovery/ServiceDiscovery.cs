// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.Client;
using Conversa.Net.Xmpp.Core;
using Conversa.Net.Xmpp.Registry;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conversa.Net.Xmpp.Discovery
{
    /// <summary>
    /// Service Discovery
    /// </summary>
    /// <remarks>
    /// XEP-0030: Service Discovery
    /// </remarks>
    public sealed class ServiceDiscovery
        : Hub
    {
        private string                node;
        private List<ServiceIdentity> identities;
        private List<ServiceFeature>  features;
        private List<Service>         services;

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
        public IEnumerable<Service> Services
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
        /// Initializes a new instance of the <see cref="ServiceDiscovery"/> class.
        /// </summary>
        public ServiceDiscovery(XmppClient client)
            : this(client, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceDiscovery"/> class.
        /// </summary>
        public ServiceDiscovery(XmppClient client, string node)
            : base(client)
        {
            this.node       = node;
            this.identities = new List<ServiceIdentity>();
            this.features   = new List<ServiceFeature>();
            this.services   = new List<Service>();
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
        /// Discover services
        /// </summary>
        public async Task DiscoverServicesAsync()
        {
            var iq = new InfoQuery
            {
                Type        = InfoQueryType.Get
              , From        = this.Client.UserAddress
              , To          = this.Node
              , ServiceItem = new ServiceItem()
            };

            await this.SendAsync(iq, r => this.OnDiscoverServices(r), e => this.OnError(e))
                      .ConfigureAwait(false);
        }

        /// <summary>
        /// Discover features.
        /// </summary>
        public async Task DiscoverFeaturesAsync()
        {
            var iq = new InfoQuery
            {
                Type        = InfoQueryType.Get
              , From        = this.Client.UserAddress
              , To          = this.Node
              , ServiceInfo = new ServiceInfo()
            };

            await this.SendAsync(iq, r => this.OnDiscoverFeatures(r), e => this.OnError(e))
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
            return this.Node;
        }

        private void OnDiscoverServices(InfoQuery response)
        {
            this.services.Clear();

            foreach (var itemDetail in response.ServiceItem.Items)
            {
                this.services.Add(new Service(this.Client, itemDetail.Jid));
            }
        }

        private void OnDiscoverFeatures(InfoQuery response)
        {
            this.features.Clear();
            this.identities.Clear();

            foreach (var identity in response.ServiceInfo.Identities)
            {
                this.AddIdentity(identity.Category, identity.Name, identity.Type);
            }

            foreach (var feature in response.ServiceInfo.Features)
            {
                this.AddFeature(feature.Name);
            }
        }

        private void OnError(InfoQuery error)
        {
        }

        private bool SupportsFeature(string featureName)
        {
            var q = from feature in this.Features
                    where feature.Name == featureName
                    select feature;

            return (q.Count() > 0);
        }
    }
}

// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.Client;
using Conversa.Net.Xmpp.Core;
using Conversa.Net.Xmpp.Registry;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Conversa.Net.Xmpp.ServiceDiscovery
{
    /// <summary>
    /// XMPP Service Discovery
    /// </summary>
    public sealed class XmppServiceDiscovery
        : XmppMessageProcessor
    {
        private string domainName;
        private bool   supportsServiceDiscovery;

        private ObservableCollection<XmppService> services;

        /// <summary>
        /// Gets the collection of discovered services
        /// </summary>
        public ObservableCollection<XmppService> Services
        {
            get
            {
                if (this.services == null)
                {
                    this.services = new ObservableCollection<XmppService>();
                }

                return this.services;
            }
        }

        /// <summary>
        /// Gets a value that indicates if it supports multi user chat
        /// </summary>
        public bool SupportsMultiuserChat
        {
            get { return this.SupportsFeature(XmppFeatures.MultiUserChat); }
        }

        /// <summary>
        /// Gets a value that indicates whether service discovery is supported
        /// </summary>
        public bool SupportsServiceDiscovery
        {
            get { return this.supportsServiceDiscovery; }
            private set { this.supportsServiceDiscovery = value; }
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
            get { return this.SupportsFeature(XmppFeatures.SimpleCommunicationsBlocking); }
        }

        /// <summary>
        /// Gets the service discovery domain name
        /// </summary>
        private string DomainName
        {
            get
            {
                if (String.IsNullOrEmpty(this.domainName))
                {
                    return this.Client.UserAddress.DomainName;
                }
                else
                {
                    return this.domainName;
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:XmppServiceDiscovery"/> class.
        /// </summary>
        /// <param name="session">The session.</param>
        internal XmppServiceDiscovery(XmppClient client)
            : base(client)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:XmppServiceDiscovery"/> class.
        /// </summary>
        /// <param name="session">The session.</param>
        internal XmppServiceDiscovery(XmppClient client, string domainName)
            : this(client)
        {
            this.domainName = domainName;
        }

        /// <summary>
        /// Clears service discovery data
        /// </summary>
        public void Clear()
        {
            this.Services.Clear();
        }

        public XmppService GetService(XmppServiceCategory category)
        {
            return this.Services
                       .Where(s => s.IsOnCategory(XmppServiceCategory.Conference))
                       .FirstOrDefault();
        }

        /// <summary>
        /// Discover existing services provided by the XMPP Server
        /// </summary>
        /// <returns></returns>
        public async Task DiscoverServicesAsync()
        {
            this.Clear();

            var iq = InfoQuery.Create()
                              .FromAddress(this.Client.UserAddress)
                              .ToAddress(this.DomainName)
                              .ForRequest();

            iq.ServiceItem = new ServiceItem();

            await this.SendMessageAsync(iq);
        }

        private bool SupportsFeature(string featureName)
        {
            var q = from service in this.Services
                    where service.Features.Where(f => f.Name == featureName).Count() > 0
                    select service;

            return (q.Count() > 0);
        }

        protected override void OnResponseMessage(InfoQuery response)
        {
            this.SupportsServiceDiscovery = true;

            if (response.ServiceItem != null)
            {
                // List of available services
                foreach (var itemDetail in response.ServiceItem.Items.OfType<ServiceItemDetail>())
                {
                    this.services.Add(new XmppService(this.Client, itemDetail.Jid));
                }
            }
        }
    }
}

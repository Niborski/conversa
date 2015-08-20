// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.Client;
using Conversa.Net.Xmpp.Core;
using Conversa.Net.Xmpp.Discovery;
using Conversa.Net.Xmpp.Registry;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conversa.Net.Xmpp.Eventing
{
    /// <summary>
    /// Personal Eventing
    /// </summary>
    /// <remarks>
    /// XEP-0163: Personal Eventing Protocol
    /// </remarks>
    public sealed class PersonalEventing
    {
        private List<string> features;
        private bool         isUserTuneEnabled;

        /// <summary>
        /// Gets the collection of features ( if personal eventing is supported )
        /// </summary>
        public IEnumerable<string> Features
        {
            get { return this.features.AsEnumerable(); }
        }

        /// <summary>
        /// Gets a value that indicates if it supports user moods
        /// </summary>
        public bool SupportsUserMood
        {
            get { return this.SupportsFeature(XmppFeatures.UserMood); }
        }

        /// <summary>
        /// Gets or sets a value that indicates if user tune is enabled
        /// </summary>
        public bool IsUserTuneEnabled
        {
            get { return this.isUserTuneEnabled; }
            set { this.isUserTuneEnabled = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:PersonalEventing"/> class.
        /// </summary>
        /// <param name="client">XMPP Client instance.</param>
        internal PersonalEventing()
        {
            this.features = new List<string>();
        }

        /// <summary>
        /// Discover if we have personal eventing support enabled
        /// </summary>
        /// <returns></returns>
        internal async Task DiscoverSupportAsync()
        {
            var transport = XmppTransportManager.GetTransport();
            var iq        = new InfoQuery
            {
                Type        = InfoQueryType.Get
              , From        = transport.UserAddress
              , To          = transport.UserAddress.BareAddress
              , ServiceItem = new ServiceItem()
            };

            await transport.SendAsync(iq, r => this.OnDiscoverSupport(r), e => this.OnDiscoverError(e))
                           .ConfigureAwait(false);
        }

        private void OnDiscoverSupport(InfoQuery response)
        {
            this.features.Clear();

            foreach (var details in response.ServiceItem.Items)
            {
                this.features.Add(details.Node);
            }
        }

        private void OnDiscoverError(InfoQuery error)
        {
        }

        private bool SupportsFeature(string featureName)
        {
            var q = from feature in this.features
                    where feature == featureName
                    select feature;

            return (q.Count() > 0);
        }
    }
}

// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.Client;
using Conversa.Net.Xmpp.Core;
using Conversa.Net.Xmpp.Registry;
using Conversa.Net.Xmpp.ServiceDiscovery;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conversa.Net.Xmpp.PersonalEventing
{
    /// <summary>
    /// XMPP Personal Eventing
    /// </summary>
    public sealed class XmppPersonalEventing
        : XmppMessageProcessor
    {
        private List<string>			features;
        private AmipNowPlayingListerner	nowPlayingListener;
        private bool                    isUserTuneEnabled;

        /// <summary>
        /// Gets the collection of features ( if personal eventing is supported )
        /// </summary>
        public IEnumerable<string> Features
        {
            get
            {
                foreach (string feature in this.features)
                {
                    yield return feature;
                }
            }
        }

        /// <summary>
        /// Gets a value that indicates if it supports user tunes
        /// </summary>
        public bool SupportsUserTune
        {
            get { return this.SupportsFeature(XmppFeatures.UserTune); }
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
        /// Initializes a new instance of the <see cref="T:XmppServiceDiscovery"/> class.
        /// </summary>
        /// <param name="client">XMPP Client instance.</param>
        internal XmppPersonalEventing(XmppClient client)
            : base(client)
        {
            this.features           = new List<string>();
            this.nowPlayingListener	= new AmipNowPlayingListerner(this.Client);
        }

        /// <summary>
        /// Discover if we have personal eventing support enabled
        /// </summary>
        /// <returns></returns>
        internal async Task DiscoverSupportAsync()
        {
            var iq = InfoQuery.Create()
                              .FromAddress(this.Client.UserAddress)
                              .ToAddress(this.Client.UserAddress.BareAddress)
                              .ForRequest();

            iq.ServiceItem = new ServiceItem();

            this.features.Clear();

            await this.SendAsync(iq);
        }

        private bool SupportsFeature(string featureName)
        {
            var q = from feature in this.features
                    where feature == featureName
                    select feature;

            return (q.Count() > 0);
        }

        protected override void OnResponseMessage(InfoQuery response)
        {
            foreach (var details in response.ServiceItem.Items)
            {
                this.features.Add(details.Node);
            }

            //this.NotifyPropertyChanged(() => SupportsUserTune);
            //this.NotifyPropertyChanged(() => SupportsUserMood);

            if (this.SupportsUserTune)
            {
                this.nowPlayingListener.Start();
            }
        }
    }
}
